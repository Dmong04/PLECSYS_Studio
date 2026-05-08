using PLECSYS_Studio.Helpers;
using PLECSYS_Studio.ViewModels;

namespace PLECSYS_Studio;

public partial class AppShell : Shell
{
	public AppShell(ShellViewModel vm)
	{
        InitializeComponent();
		BindingContext = vm;
	}

	public async void OnMenuItemComingSoon(object sender, EventArgs e)
	{
		await AlertHelper.ShowComingSoonAsync("Esta sección");
	}
}