using PLECSYS_Studio.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLECSYS_Studio.Helpers
{
    public static class SessionManager
    {
        public static void SwitchtoAppShell(ShellViewModel shellViewModel)
        {
            Application.Current.MainPage = new AppShell(shellViewModel);
        }

        public static void SwitchtoLoginShell()
        {
            Application.Current.MainPage = new LoginShell();
        }
    }
}
