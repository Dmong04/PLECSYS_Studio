using PLECSYS_Studio.ViewModels.Reports;

namespace PLECSYS_PROTOTYPE_MAUI.Views.Reports
{
    public partial class SalesReportPage : ContentPage
    {
        public SalesReportPage(SalesReportViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}