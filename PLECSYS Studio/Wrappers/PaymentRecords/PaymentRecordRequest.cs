using System;

using System.Text.Json.Serialization;

namespace PLECSYS_Studio.Wrappers.PaymentRecords
{
    public class PaymentRecordRequest
    {
        [JsonPropertyName("source_id")]
        public int Source_id { get; set; }

        [JsonPropertyName("currency_id")]
        public int Currency_id { get; set; }

        [JsonPropertyName("payment_method_id")]
        public int Payment_method_id { get; set; }

        [JsonPropertyName("detail_payment_method")]
        public string? Detail_payment_method { get; set; }

        [JsonPropertyName("paid_amount")]
        public decimal Paid_amount { get; set; }

        [JsonPropertyName("payment_date")]
        public DateTime Payment_date { get; set; }

        [JsonPropertyName("payment_detail")]
        public string? Payment_detail { get; set; }

        [JsonPropertyName("third_party_transaction_id")]
        public string? Third_party_transaction_id { get; set; }
    }
}