using PLECSYS_Studio.ViewModels;

namespace PLECSYS_Studio.Views;

public partial class HomePage : ContentPage
{
    private readonly HomePageViewModel _vm;

    public HomePage(HomePageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        _vm = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        // Carga automática con la fecha actual al entrar a la página
        await _vm.LoadExpiryInvoicesCommand.ExecuteAsync(DateTime.Today);
    }

    private async void OnDateSelected(object sender, DateChangedEventArgs e)
    {
        await _vm.LoadExpiryInvoicesCommand.ExecuteAsync(e.NewDate);
    }

    private void OnTodayArrowTapped(object sender, EventArgs e)
    {
        TodayContent.IsVisible = !TodayContent.IsVisible;
        TodayArrow.Source = TodayContent.IsVisible ? "collapse.png" : "expand.png";
    }

    private void OnWeekArrowTapped(object sender, EventArgs e)
    {
        WeekContent.IsVisible = !WeekContent.IsVisible;
        WeekArrow.Source = WeekContent.IsVisible ? "collapse.png" : "expand.png";
    }

    private void OnMonthArrowTapped(object sender, EventArgs e)
    {
        MonthContent.IsVisible = !MonthContent.IsVisible;
        MonthArrow.Source = MonthContent.IsVisible ? "collapse.png" : "expand.png";
    }

    private void OnOpenMenuClicked(object sender, EventArgs e)
    {
        if (Shell.Current != null)
            Shell.Current.FlyoutIsPresented = !Shell.Current.FlyoutIsPresented;
    }
}
