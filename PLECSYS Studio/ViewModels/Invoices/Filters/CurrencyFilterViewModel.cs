using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PLECSYS_Studio.Services.Invoices;
using PLECSYS_Studio.ViewModels.Invoices.Filters.Options;
using PLECSYS_Studio.Wrappers.Invoices;
using System.Collections.ObjectModel;

namespace PLECSYS_Studio.ViewModels.Invoices.Filters
{
    public partial class CurrencyFilterViewModel : ObservableObject
    {
        private readonly IInvoiceService _service;

        [ObservableProperty]
        private CurrencyOption selectedCurrency;

        public ObservableCollection<CurrencyOption> CurrencyOptions { get; } = [];

        public ObservableCollection<InvoiceResponse> InvoicesReference { get; set; }

        public CurrencyFilterViewModel(IInvoiceService service)
        {
            _service = service;
        }

        [RelayCommand]
        public async Task FilterByCurrency()
        {
            if (selectedCurrency is null) return;
            var filtered = await _service.GetInvoicesByCurrency(selectedCurrency.Currency_id);
            InvoicesReference.Clear();

            if (selectedCurrency.Currency_id is 0)
            {
                var response = await _service.LoadInvoices();
                if (response.Data is not null)
                    foreach (var currency in response.Data)
                        InvoicesReference.Add(currency);
                return;
            }

            if (filtered.Data is not null)
                foreach (var item in filtered.Data)
                    InvoicesReference.Add(item);
        }

        partial void OnSelectedCurrencyChanged(CurrencyOption value)
        {
            if (value is not null)
                FilterByCurrencyCommand.Execute(null);
        }

        public void LoadCurrencies(IEnumerable<CurrencyOption> currencies)
        {
            CurrencyOptions.Clear();
            CurrencyOptions.Add(new CurrencyOption { Currency_id = 0, Currency_code = "Todas" });
            foreach (var currency in currencies)
                CurrencyOptions.Add(currency);
        }
    }
}
