using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PLECSYS_Studio.Services.Invoices;
using PLECSYS_Studio.Wrappers.Invoices;
using System.Collections.ObjectModel;

namespace PLECSYS_Studio.ViewModels
{
    public partial class HomePageViewModel : ObservableObject
    {
        private readonly IInvoiceService _invoiceService;

        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private string statusMessage = string.Empty;

        public ObservableCollection<InvoiceResponse> TodayInvoices { get; } = [];
        public ObservableCollection<InvoiceResponse> WeekInvoices { get; } = [];
        public ObservableCollection<InvoiceResponse> MonthInvoices { get; } = [];

        // Para controlar el EmptyView en BindableLayout
        public bool HasNoTodayInvoices => TodayInvoices.Count == 0;
        public bool HasNoWeekInvoices => WeekInvoices.Count == 0;
        public bool HasNoMonthInvoices => MonthInvoices.Count == 0;

        public HomePageViewModel(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [RelayCommand]
        public async Task LoadExpiryInvoices(DateTime selectedDate)
        {
            if (IsBusy) return;
            IsBusy = true;

            try
            {
                var endOfDay = selectedDate.Date.AddDays(1).AddTicks(-1);
                var result = await _invoiceService.GetInvoicesByExpiryDate(endOfDay);

                TodayInvoices.Clear();
                WeekInvoices.Clear();
                MonthInvoices.Clear();

                if (!result.Success || result.Data is null) return;

                var today = selectedDate.Date;

                foreach (var invoice in result.Data)
                {
                    if (invoice.Expiry_date is null) continue;
                    var expiry = invoice.Expiry_date.Value.Date;
                    var daysOverdue = (today - expiry).Days;

                    if (expiry == today)
                        TodayInvoices.Add(invoice);        // vence hoy
                    else if (daysOverdue <= 7)
                        WeekInvoices.Add(invoice);         // vencida hace 1-7 días
                    else if (daysOverdue <= 30)
                        MonthInvoices.Add(invoice);        // vencida hace 8-30 días
                    else
                        MonthInvoices.Add(invoice);        // vencida hace más de 30 días
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
                // Notificar propiedades de EmptyView al terminar de cargar
                OnPropertyChanged(nameof(HasNoTodayInvoices));
                OnPropertyChanged(nameof(HasNoWeekInvoices));
                OnPropertyChanged(nameof(HasNoMonthInvoices));
            }
        }

        [RelayCommand]
        public async Task GoToInvoicesList() => await Shell.Current.GoToAsync("/Invoices");

        [RelayCommand]
        public async Task GoToMap() => await Shell.Current.GoToAsync(nameof(Views.GPS.GPSPage));
    }
}