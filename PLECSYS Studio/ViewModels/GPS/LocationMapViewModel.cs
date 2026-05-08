using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PLECSYS_Studio.Models.GPS;
using PLECSYS_Studio.Services.GPS;
using PLECSYS_Studio.Services.Users;

namespace PLECSYS_Studio.ViewModels.GPS
{
    public partial class LocationMapViewModel : ObservableObject
    {
        private readonly ILocationService _locationService;
        private readonly IUserService _userService;

        public LocationMapViewModel(ILocationService locationService, IUserService userService)
        {
            _locationService = locationService;
            _userService = userService;
        }

        [ObservableProperty]
        private double latitude;

        [ObservableProperty]
        private double longitude;

        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private string statusMessage;


        [RelayCommand]
        private async Task GetCurrentLocation()
        {
            try
            {
                IsBusy = true;
                StatusMessage = "Getting current location...";

                var request = new GeolocationRequest(
                    GeolocationAccuracy.Best,
                    TimeSpan.FromSeconds(10));

                var location = await Geolocation.Default.GetLocationAsync(request);

                if (location != null)
                {
                    Latitude = location.Latitude;
                    Longitude = location.Longitude;

                    StatusMessage = $"Location detected: {Latitude}, {Longitude}";
                }
                else
                {
                    StatusMessage = "Unable to get location.";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Location error: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }


        [RelayCommand]
        private async Task SaveLocation()
        {
            try
            {
                IsBusy = true;
                StatusMessage = "Requesting permission...";

                //Permissions on android 13+
                var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

                if (status != PermissionStatus.Granted)
                {
                    StatusMessage = "Location permission denied.";
                    return;
                }

                StatusMessage = "Getting GPS location...";

                var request = new GeolocationRequest(
                    GeolocationAccuracy.Best,
                    TimeSpan.FromSeconds(10));

                var location = await Geolocation.Default.GetLocationAsync(request);

                if (location == null)
                {
                    StatusMessage = "Unable to get GPS location.";
                    return;
                }

                Latitude = location.Latitude;
                Longitude = location.Longitude;

                StatusMessage = "Saving location...";

                var requestDto = new SaveLocationRequest
                {
                    SellerId = _userService.CurrentUserEmail,
                    Latitude = Latitude,
                    Longitude = Longitude
                };

                var response = await _locationService.SaveLocation(requestDto);

                StatusMessage = response.Success
                    ? "Location saved successfully"
                    : $"Error: {response.Message}";
            }
            catch (Exception ex)
            {
                StatusMessage = ex.Message;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
