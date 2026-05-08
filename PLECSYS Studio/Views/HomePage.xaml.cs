using PLECSYS_Studio.ViewModels;

namespace PLECSYS_Studio.Views;

public partial class HomePage : ContentPage
{
	public HomePage(HomePageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
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
    private void OnDateSelected(object sender, DateChangedEventArgs e)
    {
        DateTime selected_date = e.NewDate;
        Console.WriteLine($"Fecha seleccionada: {selected_date:dd/MM/yyyy}");
    }
}