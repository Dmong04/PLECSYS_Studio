using PLECSYS_Studio.Models;
using PLECSYS_Studio.Services;
using PLECSYS_Studio.Wrappers;
using PLECSYS_Studio.Wrappers.Currencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PLECSYS_Studio.Data.Currencies
{
    public class CurrencyData(IHttpClientFactory factory, SessionService session)
    {
        private readonly HttpClient _http = factory.CreateClient("PLECSYS");

        public async Task<APIResponse<List<CurrencyResponse>>> GetAllCurrencies()
        {
            var token = session.GetAccessToken();

            using var httpRequest = new HttpRequestMessage(HttpMethod.Get, "currency/all");
            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _http.SendAsync(httpRequest);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<APIResponse<List<CurrencyResponse>>>(
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                ?? new APIResponse<List<CurrencyResponse>> { Data = [], Success = false, Message = "Respuesta vacía" };
        }

        private static List<CurrencyResponse>? MapCurrencies(List<Currency>? currencies)
        {
            return currencies?.Select(c => new CurrencyResponse
            {
                CurrencyId = c.Currency_id,
                CurrencyIso = c.Currency_ISO,
                CurrencyCode = c.Currency_code,
                CurrencyName = c.Currency_name,
            }).ToList();
        }
    }
}
