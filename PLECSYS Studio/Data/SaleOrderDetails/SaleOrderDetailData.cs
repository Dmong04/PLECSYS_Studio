
using PLECSYS_Studio.Wrappers;
using PLECSYS_Studio.Wrappers.SaleOrderDetails;
using System.Net.Http.Json;

namespace PLECSYS_Studio.Data.SaleOrderDetails
{
    public class SaleOrderDetailData(IHttpClientFactory factory)
    {
        private readonly HttpClient _http = factory.CreateClient("PLECSYS");

        public async Task<APIResponse<List<DetailsResponse>>> CreateSaleOrderDetails(List<DetailsRequest> requests)
        {
            var response = await _http.PostAsJsonAsync("sale/order/details/create", requests);
            response.EnsureSuccessStatusCode();

            var created = await response.Content.ReadFromJsonAsync<APIResponse<List<DetailsResponse>>>();

            return new APIResponse<List<DetailsResponse>>()
            {
                Data = created.Data,
                Success = created.Success,
                Message = created.Message
            };
        }
    }
}
