using PLECSYS_Studio.ViewModels.SmartFlow;

namespace PLECSYS_Studio.Views.SmartFlow
{
    public partial class SmartFlowPage : ContentPage
    {
        public SmartFlowPage(SmartFlowViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
