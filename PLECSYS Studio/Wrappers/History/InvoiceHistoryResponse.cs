using System.Text.Json.Serialization;

namespace PLECSYS_Studio.Wrappers.History
{
    public class InvoiceHistoryResponse
    {
        public int? Invoice_history_id { get; set; }
        public int? Invoice { get; set; }
        public DateTime? Record_date { get; set; }
        public string? Action { get; set; }
        public string? Description { get; set; }
    }
}