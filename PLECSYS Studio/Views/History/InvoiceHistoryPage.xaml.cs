using PLECSYS_Studio.ViewModels.History;

namespace PLECSYS_Studio.Views.History
{
    public partial class InvoiceHistoryPage : ContentPage
    {
        private readonly InvoiceHistoryViewModel _vm;

        public InvoiceHistoryPage(InvoiceHistoryViewModel vm)
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
