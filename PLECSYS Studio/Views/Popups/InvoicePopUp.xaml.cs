using CommunityToolkit.Maui.Views;
using PLECSYS_Studio.ViewModels;
using PLECSYS_Studio.Views.Payments;
using PLECSYS_Studio.Views.Claims;
using CommunityToolkit.Maui.Extensions;

namespace PLECSYS_Studio.Views.Popups;

public partial class InvoicePopUp : Popup
{
    public int Consecutive { get; set; }
    public decimal PendingBalance { get; set; }

    public InvoicePopUp(SingleInvoiceViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        BackgroundColor = Colors.Transparent;

#if ANDROID
        if (Handler?.PlatformView is Android.Views.View view)
        {
            view.Background = null;
        }
#endif
    }

    private async void OnRegisterPaymentTapped(object sender, TappedEventArgs e)
    {
        try
        {
            await Shell.Current.ClosePopupAsync();

            await Shell.Current.GoToAsync(
                $"{nameof(RegisterPaymentPage)}?invoiceConsecutive={Consecutive}&pendingBalance={PendingBalance}");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error",
                $"No se pudo abrir la página de registro de pagos: {ex.Message}", "Ok");
        }
    }

    private async void OnRegisterClaimTapped(object sender, TappedEventArgs e)
    {
        try
        {
            await Shell.Current.ClosePopupAsync();

            await Shell.Current.GoToAsync(
                $"{nameof(RegisterClaimPage)}?invoiceConsecutive={Consecutive}");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error",
                $"No se pudo abrir la página de registro de reclamo: {ex.Message}", "Ok");
        }
    }
}