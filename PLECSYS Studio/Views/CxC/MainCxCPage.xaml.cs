using PLECSYS_PROTOTYPE_MAUI.ViewModels.CxC;

namespace PLECSYS_PROTOTYPE_MAUI.Views.CxC
{
    public partial class MainCxCPage : ContentPage
    {
        public MainCxCPage(MainCxCViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}