using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PLECSYS_Studio.Services.SaleOrderDetails;
using PLECSYS_Studio.Wrappers.Products;
using PLECSYS_Studio.Wrappers.SaleOrderDetails;
using System.Collections.ObjectModel;

namespace PLECSYS_Studio.ViewModels.SaleOrders.Options
{
    public partial class SaleOrderDetailViewModel : ObservableObject
    {
        private readonly ISaleOrderDetailService _service;

        [ObservableProperty]
        private int order_id;

        [ObservableProperty]
        private ProductResponse product;

        [ObservableProperty]
        private int quantity;

        public ObservableCollection<DetailsRequest> Pending_details { get; } = [];

        public SaleOrderDetailViewModel(ISaleOrderDetailService service)
        {
            _service = service;
        }

        [RelayCommand]
        private void AddDetail()
        {
            if (Order_id <= 0 || Product?.Product_id <= 0 || Quantity <= 0) return;

            var request = new DetailsRequest
            {
                Order_id = Order_id,
                Product_id = Product.Product_id,
                Quantity = Quantity,
                Product = Product
            };

            Pending_details.Add(request);
            OnPropertyChanged(nameof(Total));
            OnPropertyChanged(nameof(Subtotal));

            // Reset cantidad después de agregar
            Quantity = 0;
        }


        [RelayCommand]
        private void RemoveDetail(DetailsRequest detail)
        {
            if (Pending_details.Contains(detail))
                Pending_details.Remove(detail); OnPropertyChanged(nameof(Total)); OnPropertyChanged(nameof(Subtotal));
        }

        [RelayCommand]
        private async Task ConfirmOrder()
        {
            if (Pending_details.Count is 0) return;

            var details_list = Pending_details.ToList();
            var response = await _service.CreateSaleOrderDetails(details_list);
            if (response.Success)
                await Shell.Current.DisplayAlert("Éxito en agregar orden de compra",
                    "La orden de compra se ha realizado con éxito", "Aceptar");

            Pending_details.Clear();
            OnPropertyChanged(nameof(Total));
            OnPropertyChanged(nameof(Subtotal));
        }

        public string Product_name => Product.Product_name;
        public decimal Unit_price => Product.Unit_price;
        public decimal Subtotal => Pending_details.Sum(d => d.Subtotal);
        public decimal Total => Pending_details.Sum(d => d.Total);
    }
}
