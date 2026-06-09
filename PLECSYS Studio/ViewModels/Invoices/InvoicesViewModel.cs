using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using PLECSYS_Studio.Services.Invoices;
using PLECSYS_Studio.ViewModels.Invoices.Filters;
using PLECSYS_Studio.ViewModels.Invoices.Filters.Options;
using PLECSYS_Studio.ViewModels.Messages;
using PLECSYS_Studio.Services.InvoiceService;
using PLECSYS_Studio.Data.Invoices;
using System.Collections.ObjectModel;
using PLECSYS_Studio.Services;

namespace PLECSYS_Studio.ViewModels.Invoices
{
    public partial class InvoicesViewModel : ObservableObject
    {
        private readonly IInvoiceService _service;
        private readonly IInvoicePdfService _pdfService;
        private readonly InvoiceData _invoiceData;
        private readonly SessionService _session;

        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private string statusMessage = string.Empty;

        public ClientFilterViewModel ClientFilter { get; }
        public CurrencyFilterViewModel CurrencyFilter { get; }
        public DateFilterViewModel DateFilter { get; }

        public ObservableCollection<SingleInvoiceViewModel> Invoices { get; } = [];

        public InvoicesViewModel(
            IInvoiceService service,
            IInvoicePdfService pdfService,
            InvoiceData invoiceData,
            SessionService session,
            ClientFilterViewModel clientFilter,
            CurrencyFilterViewModel currencyFilter,
            DateFilterViewModel dateFilter)
        {
            _service = service;
            _pdfService = pdfService;
            _invoiceData = invoiceData;
            _session = session;

            ClientFilter = clientFilter;
            CurrencyFilter = currencyFilter;
            DateFilter = dateFilter;

            WeakReferenceMessenger.Default.Register<PaymentRegisteredMessage>(this, (_, msg) =>
            {
                var invoice = Invoices.FirstOrDefault(i => i.Consecutive == msg.InvoiceConsecutive);
                if (invoice is null) return;
                // SingleInvoiceViewModel ya maneja esto internamente via su propio Register
            });

            WeakReferenceMessenger.Default.Register<ClaimRegisteredMessage>(this, (_, msg) =>
            {
                var invoice = Invoices.FirstOrDefault(i => i.Consecutive == msg.InvoiceConsecutive);
                if (invoice is null) return;
                // SingleInvoiceViewModel ya maneja esto internamente via su propio Register
            });
        }

        [RelayCommand]
        public async Task LoadInvoices()
        {
            if (IsBusy) return;
            IsBusy = true;
            StatusMessage = "Cargando facturas...";

            try
            {
                var response = await _service.LoadInvoices(_session.GetEmail(), _session.GetCompanyId());
                Invoices.Clear();

                if (response.Data is not null)
                {
                    foreach (var invoice in response.Data)
                    {
                        var vm = new SingleInvoiceViewModel(_pdfService, _invoiceData)
                        {
                            Invoice_id = invoice.Invoice_id,
                            Consecutive = invoice.Consecutive,
                            Total_voucher = invoice.Total_voucher,
                            User_creator_id = invoice.User?.Email ?? string.Empty,
                            Sell_company = invoice.Sell_company?.Company_name ?? string.Empty,
                            Charged_company = invoice.Charged_company?.Company_name ?? string.Empty,
                        };

                        Invoices.Add(vm);
                    }

                    // Poblar filtros
                    var clients = response.Data
                        .Where(i => !string.IsNullOrWhiteSpace(i.User?.Email))
                        .Select(i => new ClientOption
                        {
                            Email = i.User?.Email,
                            DisplayName = $"{i.User?.Name} {i.User?.First_lastname} {i.User?.Second_lastname}",
                        })
                        .DistinctBy(i => i.DisplayName)
                        .OrderBy(i => i.DisplayName)
                        .ToList();

                    var currencies = response.Data
                        .Where(i => !string.IsNullOrWhiteSpace(i.Currency?.Currency_code))
                        .Select(i => new CurrencyOption
                        {
                            Currency_id = i.Currency?.Currency_id ?? 0,
                            Currency_code = i.Currency?.Currency_code,
                        })
                        .DistinctBy(i => i.Currency_code)
                        .OrderBy(i => i.Currency_code)
                        .ToList();

                    ClientFilter.LoadClients(clients);
                    CurrencyFilter.LoadCurrencies(currencies);

                    StatusMessage = "Se han cargado correctamente las facturas";
                }
                else
                {
                    StatusMessage = $"No se recibieron facturas: {response?.Message ?? "Respuesta nula"}";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al listar facturas: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}