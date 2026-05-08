using PLECSYS_Studio.ViewModels.Claims;

namespace PLECSYS_Studio.Views.Claims
{
    public partial class RegisterClaimPage : ContentPage
    {
        public RegisterClaimPage(RegisterClaimViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}