namespace PLECSYS_PROTOTYPE_MAUI.Models
{
    public class Claim
    {
        public int ClaimId { get; set; }
        public int InvoiceConsecutive { get; set; }
        public DateTime RecordDate { get; set; } 
        public string Description { get; set; } = string.Empty;
        public decimal? Claim_amount { get; set; }
        public string? User_email { get; set; }
        public string Status { get; set; } = "Con Reclamo";
    }
}