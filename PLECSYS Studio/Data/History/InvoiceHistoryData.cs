using PLECSYS_Studio.Wrappers;
using PLECSYS_Studio.Wrappers.History;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PLECSYS_Studio.Data.History
{
    public class InvoiceHistoryData(IHttpClientFactory factory)
    {
        private readonly HttpClient _http = factory.CreateClient("PLECSYS");

        public async Task<APIResponse<InvoiceHistoryResponse>> GetInvoicesHistorybyId(int historyId)
        {
            var response = await _http.GetAsync($"invoice/history/{historyId}");
            var raw = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<APIResponse<InvoiceHistoryResponse>>(raw,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (result is null)
            {
                throw new Exception("Respuesta inválida del backend");
            }

            return result;
        }
    }
}
