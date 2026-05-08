namespace PLECSYS_Studio.ViewModels.Messages
{
    public class PaymentRegisteredMessage
    {
        public int InvoiceConsecutive { get; set; }
        public decimal NewPendingBalance { get; set; }
        public string? NewStatus { get; set; }
    }
}