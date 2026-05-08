using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLECSYS_Studio.ViewModels
{
    public partial class HomePageViewModel : ObservableObject
    {
        [RelayCommand]
        public async Task GoToInvoicesList() => await Shell.Current.GoToAsync("/Invoices");

        [RelayCommand]
        public async Task GoToMap() => await Shell.Current.GoToAsync(nameof(Views.GPS.GPSPage));
    }
}
