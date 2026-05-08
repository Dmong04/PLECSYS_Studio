
using PLECSYS_Studio.Wrappers;
using PLECSYS_Studio.Wrappers.SaleOrders;
using System.Net.Http.Json;

namespace PLECSYS_Studio.Data.SaleOrders
{
    public class SaleOrderData(IHttpClientFactory factory)
    {
        private readonly HttpClient _http = factory.CreateClient("PLECSYS");

        public async Task<APIResponse<SaleOrderResponse>> CreateSaleOrder(SaleOrderRequest request)
        {
            var response = await _http.PostAsJsonAsync("sale/order/create", request);
            response.EnsureSuccessStatusCode();

            var created = await response.Content.ReadFromJsonAsync<APIResponse<SaleOrderResponse>>();
            var success = new SaleOrderResponse()
            {
                Order_id = created?.Data?.Order_id ?? 0,
                Client = created?.Data?.Client,
                Order_date = created?.Data?.Order_date
            };

            return new APIResponse<SaleOrderResponse>()
            {
                Data = success,
                Success = created.Success,
                Message = created.Message
            };
        }
    }
}
