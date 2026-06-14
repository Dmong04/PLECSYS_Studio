using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using PLECSYS_Studio.Services;
using PLECSYS_Studio.Services.Claims;
using PLECSYS_Studio.ViewModels.Messages;
using PLECSYS_Studio.Wrappers.Claims;
using System.Text.Json;

namespace PLECSYS_Studio.ViewModels.Claims
{
    public partial class RegisterClaimViewModel : ObservableObject, IQueryAttributable
    {
        private readonly IClaimService _claimService;
        private readonly SessionService _session;

        [ObservableProperty] private int invoiceId;
        [ObservableProperty] private int invoiceConsecutive;
        [ObservableProperty] private DateTime recordDate = DateTime.Today;
        [ObservableProperty] private string? description;
        [ObservableProperty] private string? claimAmountText;
        [ObservableProperty] private bool isSaving;

        public RegisterClaimViewModel(IClaimService claimService, SessionService session)
        {
            _claimService = claimService;
            _session = session;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("invoiceId", out var idObj))
                InvoiceId = Convert.ToInt32(idObj);

            if (query.TryGetValue("invoiceConsecutive", out var consecutiveObj))
                InvoiceConsecutive = Convert.ToInt32(consecutiveObj);
        }

        [RelayCommand]
        private async Task SaveAsync()
        {
            if (IsSaving) return;
            IsSaving = true;

            try
            {
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

                decimal claimAmount = 0;
                if (!string.IsNullOrWhiteSpace(ClaimAmountText))
                {
                    if (!decimal.TryParse(ClaimAmountText.Replace(",", ""), out claimAmount) || claimAmount < 0)
                    {
                        await Shell.Current.DisplayAlert("Monto inválido", "Ingrese un monto válido o deje vacío.", "OK");
                        return;
                    }
                }

                var request = new ClaimRequest
                {
                    Record_date = RecordDate,
                    User_id = _session.GetEmail(),
                    Description = Description.Trim(),
                    Invoice_id = InvoiceId,
                    Claim_amount = claimAmount
                };

                var result = await _claimService.RegisterClaimAsync(request);

                var json = JsonSerializer.Serialize(request);
                Console.WriteLine($"Request body: {json}");

                if (result.Success)
                {
                    WeakReferenceMessenger.Default.Send(new ClaimRegisteredMessage
                    {
                        InvoiceConsecutive = InvoiceConsecutive,
                        NewStatus = "Con reclamo"
                    });

                    await Shell.Current.DisplayAlert("Éxito", result.Message ?? "Reclamo registrado.", "OK");
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", result.Message ?? "No se pudo registrar el reclamo.", "OK");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
            }
            finally
            {
                IsSaving = false;
            }
        }

        [RelayCommand]
        private async Task GoBackAsync() => await Shell.Current.GoToAsync("..");
    }
}