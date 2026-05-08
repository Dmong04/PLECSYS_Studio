using PLECSYS_Studio.ViewModels.Support;

namespace PLECSYS_Studio.Views.Support
{
    public partial class SupportPage : ContentPage
    {
        public SupportPage(SupporViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}