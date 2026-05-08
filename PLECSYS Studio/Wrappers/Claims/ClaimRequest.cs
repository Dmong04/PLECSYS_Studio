namespace PLECSYS_Studio.Wrappers.Claims
{
    public class ClaimRequest
    {
        //Lo tomamos del popup y se paso por el QueryProperty
        public int InvoiceConsecutive { get; set; }
        public DateTime RecordDate { get; set; }
        public required string Description { get; set; }
        public decimal? ClaimAmount { get; set; }
        public string? User_email { get; set; }

    }
}