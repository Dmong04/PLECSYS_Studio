using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using PLECSYS_Studio.Views.Popups;
using PLECSYS_Studio.Views.Claims;
using PLECSYS_Studio.Services.InvoiceService;
using PLECSYS_Studio.Data.Invoices;
using System.Net;
using PLECSYS_Studio.ViewModels.Messages;
using PLECSYS_Studio.Views.Payments;
using PLECSYS_Studio.Views.History;
using CommunityToolkit.Maui.Extensions;

namespace PLECSYS_Studio.ViewModels
{
    public partial class SingleInvoiceViewModel : ObservableObject
    {
        private readonly IInvoicePdfService _pdfService;
        private readonly InvoiceData _data;

        public int Consecutive { get; set; }
        public int Invoice_id { get; set; }
        public decimal Total_voucher { get; set; }
        public string User_creator_id { get; set; } = string.Empty;
        public string Sell_company { get; set; } = string.Empty;
        public string Charged_company { get; set; } = string.Empty;


        [ObservableProperty] private string status = "Pendiente";
        [ObservableProperty] private decimal pendingBalance;

        [ObservableProperty] private bool isExpanded;
        [ObservableProperty] private bool isDownloading;
        [ObservableProperty] private double downloadProgress;

        private Popup? currentPopup;

        
        public SingleInvoiceViewModel(IInvoicePdfService pdfService, InvoiceData data)
        {
            _pdfService = pdfService;
            _data = data;

            WeakReferenceMessenger.Default.Register<PaymentRegisteredMessage>(this, (_,msg) =>
            {
                if(msg.InvoiceConsecutive !=Consecutive) return;

                PendingBalance = msg.NewPendingBalance;
                Status = string.IsNullOrWhiteSpace(msg.NewStatus)
                    ? (PendingBalance <= 0 ? "Pagado" : "Parcial")
                    : msg.NewStatus;
            });

            WeakReferenceMessenger.Default.Register<ClaimRegisteredMessage>(this, (_, msg) =>
            {
                if (msg.InvoiceConsecutive != Consecutive) return;
                Status = string.IsNullOrWhiteSpace(msg.NewStatus) ? "Con reclamo" : msg.NewStatus!;
            });
        }

        //PopUp Funtions

        [RelayCommand]
        public async Task OpenPopup()
        {
            var popup = new InvoicePopUp(this)
            {
                Consecutive = Consecutive,
                PendingBalance = PendingBalance
            };

            if (Shell.Current != null)
            {
                await Shell.Current.ShowPopupAsync(popup);
            }
        }

        [RelayCommand]
        public void ViewDetails()
        {
            Shell.Current.DisplayAlert("Detalles", "Ejemplo de pop up", "Aceptar");
        }

        [RelayCommand]
        public void Delete()
        {
            Shell.Current.DisplayAlert("Eliminar", "Ejemplo de pop up", "Aceptar");
        }

        [RelayCommand]
        public void ClosePopup()
        {
            if (currentPopup != null)
            {
                currentPopup.CloseAsync();
                currentPopup = null;
            }
        }
        // Invoice Funtions
        [RelayCommand]
        public async Task OpenHistoryAsync()
        {
            try
            {
                currentPopup?.CloseAsync();
                await Shell.Current.GoToAsync($"{nameof(InvoiceHistoryPage)}?invoiceConsecutive={Consecutive}");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error",$"No se pudo abrir el histórico: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        public async Task RegisterPaymentAsync()
        {
            try 
            { 
                currentPopup?.CloseAsync();

                await Shell.Current.GoToAsync($"{nameof(RegisterPaymentPage)}?invoiceConsecutive={Consecutive}&pendingBalance={PendingBalance}");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"No se pudo abrir la página de registro de pagos: {ex.Message}", "Ok");
            }
        }

        [RelayCommand]
        public async Task RegisterClaimAsync()
        {
            try
            {
                currentPopup?.CloseAsync();
                await Shell.Current.GoToAsync($"{nameof(RegisterClaimPage)}?invoiceConsecutive={Consecutive}");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"No se pudo abrir la página de reclamos: {ex.Message}", "Ok");
            }
        }

        [RelayCommand]
        public async Task DownloadPdfAsync()
        {
            bool confirm = await Shell.Current.DisplayAlert(
                "Descargar PDF",
                $"¿Desea descargar la factura #{Consecutive} en PDF?",
                "Si", "Cancelar");

            if (!confirm) return;

            await ExecuteDownloadPdfAsync();
        }

        private async Task ExecuteDownloadPdfAsync()
        {
            try
            {
                IsDownloading = true;
                DownloadProgress = 0;
                var progress = new Progress<double>(p => DownloadProgress = p);

                string path = await _pdfService.DownloadInvoicePdfAsync(Invoice_id, progress); // ← Invoice_id

                IsDownloading = false;
                await Shell.Current.DisplayAlert("Descarga completa",
                    $"La factura se ha guardado como:\n{Path.GetFileName(path)}",
                    "Aceptar");

                await Launcher.OpenAsync(new OpenFileRequest { File = new ReadOnlyFile(path) });
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                IsDownloading = false;

                bool regen = await Shell.Current.DisplayAlert(
                    "PDF no disponible",
                    "No se encontró el PDF. ¿Desea volver a intentarlo?",
                    "Si", "Cancelar");

                if (!regen) return;
                await RegenerateAndRetryAsync();
            }
            catch (Exception ex)
            {
                IsDownloading = false;
                await Shell.Current.DisplayAlert("Error",
                    $"Ocurrió un error al descargar el PDF:\n{ex.Message}",
                    "Aceptar");
            }
        }

        private async Task RegenerateAndRetryAsync()
        {
            try
            {
                IsDownloading = true;
                DownloadProgress = 0;

                bool confirmation = await _data.RegenerateInvoicePdfAsync(Invoice_id); // ← Invoice_id

                if (!confirmation)
                {
                    IsDownloading = false;
                    await Shell.Current.DisplayAlert("Error",
                        "No se pudo regenerar el PDF en el servidor.",
                        "Aceptar");
                    return;
                }

                var progress = new Progress<double>(p => DownloadProgress = p);
                string path = await _pdfService.DownloadInvoicePdfAsync(Invoice_id, progress); // ← Invoice_id

                IsDownloading = false;
                await Shell.Current.DisplayAlert("Descarga completa",
                    $"La factura se ha guardado como:\n{Path.GetFileName(path)}",
                    "Aceptar");

                await Launcher.OpenAsync(new OpenFileRequest { File = new ReadOnlyFile(path) });
            }
            catch (Exception ex)
            {
                IsDownloading = false;
                await Shell.Current.DisplayAlert("Error",
                    $"Ocurrió un error al regenerar el PDF:\n{ex.Message}",
                    "Aceptar");
            }
        }
    }
}