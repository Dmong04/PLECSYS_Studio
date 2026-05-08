using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLECSYS_Studio.Wrappers.PaymentMethods
{
    public class PaymentMethodResponse
    {
        public int PaymentMethodId { get; set; }

        public string? PaymentMethodName { get; set; }

        public int? PaymentMethodCode { get; set; }
    }
}
