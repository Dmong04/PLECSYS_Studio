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
}