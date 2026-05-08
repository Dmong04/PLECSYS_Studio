using System.Text.Json.Serialization;

namespace PLECSYS_Studio.Models.GPS
{
    public class TrackingConfigModels
    {
        public class GetTrackingConfigRequest
        {
            [JsonPropertyName("sellerId")]
            public string SellerId { get; set; } = string.Empty;
        }

        public class UpdateTrackingConfigRequest  
        {
            [JsonPropertyName("sellerId")]
            public string SellerId { get; set; } = string.Empty;
            [JsonPropertyName("intervalMinutes")]
            public int IntervalMinutes { get; set; }
        }

        public class TrackingConfigResponse
        {
            [JsonPropertyName("sellerId")]
            public string SellerId { get; set; } = string.Empty;
            [JsonPropertyName("intervalMinutes")]
            public int IntervalMinutes { get; set; }
        }
    }
}
