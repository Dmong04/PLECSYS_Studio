using PLECSYS_Studio.Wrappers.Companies;
using PLECSYS_Studio.Wrappers.Currencies;
using PLECSYS_Studio.Wrappers.Invoices;
using PLECSYS_Studio.Wrappers.PaymentMethods;
using System.Text.Json.Serialization;

namespace PLECSYS_Studio.Wrappers.PaymentRecords
{
    public class PaymentRecordResponse
    {
        public int Payment_record_id { get; set; }
        public InvoiceResponse? Source { get; set; }
        public CurrencyResponse? Currency { get; set; }       //  del namespace Currencies
        public PaymentMethodResponse? Payment_method { get; set; } //  del namespace PaymentMethods
        public string? Detail_payment_method { get; set; }
        public decimal? Paid_amount { get; set; }
        public DateTime? Payment_date { get; set; }
        public string? Payment_detail { get; set; }
        public string? Third_party_transaction_id { get; set; }
    }
}