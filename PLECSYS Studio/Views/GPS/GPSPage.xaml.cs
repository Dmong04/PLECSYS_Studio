using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using PLECSYS_Studio.ViewModels.GPS;

namespace PLECSYS_Studio.Views.GPS;

public partial class GPSPage : ContentPage
{
    public int clickCount = 0;

    public GPSPage(MapViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    private async void OnMapClicked(object sender, MapClickedEventArgs e)
    {
        if (BindingContext is MapViewModel vm)
        {
            clickCount++;

            if (clickCount == 1)
            {
                vm.Start_latitude = e.Location.Latitude;
                vm.Start_longitude = e.Location.Longitude;

                var placemarks = await Geocoding.Default.GetPlacemarksAsync(e.Location);
                vm.Start_location_name = placemarks?.FirstOrDefault()?.Thoroughfare ?? "Ubicación de inicio";

                vm.StatusMessage = $"Inicio marcado: {vm.Start_latitude}, {vm.Start_longitude}";
            }
            else if (clickCount == 2)
            {
                vm.End_latitude = e.Location.Latitude;
                vm.End_longitude = e.Location.Longitude;

                var placemarks = await Geocoding.Default.GetPlacemarksAsync(e.Location);
                vm.End_location_name = placemarks?.FirstOrDefault()?.Thoroughfare ?? "Ubicación de cierre";

                vm.StatusMessage = $"Fin del marcado: {vm.End_latitude}, {vm.End_longitude}";
                clickCount = 0;
            }
        }
    }
}