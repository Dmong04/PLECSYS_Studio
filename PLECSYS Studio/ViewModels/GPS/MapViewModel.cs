using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MongoDB.Driver.Core.Servers;
using MongoDB.Driver.GeoJsonObjectModel;
using PLECSYS_Studio.Data.GPS;
using PLECSYS_Studio.Wrappers.GPS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLECSYS_Studio.ViewModels.GPS
{
    public partial class MapViewModel : ObservableObject
    {
        private readonly GPSData _gps;

        public MapViewModel(GPSData gps)
        {
            _gps = gps;
        }

        [ObservableProperty] private string seller_id;
        [ObservableProperty] private string start_location_name;
        [ObservableProperty] private double start_latitude;
        [ObservableProperty] private double start_longitude;
        [ObservableProperty] private string end_location_name;
        [ObservableProperty] private double end_latitude;
        [ObservableProperty] private double end_longitude;

        [ObservableProperty] private string statusMessage;
        [ObservableProperty] private bool isBusy;
        [ObservableProperty] private CoordinateResponse? route_result;

        [RelayCommand]
        private async Task SaveRoute()
        {
            try
            {
                IsBusy = true;
                StatusMessage = "Guardando la ruta de vendedor...";

                var request = new CoordinateRequest()
                {
                    Seller_id = Seller_id,
                    Start_location_name = Start_location_name,
                    Start_location = new LocationSeller { Coordinates = new[] { Start_longitude, Start_latitude } },
                    End_location_name = End_location_name,
                    End_location = new LocationSeller { Coordinates = new[] { End_longitude, End_latitude } }
                };

                var response = await _gps.SaveSellerRoute(request);

                if (!response.Success)
                {
                    StatusMessage = $"Error: {response.Message}";
                }
                else
                {
                    Route_result = response.Data;
                    StatusMessage = $"Ruta registrada: {Route_result?.Start_location_name} → {Route_result?.End_location_name}";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Excepción: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
