using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PLECSYS_Studio.Wrappers;
using PLECSYS_Studio.Wrappers.PaymentRecords;

namespace PLECSYS_Studio.Services.PaymentService
{
    public interface IPaymentRecordService
    {
        Task<APIResponse<PaymentRecordResponse>?> RegisterPaymentAsync(PaymentRecordRequest request);
    }
}
