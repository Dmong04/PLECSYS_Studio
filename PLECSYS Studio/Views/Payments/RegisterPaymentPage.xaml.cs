using PLECSYS_Studio.ViewModels;
using PLECSYS_Studio.ViewModels.Payments;

namespace PLECSYS_Studio.Views.Payments
{
    public partial class RegisterPaymentPage : ContentPage
    {
        private readonly RegisterPaymentViewModel _vm;

        public RegisterPaymentPage(RegisterPaymentViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
            _vm = vm;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _vm.LoadCommand.ExecuteAsync(null);
        }
    }
}