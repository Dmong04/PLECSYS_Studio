using PLECSYS_Studio.Data.PaymentMethods;
using PLECSYS_Studio.Wrappers;
using PLECSYS_Studio.Wrappers.PaymentMethods;

namespace PLECSYS_Studio.Services.PaymentMethods
{
    public class PaymentMethodService(PaymentMethodData _data) : IPaymentMethodService
    {
        public async Task<APIResponse<List<PaymentMethodResponse>>> GetPaymentMethods()
        {
            try
            {
                var response = await _data.GetAllPaymentmethods();
                if (response?.Data?.Count == 0)
                {
                    return new APIResponse<List<PaymentMethodResponse>>()
                    {
                        Data = null,
                        Success = false,
                        Message = response.Message
                    };
                }

                return new APIResponse<List<PaymentMethodResponse>>()
                {
                    Data = response?.Data,
                    Success = true,
                    Message = response?.Message
                };
            } catch (Exception ex)
            {
                return new APIResponse<List<PaymentMethodResponse>>()
                {
                    Data = null,
                    Success = true,
                    Message = ex.Message
                };
            }
        }
    }
}
