using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PLECSYS_Studio.Wrappers.Currencies
{
    public class CurrencyResponse
    {
        [JsonPropertyName("currency_id")]
        public int CurrencyId { get; set; }

        [JsonPropertyName("currency_ISO")]
        public string? CurrencyIso { get; set; }

        [JsonPropertyName("currency_code")]
        public string? CurrencyCode { get; set; }

        [JsonPropertyName("currency_name")]
        public string? CurrencyName { get; set; }
    }
}
