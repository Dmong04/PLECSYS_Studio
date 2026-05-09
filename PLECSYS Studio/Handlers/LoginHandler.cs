using CommunityToolkit.Maui.Extensions;
using PLECSYS_Studio.Helpers;
using PLECSYS_Studio.Services;
using PLECSYS_Studio.Services.GPS;
using PLECSYS_Studio.Services.Users;
using PLECSYS_Studio.ViewModels;
using PLECSYS_Studio.Views.Popups;

namespace PLECSYS_Studio.Handlers
{
    public class LoginHandler
    {

        private readonly IUserService _service;

        private readonly ShellViewModel _shellViewModel;

        private readonly LocationTrackingService _trackingService;

        private readonly SessionService _sessionService;

        public required string Email { get; set; }

        public required string Password { get; set; }

        public LoginHandler(IUserService service, ShellViewModel shellViewModel, LocationTrackingService trackingService, SessionService sessionService)
        {
            _service = service;
            _shellViewModel = shellViewModel;
            _trackingService = trackingService;
            _sessionService = sessionService;
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
                var login = await _service.Login(Email, Password);
                if (!login.Success)
                {
                    await Shell.Current.DisplayAlert("Inicio de sesión fallido",
                        $"No se pudo ingresar al sistema: {login.Message}",
                        "Aceptar");
                    return;
                }

                // Si el usuario solo tiene una compañía, la seleccionamos automáticamente
                if (login.Data?.linked_companies?.Count == 1)
                {
                    var only = login.Data.linked_companies.First();
                    _sessionService.SaveSession(login.Data, only.company_id, only.company_name);
                }
                else if (login.Data?.linked_companies?.Count > 1)
                {
                    bool selected = false;

                    while (!selected)
                    {
                        var popup = new CompanySelectionPopup(login.Data.linked_companies, _sessionService);
                        await Shell.Current.ShowPopupAsync(popup);

                        selected = _sessionService.HasCompany();

                        if (!selected)
                        {
                            await Shell.Current.DisplayAlert(
                                "Selección requerida",
                                "Debes seleccionar una compañía para continuar.",
                                "OK");
                        }
                    }

                    _sessionService.SaveSession(login.Data,
                        _sessionService.GetCompanyId(),
                        _sessionService.GetCompanyName());
                }
                await Shell.Current.DisplayAlert($"Bienvenido, {login.Data?.email}",
                    "Inicio de sesión exitoso", "Aceptar");
                SessionManager.SwitchtoAppShell(_shellViewModel);
                await _trackingService.StartTracking();
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
