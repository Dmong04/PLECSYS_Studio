using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PLECSYS_Studio.Services.Users;
using PLECSYS_Studio.Wrappers.Users;
using System.Collections.ObjectModel;
using System.Threading;

namespace PLECSYS_Studio.ViewModels.SaleOrders.Options
{
    public partial class ClientViewModel : ObservableObject
    {
        private readonly IUserService _service;

        private CancellationTokenSource? _cts;

        [ObservableProperty]
        private string searchText;

        [ObservableProperty]
        private string? selectedClientId;

        [ObservableProperty]
        private bool isBusy;

        public ObservableCollection<UserResponse> Clients { get; } = [];

        public ClientViewModel(IUserService service)
        {
            _service = service;
        }

        [RelayCommand]
        private async Task SearchClients(string query)
        {
            System.Diagnostics.Debug.WriteLine($"SearchClients ejecutado con: {query}");

            if (string.IsNullOrWhiteSpace(query) || query.Length < 2)
            {
                Clients.Clear();
                return;
            }

            var response = await _service.GetUsersByName(query);
            if (!response.Success || response.Data is null)
            {
                Clients.Clear();
                return;
            }

            Clients.Clear();
            foreach (var client in response.Data.Take(5))
                Clients.Add(client);
        }

        [RelayCommand]
        private void SelectClient(UserResponse client)
        {
            if (client is null) return;
            SelectedClientId = client.Email;
            SearchText = client.Full_name;
            Clients.Clear();
            System.Diagnostics.Debug.WriteLine($"Cliente seleccionado: {SelectedClientId}");
        }
    }
}
