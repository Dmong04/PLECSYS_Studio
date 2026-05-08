using PLECSYS_Studio.ViewModels.SmartFlow;

namespace PLECSYS_PROTOTYPE_MAUI.Views.SmartFlow
{
    public partial class ListFormsPage : ContentPage
    {
        public ListFormsPage(ListFormsViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}