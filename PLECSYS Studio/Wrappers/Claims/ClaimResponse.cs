using System.Text.Json.Serialization;

namespace PLECSYS_Studio.Wrappers.Claims
{
    public class ClaimResponse
    {
        public int? Claim_id { get; set; }
        public DateTime? Record_date { get; set; }
        public string? User { get; set; }
        public string? Description { get; set; }
        public int? Invoice { get; set; }
        public decimal? Claim_amount { get; set; }
    }
}