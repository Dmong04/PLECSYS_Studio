using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLECSYS_Studio.Wrappers.Currencies
{
    public class CurrencyResponse
    {
        public int CurrencyId { get; set; }

        public string? CurrencyIso { get; set; }

        public string? CurrencyCode { get; set; }

        public string? CurrencyName { get; set; }
    }
}
