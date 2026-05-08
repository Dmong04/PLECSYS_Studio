using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PLECSYS_Studio.Helpers;
using PLECSYS_Studio.Services.Users;
using PLECSYS_Studio.Wrappers.Users;

namespace PLECSYS_Studio.ViewModels
{
    public partial class SignUpViewModel : ObservableObject
    {
        private readonly IUserService _service;

        [ObservableProperty] private string email = string.Empty;
        [ObservableProperty] private string name = string.Empty;
        [ObservableProperty] private string first_lastname = string.Empty;
        [ObservableProperty] private string second_lastname = string.Empty;
        [ObservableProperty] private string phoneCode = "+506";
        [ObservableProperty] private string phone = string.Empty;
        [ObservableProperty] private string password = string.Empty;
        [ObservableProperty] private string confirmPassword = string.Empty;
        [ObservableProperty] private bool termsAccepted;
        [ObservableProperty] private string statusMessage = string.Empty;
        [ObservableProperty] private bool isBusy;
        [ObservableProperty] private string recaptchaToken = string.Empty;

        public SignUpViewModel(IUserService service)
        {
            _service = service;
        }

        [RelayCommand]
        public async Task SignUp()
        {
            if (IsBusy) return;
            IsBusy = true;

            try
            {
                if (!TermsAccepted)
                {
                    await Shell.Current.DisplayAlert("Aviso", "Debes aceptar los términos y condiciones.", "OK");
                    return;
                }

                if (Password != ConfirmPassword)
                {
                    await Shell.Current.DisplayAlert("Error", "Las contraseñas no coinciden.", "OK");
                    return;
                }

                // ReCatpha pendiente y emulado
                if (!await VerifyRecaptchaAsync())
                {
                    await Shell.Current.DisplayAlert("Error", "Por favor completa el reCAPTCHA.", "OK");
                    return;
                }

                var request = new SignUpRequest()
                {
                    Email = Email,
                    Name = Name,
                    First_lastname = First_lastname,
                    Second_lastname = Second_lastname,
                    Phone = $"{PhoneCode} {Phone}",
                    Password = Password
                };

                var response = await _service.SignUp(request);
                if (!response.Success)
                {
                    await Shell.Current.DisplayAlert("Error al registrarse", 
                        $"No se ha podido registrar al usuario: {response.Message}", "Aceptar");
                    return;
                }

                await Shell.Current.DisplayAlert("Registro exitoso", 
                    $"Usuario registrado exitosamente para {response.Data?.Email}", "Aceptar");
                await Shell.Current.GoToAsync("//LoginPage");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error al registrarse",
                        $"Ha ocurrido un error en el proceso: {ex.Message}", "Aceptar");
                return;
            } finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        public async Task GoToLogin()
        {
            if (IsBusy) return;
            IsBusy = true;
            try
            {
                await Shell.Current.GoToAsync("//LoginPage");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task<bool> VerifyRecaptchaAsync()
        {
            //Emulado y debe cambiarse
            await Task.Delay(500);
            return true;
        }
    }
}
