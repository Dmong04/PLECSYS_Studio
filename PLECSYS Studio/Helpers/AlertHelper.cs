using Microsoft.Maui.Controls;
using System.Threading.Tasks;

namespace PLECSYS_Studio.Helpers
{
    public static class AlertHelper
    {
        public static async Task ShowComingSoonAsync(string featureName = "")
        {
           var page = Application.Current?.Windows.FirstOrDefault()?.Page;

            if (page == null)
                return; 
            
            await page.DisplayAlert(" En desarrollo",
                $"La opción {featureName} aún no está disponible.",
                "Aceptar");     
        }

    }
}