using PLECSYS_Studio.ViewModels.SaleOrders;
using PLECSYS_Studio.ViewModels.SaleOrders.Options;

namespace PLECSYS_Studio.Views.SaleOrders;

public partial class SaleOrderPage : ContentPage
{
    public SaleOrderPage(
        SaleOrderViewModel saleOrderVM,
        ClientViewModel clientVM,
        ProductViewModel productVM,
        SaleOrderDetailViewModel detailVM)
    {
        InitializeComponent();

        // Contexto principal para cabecera de la orden
        BindingContext = saleOrderVM;

        // Contextos locales para cada sección
        ClientSection.BindingContext = clientVM;
        ProductSection.BindingContext = productVM;
        DetailSection.BindingContext = detailVM;

        // Propagar Order_id al VM de detalles
        detailVM.Order_id = saleOrderVM.Order_id;
        saleOrderVM.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(SaleOrderViewModel.Order_id))
                detailVM.Order_id = saleOrderVM.Order_id;
        };

        // Propagar cliente seleccionado a la orden
        clientVM.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(ClientViewModel.SelectedClientId))
                saleOrderVM.Client_id = clientVM.SelectedClientId!;
        };

        // Propagar producto seleccionado al detalle
        productVM.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(ProductViewModel.SelectedProduct))
                detailVM.Product = productVM.SelectedProduct;
        };
    }
}
