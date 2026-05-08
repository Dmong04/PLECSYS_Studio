using PLECSYS_Studio.Models.GPS;
using PLECSYS_Studio.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PLECSYS_Studio.Data.GPS
{
    public class LocationData(IHttpClientFactory factory)
    {
        private readonly HttpClient _http = factory.CreateClient("PLECSYS");

        public async Task<APIResponse<SaveLocationResponse>> SaveLocation(SaveLocationRequest request)
        {
            try
            {
                var response = await _http.PostAsJsonAsync(
                    "gps/seller/location",
                    request);

                response.EnsureSuccessStatusCode();

                var result = await response.Content
                    .ReadFromJsonAsync<APIResponse<SaveLocationResponse>>();

                return result ?? new APIResponse<SaveLocationResponse>
                {
                    Data = null,
                    Success = false,
                    Message = "Empty response"
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<SaveLocationResponse>
                {
                    Data = null,
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}