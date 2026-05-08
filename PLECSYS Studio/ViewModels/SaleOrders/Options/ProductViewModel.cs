using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PLECSYS_Studio.Services.Products;
using PLECSYS_Studio.Wrappers.Products;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PLECSYS_Studio.ViewModels.SaleOrders.Options
{
    public partial class ProductViewModel : ObservableObject
    {
        private readonly IProductService _service;

        [ObservableProperty]
        private string searchText;
        [ObservableProperty]
        private ProductResponse selectedProduct;

        public ObservableCollection<ProductResponse> Products { get; } = [];

        public ProductViewModel(IProductService service)
        {
            _service = service;
        }

        [RelayCommand]
        private async Task SearchProducts(string query)
        {
            if (string.IsNullOrWhiteSpace(query) || query.Length < 2)
            {
                Products.Clear();
                return;
            }

            var response = await _service.GetProductsByName(query);
            if (!response.Success || response.Data is null)
            {
                Products.Clear();
                return;
            }

            Products.Clear();
            foreach (var product in response.Data.Take(3))
                Products.Add(product);
        }

        [RelayCommand]
        private void SelectProduct(ProductResponse product)
        {
            if (product is null) return;

            SelectedProduct = product;
            SearchText = product.Product_name;
            Products.Clear();

            System.Diagnostics.Debug.WriteLine($"Producto seleccionado: {SelectedProduct.Product_name}");
        }

    }
}
