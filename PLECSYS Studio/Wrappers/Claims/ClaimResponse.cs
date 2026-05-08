using System.Text.Json.Serialization;

namespace PLECSYS_Studio.Wrappers.Claims
{
    public class ClaimResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        [JsonPropertyName("message")]
        public  string? Message { get; set; } = string.Empty;
        [JsonPropertyName("new_status")]
        public string? NewStatus { get; set; }
        [JsonPropertyName("claim_id")]
        public int? ClaimId { get; set; }
       
    }
}