using PLECSYS_PROTOTYPE_MAUI.Models;
using PLECSYS_Studio.Services;
using PLECSYS_Studio.Wrappers;
using PLECSYS_Studio.Wrappers.PaymentMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PLECSYS_Studio.Data.PaymentMethods
{
    public class PaymentMethodData(IHttpClientFactory factory, SessionService session)
    {
        private readonly HttpClient _http = factory.CreateClient("PLECSYS");

        public async Task<APIResponse<List<PaymentMethodResponse>>> GetAllPaymentMethods()
        {
            var token = session.GetAccessToken();

            using var httpRequest = new HttpRequestMessage(HttpMethod.Get, "payment/method/all");
            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _http.SendAsync(httpRequest);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<APIResponse<List<PaymentMethodResponse>>>(
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                ?? new APIResponse<List<PaymentMethodResponse>> { Data = [], Success = false, Message = "Respuesta vacía" };
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
