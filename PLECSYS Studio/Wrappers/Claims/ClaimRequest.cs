using System.Text.Json.Serialization;

namespace PLECSYS_Studio.Wrappers.Claims
{
    public class ClaimRequest
    {
        [JsonPropertyName("record_date")]
        public DateTime Record_date { get; set; }

        [JsonPropertyName("user_id")]
        public string User_id { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("invoice_id")]
        public int Invoice_id { get; set; }

        [JsonPropertyName("claim_amount")]
        public decimal Claim_amount { get; set; }
    }
}