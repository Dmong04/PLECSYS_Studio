using PLECSYS_PROTOTYPE_MAUI.ViewModels.CxC;

namespace PLECSYS_PROTOTYPE_MAUI.Views.CxC
{
    public partial class CxCReportPage : ContentPage
    {
        public CxCReportPage(CxCReportViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}