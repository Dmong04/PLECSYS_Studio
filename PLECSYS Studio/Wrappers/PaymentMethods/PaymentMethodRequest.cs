using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLECSYS_Studio.Wrappers.PaymentMethods
{
    public class PaymentMethodRequest
    {
        public required string PaymentMethodName { get; set; }
        public required int PaymentMethodCode { get; set; }
    }
}
