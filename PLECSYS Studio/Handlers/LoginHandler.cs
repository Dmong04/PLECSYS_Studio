using PLECSYS_Studio.Helpers;
using PLECSYS_Studio.Services.GPS;
using PLECSYS_Studio.Services.Users;
using PLECSYS_Studio.ViewModels;

namespace PLECSYS_Studio.Handlers
{
    public class LoginHandler
    {

        private readonly IUserService _service;

        private readonly ShellViewModel _shellViewModel;

        private readonly LocationTrackingService _trackingService;

        public required string Email { get; set; }

        public required string Password { get; set; }

        public LoginHandler(IUserService service, ShellViewModel shellViewModel, LocationTrackingService trackingService)
        {
            _service = service;
            _shellViewModel = shellViewModel;
            _trackingService = trackingService;
        }

        public async Task LoginWithoutAPI()
        {
            if (Email == "admin" && Password == "admin")
            {
                await Shell.Current.DisplayAlert($"Bienvenido {Email}",
                    "Inicio de sesión exitoso", "Aceptar");
            }
        }

        public async Task Login()
        {
            try
            {
                var login = await _service.MockLogin(Email, Password);
                if (!login.Success)
                {
                    await Shell.Current.DisplayAlert("Inicio de sesión fallido",
                        $"No se pudo ingresar al sistema: {login.Message}",
                        "Aceptar");
                    return;
                }
                await Shell.Current.DisplayAlert($"Bienvenido, {login.Data?.Name} {login.Data?.First_lastname}",
                    "Inicio de sesión exitoso", "Aceptar");
                await _trackingService.StartTracking();
                SessionManager.SwitchtoAppShell(_shellViewModel);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error de servidor",
                    $"Ha ocurrido un problema al iniciar sesión: {ex.Message}",
                    "Cerrar");
            }
        }
    }
}
