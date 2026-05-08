using PLECSYS_Studio.Services.GPS;
using static PLECSYS_Studio.Models.GPS.TrackingConfigModels;

namespace PLECSYS_Studio.Handlers.GPS
{
    public class TrackingConfigHandler
    {
        private readonly ITrackingConfigService _configService;

        public string SellerId { get; set; } = string.Empty;
        public int IntervalMinutes { get; set; } = 30;
        public string Message { get; set; } = string.Empty;
        public bool IsSuccess { get; set; }

        public TrackingConfigHandler(ITrackingConfigService configService)
        {
            _configService = configService;
        }

        public async Task UpdateConfigAsync()
        {
            if (string.IsNullOrWhiteSpace(SellerId))
            {
                Message = "El correo es requerido";
                IsSuccess = false;
                return;
            }

            if (IntervalMinutes < 1)
            {
                Message = "El intervalo debe ser al menos 1 minuto";
                IsSuccess = false;
                return;
            }

            var request = new UpdateTrackingConfigRequest
            {
                SellerId = SellerId,
                IntervalMinutes = IntervalMinutes
            };

            var result = await _configService.UpdateTrackingConfigAsync(request);

            Message = result.Message;
            IsSuccess = result.Success;
        }
    }
}
