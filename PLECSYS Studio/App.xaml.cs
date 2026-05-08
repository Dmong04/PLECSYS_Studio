using PLECSYS_Studio.Services.Users;
using PLECSYS_Studio.ViewModels;

namespace PLECSYS_Studio
{
    public partial class App : Application
    {
        private readonly IUserService loginService;

        private readonly ShellViewModel shellViewModel;

        public App(IUserService _service, ShellViewModel _shell)
        {
            InitializeComponent();
            loginService = _service;
            shellViewModel = _shell;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            Page startPage = loginService.IsAuthenticated
                ? new AppShell(shellViewModel)
                : new LoginShell();

            return new Window(startPage);
        }
    }
}
