using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PLECSYS_Studio.Wrappers.PaymentMethods
{
    public class PaymentMethodResponse
    {
        [JsonPropertyName("payment_method_id")]
        public int PaymentMethodId { get; set; }

        [JsonPropertyName("payment_method_name")]
        public string? PaymentMethodName { get; set; }

        [JsonPropertyName("payment_method_code")]
        public int? PaymentMethodCode { get; set; }
    }
}
