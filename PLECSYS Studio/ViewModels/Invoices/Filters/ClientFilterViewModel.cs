using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PLECSYS_Studio.Services.Invoices;
using PLECSYS_Studio.ViewModels.Invoices.Filters.Options;
using PLECSYS_Studio.Wrappers.Invoices;
using System.Collections.ObjectModel;

namespace PLECSYS_Studio.ViewModels.Invoices.Filters
{
    public partial class ClientFilterViewModel : ObservableObject
    {
        private readonly IInvoiceService _service;

        [ObservableProperty]
        private ClientOption selectedClient;
        public ObservableCollection<ClientOption> ClientOptions { get; } = [];

        public ObservableCollection<InvoiceResponse> InvoicesReference { get; set; }

        public ClientFilterViewModel(IInvoiceService service)
        {
            _service = service;
        }

        [RelayCommand]
        public async Task FilterByClient()
        {
            if (selectedClient is null) return;

            if (string.IsNullOrEmpty(SelectedClient.Email))
            {
                var response = await _service.LoadInvoices();
                InvoicesReference.Clear();
                if (response.Data is not null)
                    foreach (var invoice in response.Data)
                        InvoicesReference.Add(invoice);
                return;
            }

            var filtered = await _service.GetInvoicesByClient(SelectedClient.Email);

            InvoicesReference.Clear();
            if (filtered.Data is not null)
                foreach (var invoice in filtered.Data)
                    InvoicesReference.Add(invoice);
        }

        partial void OnSelectedClientChanged(ClientOption value)
        {
            if (value is not null)
                FilterByClientCommand.Execute(null);
        }

        public void LoadClients(IEnumerable<ClientOption> clients)
        {
            ClientOptions.Clear();
            ClientOptions.Add(new ClientOption { Email = null, DisplayName = "Todos" });
            foreach (var client in clients)
                ClientOptions.Add(client);
        }
    }
}
