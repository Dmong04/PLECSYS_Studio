using PLECSYS_Studio.ViewModels.History;
using PLECSYS_Studio.Views.Payments;

namespace PLECSYS_Studio.Views.History;

[QueryProperty(nameof(InvoiceId), "invoiceId")]
[QueryProperty(nameof(InvoiceConsecutive), "invoiceConsecutive")]
public partial class PaymentHistoryPage : ContentPage
{
    private readonly PaymentHistoryViewModel _viewModel;

    private int _invoiceId;
    public int InvoiceId
    {
        get => _invoiceId;
        set { _invoiceId = value; LoadData(); }
    }

    private int _invoiceConsecutive;
    public int InvoiceConsecutive
    {
        get => _invoiceConsecutive;
        set { _invoiceConsecutive = value; }
    }

    public PaymentHistoryPage(PaymentHistoryViewModel vm)
    {
        InitializeComponent();
        BindingContext = _viewModel = vm;
    }

    private async void LoadData()
    {
        if (_invoiceId > 0)
            await _viewModel.LoadPayments(_invoiceId);
    }

    private async void OnRegisterPaymentClicked(object sender, EventArgs e)
    {
        if (_invoiceId <= 0)
        {
            await DisplayAlert("Error", "Factura inválida", "OK");
            return;
        }

        await Shell.Current.GoToAsync(
            $"{nameof(RegisterPaymentPage)}?invoiceId={_invoiceId}&invoiceConsecutive={_invoiceConsecutive}");
    }
}