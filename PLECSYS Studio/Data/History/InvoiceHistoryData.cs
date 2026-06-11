using PLECSYS_Studio.Services;
using PLECSYS_Studio.Wrappers;
using PLECSYS_Studio.Wrappers.History;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace PLECSYS_Studio.Data.History
{
    public class InvoiceHistoryData(IHttpClientFactory factory, SessionService session)
    {
        private readonly HttpClient _http = factory.CreateClient("PLECSYS");

        public async Task<APIResponse<List<InvoiceHistoryResponse>>> GetPaymentHistory(int invoiceId)
        {
            var response = await _http.GetAsync($"invoice/payment/history/{invoiceId}");
            return await response.Content.ReadFromJsonAsync<APIResponse<List<InvoiceHistoryResponse>>>();
        }

        public async Task<APIResponse<List<InvoiceHistoryResponse>>> GetClaimHistory(int invoiceId)
        {
            var response = await _http.GetAsync($"invoice/claim/history/{invoiceId}");
            return await response.Content.ReadFromJsonAsync<APIResponse<List<InvoiceHistoryResponse>>>();
        }

        public async Task<APIResponse<List<InvoiceHistoryResponse>>> GetInvoiceHistoryByUserAndCompanyId(FindHistoryRequest request)
        {
            var token = session.GetAccessToken();

            using var httpRequest = new HttpRequestMessage(HttpMethod.Post, "invoice/history/all");
            httpRequest.Content = new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json");
            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _http.SendAsync(httpRequest);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<APIResponse<List<InvoiceHistoryResponse>>>();
            return result ?? new APIResponse<List<InvoiceHistoryResponse>> { Data = null, Success = false, Message = "Respuesta vacía" };
        }
    }
}