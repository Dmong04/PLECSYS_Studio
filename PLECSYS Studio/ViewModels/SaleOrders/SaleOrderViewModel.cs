using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PLECSYS_Studio.Services.SaleOrders;
using PLECSYS_Studio.Wrappers.SaleOrders;
using System.Collections.ObjectModel;

namespace PLECSYS_Studio.ViewModels.SaleOrders
{
    public partial class SaleOrderViewModel : ObservableObject
    {
        private readonly ISaleOrderService _service;

        [ObservableProperty]
        private int order_id;

        [ObservableProperty]
        private string client_id;

        [ObservableProperty]
        private bool isConfirmed;

        public ObservableCollection<SaleOrderResponse> Orders { get; } = [];

        public SaleOrderViewModel(ISaleOrderService service)
        {
            _service = service;
        }

        [RelayCommand]
        private async Task CreateOrder()
        {
            if (string.IsNullOrEmpty(Client_id))
            {
                await Shell.Current.DisplayAlert("Error", "Debe seleccionar un cliente", "Aceptar");
                return;
            }

            var request = new SaleOrderRequest()
            {
                Client_id = Client_id
            };

            var response = await _service.CreateSaleOrder(request);
            if (response.Success && response.Data is not null)
            {
                Order_id = response.Data.Order_id;
                Orders.Add(response.Data);

                await Shell.Current.DisplayAlert("Orden creada",
                    $"Orden #{Order_id} creada para {Client_id}", "Aceptar");
            }
            await Shell.Current.DisplayAlert("Error",
                    response.Message ?? "No se pudo crear la orden", "Aceptar");
        }
    }
}
