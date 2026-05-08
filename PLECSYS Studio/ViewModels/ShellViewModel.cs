using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PLECSYS_Studio.Services.GPS;
using PLECSYS_Studio.Services.Users;

namespace PLECSYS_Studio.ViewModels
{
    public partial class ShellViewModel : ObservableObject
    {
        private readonly IUserService _service;
        private readonly LocationTrackingService _trackingService;

        public ShellViewModel(IUserService service, LocationTrackingService trackingService)
        {
            _service = service;
            _trackingService = trackingService;

        }

        [RelayCommand]
        public async Task Logout()
        {
            _trackingService.StopTracking();
            var mainWindow = Application.Current?.Windows.FirstOrDefault();
            await Shell.Current.DisplayAlert("Cerrando sesión", "Cierre de sesión exitoso", "Aceptar");
            if (mainWindow != null)
            {
                mainWindow.Page = new LoginShell();
            }
        }
    }
}
