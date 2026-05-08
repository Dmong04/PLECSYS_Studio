using PLECSYS_Studio.ViewModels.SmartFlow;

namespace PLECSYS_PROTOTYPE_MAUI.Views.SmartFlow
{
    public partial class MainSmartFlowPage : ContentPage
    {
        public MainSmartFlowPage(MainSmartFlowViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}