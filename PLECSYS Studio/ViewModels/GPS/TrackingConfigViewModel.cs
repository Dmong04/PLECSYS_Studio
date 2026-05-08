using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PLECSYS_Studio.Handlers.GPS;

namespace PLECSYS_Studio.ViewModels.GPS
{
    public partial class TrackingConfigViewModel : ObservableObject  
    {
        private readonly TrackingConfigHandler _handler;

        [ObservableProperty]
        private string sellerId = string.Empty;

        [ObservableProperty]
        private int intervalMinutes = 30;

        [ObservableProperty]
        private string message = string.Empty;

        [ObservableProperty]
        private bool isSuccess;

        [ObservableProperty]
        private bool isBusy;

        public TrackingConfigViewModel(TrackingConfigHandler handler)
        {
            _handler = handler;
        }

        [RelayCommand]
        public async Task UpdateConfigAsync()
        {
            IsBusy = true;
            Message = string.Empty;

            _handler.SellerId = SellerId;
            _handler.IntervalMinutes = IntervalMinutes;

            await _handler.UpdateConfigAsync();

            Message = _handler.Message;
            IsSuccess = _handler.IsSuccess;
            IsBusy = false;
        }
    }
}
