using PLECSYS_Studio.Models;
using PLECSYS_Studio.Wrappers;
using PLECSYS_Studio.Wrappers.Currencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PLECSYS_Studio.Data.Currencies
{
    public class CurrencyData(IHttpClientFactory factory)
    {
        private readonly HttpClient _http = factory.CreateClient("PLECSYS");

        public async Task<APIResponse<List<CurrencyResponse>>> GetAllCurrencies()
        {
            var response = await _http.GetFromJsonAsync<APIResponse<List<Currency>>>("currency/all");

            return new APIResponse<List<CurrencyResponse>>()
            {
                Data = MapCurrencies(response?.Data),
                Success = true,
                Message = response?.Message
            };
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
