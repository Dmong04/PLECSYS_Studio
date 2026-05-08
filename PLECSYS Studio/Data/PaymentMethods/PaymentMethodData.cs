using PLECSYS_PROTOTYPE_MAUI.Models;
using PLECSYS_Studio.Wrappers;
using PLECSYS_Studio.Wrappers.PaymentMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PLECSYS_Studio.Data.PaymentMethods
{
    public class PaymentMethodData(IHttpClientFactory factory)
    {
        private readonly HttpClient _http = factory.CreateClient("PLECSYS");

        public async Task<APIResponse<List<PaymentMethodResponse>>> GetAllPaymentmethods()
        {
            var response = await _http.GetFromJsonAsync<APIResponse<List<PaymentMethod>>>("payment/method/all");

            return new APIResponse<List<PaymentMethodResponse>>()
            {
                Data = MapPaymentMethods(response?.Data),
                Success = true,
                Message = response?.Message
            };
        }

        private static List<PaymentMethodResponse>? MapPaymentMethods(List<PaymentMethod>? paymentMethods)
        {
            return paymentMethods?.Select(pm => new PaymentMethodResponse()
            {
                PaymentMethodId = pm.payment_method_id,
                PaymentMethodName = pm.Payment_method_Name,
                PaymentMethodCode = pm.Payment_method_Code,
            }).ToList();
        }
    }
}
