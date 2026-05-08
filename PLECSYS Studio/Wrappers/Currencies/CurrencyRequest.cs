using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLECSYS_Studio.Wrappers.Currencies
{
    public class CurrencyRequest
    {
        public required string CurrencyISO { get; set; }

        public required string CurrencyCode { get; set; }

        public required string CurrencyName { get; set; }
    }
}
