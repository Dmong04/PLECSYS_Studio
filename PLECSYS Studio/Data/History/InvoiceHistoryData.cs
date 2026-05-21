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

        public async Task<APIResponse<List<InvoiceHistoryResponse>>> GetInvoiceHistoryByUserAndCompanyId(FindHistoryRequest request)
        {
            var response = await _http.PostAsJsonAsync("invoice/history/all", request);
            response.EnsureSuccessStatusCode();

            var success = await response.Content.ReadFromJsonAsync<APIResponse<List<InvoiceHistoryResponse>>>();
            return success;
        }
    }
}
