using DnsClient;
using PLECSYS_Studio.Wrappers;
using PLECSYS_Studio.Wrappers.GPS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PLECSYS_Studio.Data.GPS
{
    public class GPSData(IHttpClientFactory factory)
    {
        private readonly HttpClient _http = factory.CreateClient("PLECSYS");

        public async Task<APIResponse<CoordinateResponse>> SaveSellerRoute(CoordinateRequest request)
        {
            try
            {
                var coordinates = await _http.PostAsJsonAsync("gps/seller/route/register", request);
                coordinates.EnsureSuccessStatusCode();

                var result = await coordinates.Content.ReadFromJsonAsync<APIResponse<CoordinateResponse>>();
                var success = new CoordinateResponse()
                {
                    Id = result?.Data?.Id,
                    Seller_id = result?.Data?.Seller_id,
                    Timestamp = result?.Data?.Timestamp,
                    Start_location_name = result?.Data?.Start_location_name,
                    Start_location = result?.Data?.Start_location,
                    End_location_name = result?.Data?.End_location_name,
                    End_location = result?.Data?.End_location
                };

                return new APIResponse<CoordinateResponse>()
                {
                    Data = success,
                    Success = result.Success,
                    Message = result.Message
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<CoordinateResponse>()
                {
                    Data = null,
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
