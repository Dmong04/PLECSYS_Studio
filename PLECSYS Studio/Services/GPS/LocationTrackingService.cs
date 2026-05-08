using PLECSYS_Studio.Services.Users;
using PLECSYS_Studio.Models.GPS;

namespace PLECSYS_Studio.Services.GPS
{
    public class LocationTrackingService
    {
        private readonly ILocationService _locationService;
        private readonly IUserService _userService;
        private readonly ITrackingConfigService _configService;
        private CancellationTokenSource? _cts;
        private int _intervalMinutes = 30;

        public LocationTrackingService(
            ILocationService locationService,
            IUserService userService,
            ITrackingConfigService configService)
        {
            _locationService = locationService;
            _userService = userService;
            _configService = configService;
        }

        public async Task StartTracking() 
        {
            if (!_userService.IsAuthenticated)
                return;

            await LoadIntervalConfigAsync(); 

            _cts = new CancellationTokenSource();

            _ = Task.Run(async () =>
            {
                int cycleCount = 0;

                while (!_cts.Token.IsCancellationRequested)
                {
                    if (_userService.IsAuthenticated)
                        await SaveLocationAsync();

                    
                    if (++cycleCount % 10 == 0)
                        await LoadIntervalConfigAsync();

                    await Task.Delay(
                        TimeSpan.FromMinutes(_intervalMinutes),
                        _cts.Token);
                }
            }, _cts.Token);
        }

        public void StopTracking()
        {
            _cts?.Cancel();
        }

        private async Task LoadIntervalConfigAsync()
        {
            var config = await _configService
                .GetTrackingConfigAsync(_userService.CurrentUserEmail);

            if (config.Success && config.Data is not null)
                _intervalMinutes = config.Data.IntervalMinutes;
        }

        private async Task SaveLocationAsync()
        {
            try
            {
                var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

                if (status != PermissionStatus.Granted)
                    return;

                var request = new GeolocationRequest(
                    GeolocationAccuracy.Best,
                    TimeSpan.FromSeconds(10));

                var location = await Geolocation.Default.GetLocationAsync(request);

                if (location == null)
                    return;

                var requestDto = new SaveLocationRequest
                {
                    SellerId = _userService.CurrentUserEmail,
                    Latitude = location.Latitude,
                    Longitude = location.Longitude
                };

                await _locationService.SaveLocation(requestDto);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Tracking error: {ex.Message}");
            }
        }
    }
}
