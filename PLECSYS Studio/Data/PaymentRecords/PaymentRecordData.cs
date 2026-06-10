using PLECSYS_Studio.Services;
using PLECSYS_Studio.Wrappers;
using PLECSYS_Studio.Wrappers.PaymentRecords;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace PLECSYS_Studio.Data.PaymentRecords
{
    public class PaymentRecordData(IHttpClientFactory factory, SessionService session)
    {
        private readonly HttpClient _http = factory.CreateClient("PLECSYS");

        public async Task<APIResponse<PaymentRecordResponse>> RegisterPayment(PaymentRecordRequest request)
        {
            var token = session.GetAccessToken();

            using var httpRequest = new HttpRequestMessage(HttpMethod.Post, "payment/record/create");
            httpRequest.Content = new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json");
            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _http.SendAsync(httpRequest);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<APIResponse<PaymentRecordResponse>>(
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                ?? new APIResponse<PaymentRecordResponse> { Data = null, Success = false, Message = "Respuesta vacía" };
        }
    }
}
