using PLECSYS_Studio.ViewModels.GPS;

namespace PLECSYS_Studio.Views.GPS;

public partial class TrackingConfigPage : ContentPage
{
    public TrackingConfigPage(TrackingConfigViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}