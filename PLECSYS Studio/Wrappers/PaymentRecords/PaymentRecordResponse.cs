using PLECSYS_Studio.Wrappers.Companies;
using PLECSYS_Studio.Wrappers.Currencies;
using PLECSYS_Studio.Wrappers.Invoices;
using PLECSYS_Studio.Wrappers.PaymentMethods;
using System.Text.Json.Serialization;

namespace PLECSYS_Studio.Wrappers.PaymentRecords
{
    public class PaymentRecordResponse
    {
        public int PaymentId { get; set; }
        public string? PaymentDetail { get; set; }
        public CompanyResponse? SellCompany { get; set; }
        public CompanyResponse? ChargedCompany { get; set; }
        public InvoiceResponse? SourceId { get; set; }
        public CurrencyResponse? Currency { get; set; }
        public PaymentMethodResponse? PaymentMethod { get; set; }
        public string? DetailPaymentmethod { get; set; }
        public decimal PaidAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? ThirdpartytransactionId { get; set; }
    }
}