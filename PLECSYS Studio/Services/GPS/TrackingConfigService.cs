using PLECSYS_Studio.Wrappers;
using System.Net.Http.Json;
using static PLECSYS_Studio.Models.GPS.TrackingConfigModels;

namespace PLECSYS_Studio.Services.GPS
{
    public class TrackingConfigService : ITrackingConfigService
    {
        private readonly HttpClient _http;

        public TrackingConfigService(IHttpClientFactory httpClientFactory)
        {
            _http = httpClientFactory.CreateClient("PLECSYS"); 
        }

        public async Task<APIResponse<TrackingConfigResponse>> GetTrackingConfigAsync(string sellerId)
        {
            try
            {
                var response = await _http.GetFromJsonAsync<APIResponse<TrackingConfigResponse>>(
                    $"gps/seller/tracking-config?SellerId={sellerId}");

                return response ?? new APIResponse<TrackingConfigResponse>
                {
                    Success = false,
                    Message = "Respuesta vacía del servidor"
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<TrackingConfigResponse>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }


        public async Task<APIResponse<TrackingConfigResponse>> UpdateTrackingConfigAsync(
            UpdateTrackingConfigRequest request)
        {
            try
            {
                var response = await _http.PutAsJsonAsync(
                    "gps/seller/tracking-config", request);

                var result = await response.Content
                    .ReadFromJsonAsync<APIResponse<TrackingConfigResponse>>();

                return result ?? new APIResponse<TrackingConfigResponse>
                {
                    Success = false,
                    Message = "Respuesta vacía del servidor"
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<TrackingConfigResponse>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
