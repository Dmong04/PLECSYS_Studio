using PLECSYS_Studio.ViewModels;

namespace PLECSYS_Studio.Views.SignUp;

public partial class SignUpPage : ContentPage
{
	public SignUpPage(SignUpViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}