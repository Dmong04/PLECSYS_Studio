using PLECSYS_PROTOTYPE_MAUI.ViewModels.CxC;

namespace PLECSYS_PROTOTYPE_MAUI.Views.CxC
{
    public partial class ListByClientPage : ContentPage
    {
        public ListByClientPage(ListByClientViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}