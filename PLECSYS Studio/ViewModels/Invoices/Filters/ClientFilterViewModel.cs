using CommunityToolkit.Mvvm.ComponentModel;
using PLECSYS_Studio.ViewModels.Invoices.Filters.Options;
using System.Collections.ObjectModel;

namespace PLECSYS_Studio.ViewModels.Invoices.Filters
{
    public partial class ClientFilterViewModel : ObservableObject
    {
        [ObservableProperty]
        private ClientOption selectedClient;

        public ObservableCollection<ClientOption> ClientOptions { get; } = [];

        public void LoadClients(IEnumerable<ClientOption> clients)
        {
            ClientOptions.Clear();
            ClientOptions.Add(new ClientOption { CompanyId = null, DisplayName = "Todos" });
            foreach (var client in clients)
                ClientOptions.Add(client);
        }
    }
}