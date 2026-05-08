using System;

namespace PLECSYS_Studio.Wrappers.PaymentRecords
{
    public class PaymentRecordRequest
    {
        public string? PaymentDetail { get; set; }
        public int SellCompanyId { get; set; }
        public int ChargedCompanyId { get; set; }
        public int SourceId { get; set; }
        public required int CurrencyId { get; set; }
        public required int PaymentMethodId { get; set; }
        public string? DetailPaymentmethod { get; set; }
        public decimal PaidAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public required string ThirdpartytransactionId { get; set; }

        //Idempotence
        public string ClientOperationId { get; set; } = Guid.NewGuid().ToString();
    }
}