using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PLECSYS_Studio.Services;
using PLECSYS_Studio.Services.Invoices;
using PLECSYS_Studio.Wrappers.Invoices;
using System.Collections.ObjectModel;

namespace PLECSYS_Studio.ViewModels.Invoices.Filters
{
    public partial class DateFilterViewModel : ObservableObject
    {
        private readonly IInvoiceService _service;
        private readonly SessionService _session;

        [ObservableProperty]
        private DateTime? selectedDate = DateTime.Today;

        public ObservableCollection<InvoiceResponse> InvoicesReference { get; set; }

        public DateFilterViewModel(IInvoiceService service, SessionService session)
        {
            _service = service;
            _session = session;
        }

        [RelayCommand]
        public async Task FilterByDate()
        {
            if (selectedDate is null)
            {
                var response = await _service.LoadInvoices(_session.GetEmail(), _session.GetCompanyId());
                InvoicesReference.Clear();
                if (response.Data is not null)
                    foreach (var item in response.Data)
                        InvoicesReference.Add(item);
            }
            else
            {
                var filtered = await _service.GetInvoicesByDate(selectedDate.Value);
                InvoicesReference.Clear();
                if (filtered.Data is not null)
                    foreach (var invoice in filtered.Data)
                        InvoicesReference.Add(invoice);
            }
        }


        partial void OnSelectedDateChanged(DateTime? value)
        {
            if (value.HasValue)
                FilterByDateCommand.Execute(null);
        }
    }
}
