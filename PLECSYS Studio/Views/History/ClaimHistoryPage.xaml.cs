using PLECSYS_Studio.ViewModels.History;
using PLECSYS_Studio.Views.Claims;

namespace PLECSYS_Studio.Views.History;

[QueryProperty(nameof(InvoiceId), "invoiceId")]
public partial class ClaimHistoryPage : ContentPage
{
    private readonly ClaimHistoryViewModel _viewModel;

    private int _invoiceId;
    public int InvoiceId
    {
        get => _invoiceId;
        set
        {
            _invoiceId = value;
            LoadData();
        }
    }

    public ClaimHistoryPage(ClaimHistoryViewModel vm)
    {
        InitializeComponent();
        BindingContext = _viewModel = vm;
    }

    private async void LoadData()
    {
        if (_invoiceId > 0)
        {
            await _viewModel.LoadClaims(_invoiceId);
        }
    }

    private async void OnRegisterClaimClicked(object sender, EventArgs e)
    {
        if (_invoiceId <= 0)
        {
            await DisplayAlert("Error", "Factura inválida", "OK");
            return;
        }

        await Shell.Current.GoToAsync($"{nameof(RegisterClaimPage)}?invoiceId={_invoiceId}");
    }
}