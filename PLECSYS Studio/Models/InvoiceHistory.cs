namespace PLECSYS_Studio.Models
{
    public class InvoiceHistory
    {
        public int InvoiceHistoryId { get; set; }
        public DateTime RecordDate { get; set; }
        public string Action {  get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}