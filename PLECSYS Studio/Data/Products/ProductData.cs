using PLECSYS_Studio.Wrappers;
using PLECSYS_Studio.Wrappers.Products;
using System.Net.Http.Json;

namespace PLECSYS_Studio.Data.Products
{
    public class ProductData(IHttpClientFactory factory)
    {
        private readonly HttpClient _http = factory.CreateClient("PLECSYS");

        public async Task<APIResponse<List<ProductResponse>>> GetAllProducts()
        {
            var response = await _http.GetFromJsonAsync<APIResponse<List<ProductResponse>>>("product/all");
            var success = response.Data?.Select(p => new ProductResponse()
            {
                Product_id = p.Product_id,
                Product_name = p.Product_name,
                Product_detail = p.Product_detail,
                Unit_price = p.Unit_price
            }).ToList();

            return new APIResponse<List<ProductResponse>>()
            {
                Data = success,
                Success = response.Success,
                Message = response.Message
            };
        }

        public async Task<APIResponse<List<ProductResponse>>> GetProductsByName(string query)
        {
            var response = await _http.GetFromJsonAsync<APIResponse<List<ProductResponse>>>($"product/search/{query}");
            var success = response.Data?.Select(p => new ProductResponse()
            {
                Product_id = p.Product_id,
                Product_name = p.Product_name,
                Product_detail = p.Product_detail,
                Unit_price = p.Unit_price
            }).ToList();

            return new APIResponse<List<ProductResponse>>()
            {
                Data = success,
                Success = response.Success,
                Message = response.Message
            };
        }
    }
}
