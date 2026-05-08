using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using PLECSYS_Studio.Services.Claims;
using PLECSYS_Studio.Services.Users;
using PLECSYS_Studio.ViewModels.Messages;
using PLECSYS_Studio.Wrappers.Claims;
using System.Collections.ObjectModel;

namespace PLECSYS_Studio.ViewModels.Claims
{
    [QueryProperty(nameof(InvoiceConsecutiveString), "invoiceConsecutive")]

    public partial class RegisterClaimViewModel: ObservableObject
    {
        private readonly IClaimService _claimService;
        private readonly IUserService _userService;

        public RegisterClaimViewModel(IClaimService claimService, IUserService userService)
        {
            _claimService = claimService;
             _userService = userService;
        }

        [ObservableProperty] private string? invoiceConsecutiveString;
        public int InvoiceConsecutive => int.TryParse(InvoiceConsecutiveString, out var n) ? n : 0;

        [ObservableProperty] private DateTime? recordDate = DateTime.Today;
        [ObservableProperty] private string? description;
        [ObservableProperty] private string? claimAmountText;

        public ObservableCollection<AttachmentItem> Attachments { get; } = new();

        [ObservableProperty] private bool isSaving;

        public static readonly string[] AllowedContentTypes = { "application/pdf", "image/png", "image/jpeg" };
        public const long MaxFileBytes = 5 * 1024 * 1024;
        public const int MaxFiles = 5;

        [RelayCommand]
        private async Task PickFilesAsync()
        {
            try
            {
                if (Attachments.Count >= MaxFiles)
                {
                    await Shell.Current.DisplayAlert("Límite", $"Máximo {MaxFiles} archivos.", "OK");
                    return;
                }

                var results = await FilePicker.Default.PickMultipleAsync(new PickOptions
                {
                    PickerTitle = "Selecciona adjuntos (PDF/JPG/PNG)"
                });

                if (results is null) return;

                foreach (var r in results)
                {
                    if (Attachments.Count >= MaxFiles) break;

                    var contentType = r.ContentType ?? MimeTypeFromFileName(r.FileName);
                    if (!AllowedContentTypes.Contains(contentType))
                    {
                        await Shell.Current.DisplayAlert("Archivo inválido", $"{r.FileName}: tipo no permitido.", "OK");
                        continue;
                    }

                    using var s = await r.OpenReadAsync();
                    if (s.Length > MaxFileBytes)
                    {
                        await Shell.Current.DisplayAlert("Archivo grande", $"{r.FileName}: supera 5MB.", "OK");
                        continue;
                    }

                    var ms = new MemoryStream();
                    await s.CopyToAsync(ms);
                    ms.Position = 0;

                    Attachments.Add(new AttachmentItem(r.FileName, contentType, ms));
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"No se pudieron cargar archivos: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        private void RemoveAttachment(AttachmentItem? item)
        {
            if (item is null) return;
            Attachments.Remove(item);
            try { item.Stream?.Dispose(); } catch { }
        }

        [RelayCommand]
        private async Task SaveAsync()
        {
            if (IsSaving) return;
            IsSaving = true;

            try
            {
                if (InvoiceConsecutive <= 0)
                {
                    await Shell.Current.DisplayAlert("Error", "Factura inválida.", "OK");
                    return;
                }
                if (RecordDate is null)
                {
                    await Shell.Current.DisplayAlert("Faltan datos", "Seleccione la fecha del reclamo.", "OK");
                    return;
                }
                if (string.IsNullOrWhiteSpace(Description))
                {
                    await Shell.Current.DisplayAlert("Faltan datos", "Ingrese la descripción del reclamo.", "OK");
                    return;
                }
                if (RecordDate > DateTime.Today)
                {
                    await Shell.Current.DisplayAlert("Fecha inválida", "No puede seleccionar una fecha futura.", "OK");
                    return;
                }

                decimal? claimAmount = null;
                if (!string.IsNullOrWhiteSpace(ClaimAmountText))
                {
                    if (decimal.TryParse(ClaimAmountText.Replace(",", ""), out var parsed) && parsed >= 0)
                        claimAmount = parsed;
                    else
                    {
                        await Shell.Current.DisplayAlert("Monto inválido", "Ingrese un monto válido o deje vacío.", "OK");
                        return;
                    }
                }

                var req = new ClaimRequest
                {
                    InvoiceConsecutive = InvoiceConsecutive,
                    RecordDate = RecordDate.Value,
                    Description = Description!.Trim(),
                    ClaimAmount = claimAmount,
                    // User_email = _userService.CurrentEmail
                };

                var files = Attachments.Select(a => (Stream: (Stream)a.Stream, FileName: a.FileName, ContentType: a.ContentType)).ToList();
                var resp = await _claimService.RegisterClaimAsync(req, files);

                if (resp is { Success: true })
                {
                    WeakReferenceMessenger.Default.Send(new ClaimRegisteredMessage
                    {
                        InvoiceConsecutive = InvoiceConsecutive,
                        NewStatus = string.IsNullOrWhiteSpace(resp.NewStatus) ? "Con reclamo" : resp.NewStatus
                    });

                    await Shell.Current.DisplayAlert("Éxito", resp.Message.Length > 0 ? resp.Message : "Reclamo registrado.", "OK");
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", resp?.Message ?? "No se pudo registrar el reclamo.", "OK");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
            }
            finally { IsSaving = false; }
        }

        [RelayCommand]
        private async Task GoBackAsync() => await Shell.Current.GoToAsync("..");

        private static string MimeTypeFromFileName(string fileName)
        {
            var ext = Path.GetExtension(fileName)?.ToLowerInvariant();
            return ext switch
            {
                ".pdf" => "application/pdf",
                ".png" => "image/png",
                ".jpg" or ".jpeg" => "image/jpeg",
                _ => "application/octet-stream"
            };
        }
    }

    public sealed class AttachmentItem(string fileName, string contentType, MemoryStream stream)
    {
        public string FileName { get; } = fileName;
        public string ContentType { get; } = contentType;
        public MemoryStream Stream { get; } = stream;
    }

}