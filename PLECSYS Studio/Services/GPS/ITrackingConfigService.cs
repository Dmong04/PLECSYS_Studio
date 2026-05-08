using PLECSYS_Studio.Models.GPS;
using PLECSYS_Studio.Wrappers;

namespace PLECSYS_Studio.Services.GPS
{
    public interface ITrackingConfigService
    {
        Task<APIResponse<TrackingConfigModels.TrackingConfigResponse>> GetTrackingConfigAsync(string sellerId);
        Task<APIResponse<TrackingConfigModels.TrackingConfigResponse>> UpdateTrackingConfigAsync(TrackingConfigModels.UpdateTrackingConfigRequest request);
    }
}
