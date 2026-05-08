
using PLECSYS_Studio.ViewModels.Invoices;
using System.Threading.Tasks;

namespace PLECSYS_Studio.Views.Invoices
{
    public partial class InvoicesPage : ContentPage
    {
        private readonly InvoicesViewModel viewModel;

        public InvoicesPage(InvoicesViewModel vm)
        {
            InitializeComponent();
            viewModel = vm;
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await viewModel.LoadInvoicesCommand.ExecuteAsync(null);
        }
    }
}