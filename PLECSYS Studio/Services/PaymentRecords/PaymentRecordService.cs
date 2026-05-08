using PLECSYS_Studio.Data;
using PLECSYS_Studio.Data.PaymentRecords;
using PLECSYS_Studio.Services.PaymentService;
using PLECSYS_Studio.Wrappers;
using PLECSYS_Studio.Wrappers.PaymentRecords;
using System.Net.Http.Json;

namespace PLECSYS_Studio.Services.Payments
{

    public class PaymentRecordService : IPaymentRecordService
    {

        private readonly PaymentRecordData _data;
        public PaymentRecordService(PaymentRecordData data)
        {
            _data = data;
        }

        public async Task<APIResponse<PaymentRecordResponse>?> RegisterPaymentAsync(PaymentRecordRequest request)
        {
            try
            {
                var newPayment = await _data.RegisterPayment(request);

                if (!newPayment.Success)
                {
                    return new APIResponse<PaymentRecordResponse>()
                    {
                        Data = null,
                        Success = false,
                        Message = "No se ha registrado el pago"
                    };
                }

                return new APIResponse<PaymentRecordResponse>()
                {
                    Data = newPayment.Data,
                    Success = true,
                    Message = newPayment.Message,
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error registrando el pago: {ex.Message}");
                return new APIResponse<PaymentRecordResponse>()
                {
                    Data = null,
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}