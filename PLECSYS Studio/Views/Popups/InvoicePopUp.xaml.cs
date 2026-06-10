using CommunityToolkit.Maui.Views;
using PLECSYS_Studio.ViewModels;

namespace PLECSYS_Studio.Views.Popups;

public partial class InvoicePopUp : Popup
{
    public int Consecutive { get; set; }
    public decimal PendingBalance { get; set; }
    private readonly SingleInvoiceViewModel _vm;

    public InvoicePopUp(SingleInvoiceViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        BackgroundColor = Colors.Transparent;
        _vm = vm;
        vm.SetPopup(this);

#if ANDROID
        if (Handler?.PlatformView is Android.Views.View view)
        {
            view.Background = null;
        }
#endif
    }

    public async void OnPaymentTapped(object sender, EventArgs e)
    {
        await CloseAsync();
        await Shell.Current.GoToAsync($"PaymentHistoryPage?invoiceId={_vm.Invoice_id}");
    }

    public async void OnClaimTapped(object sender, EventArgs e)
    {
        await CloseAsync();
        await Shell.Current.GoToAsync($"ClaimHistoryPage?invoiceId={_vm.Invoice_id}");
    }

    public async void OnHistoryTapped(object sender, EventArgs e)
    {
        await CloseAsync();
        await Shell.Current.GoToAsync($"InvoiceHistoryPage?invoiceConsecutive={_vm.Consecutive}");
    }

    public async void OnCloseTapped(object sender, EventArgs e)
    {
        await CloseAsync();
    }
}