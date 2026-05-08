using PLECSYS_Studio.ViewModels.Reports;

namespace PLECSYS_Studio.Views.Reports
{
    public partial class MainReportPage : ContentPage
    {
        public MainReportPage(MainReportPage vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}