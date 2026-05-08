using PLECSYS_Studio.Wrappers;
using PLECSYS_Studio.Wrappers.PaymentMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLECSYS_Studio.Services.PaymentMethods
{
    public interface IPaymentMethodService
    {
        Task<APIResponse<List<PaymentMethodResponse>>> GetPaymentMethods();
    }
}
