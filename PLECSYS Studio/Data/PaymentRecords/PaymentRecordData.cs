using PLECSYS_Studio.Wrappers;
using PLECSYS_Studio.Wrappers.PaymentRecords;
using System.Net.Http.Json;

namespace PLECSYS_Studio.Data.PaymentRecords
{
    public class PaymentRecordData(IHttpClientFactory factory)
    {
        private readonly HttpClient _http = factory.CreateClient("PLECSYS");

        public async Task<APIResponse<PaymentRecordResponse>> RegisterPayment(PaymentRecordRequest request)
        {
            var response = await _http.PostAsJsonAsync("payment/record/create", request);
            response.EnsureSuccessStatusCode();

            var paymentResult = await response.Content.ReadFromJsonAsync<APIResponse<PaymentRecordResponse>>();
            return paymentResult;
        }
    }
}
