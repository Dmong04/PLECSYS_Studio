using PLECSYS_Studio.ViewModels.Reports;

namespace PLECSYS_PROTOTYPE_MAUI.Views.Reports
{ 
    public partial class IVAReportPage : ContentPage
    {
        public IVAReportPage(IVAReportViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}