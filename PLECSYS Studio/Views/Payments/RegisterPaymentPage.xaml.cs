using PLECSYS_Studio.ViewModels.PaymentRecords;

namespace PLECSYS_Studio.Views.Payments
{
    public partial class RegisterPaymentPage : ContentPage, IQueryAttributable
    {
        private readonly RegisterPaymentViewModel _vm;

        public RegisterPaymentPage(RegisterPaymentViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
            _vm = vm;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            _vm.ApplyQueryAttributes(query);
        }

        protected override void OnNavigatedTo(NavigatedToEventArgs args)
        {
            base.OnNavigatedTo(args);
            _ = _vm.InitializeAsync();
        }
    }
}