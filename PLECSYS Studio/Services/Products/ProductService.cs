using PLECSYS_Studio.Data.Products;
using PLECSYS_Studio.Wrappers;
using PLECSYS_Studio.Wrappers.Products;

namespace PLECSYS_Studio.Services.Products
{
    public class ProductService(ProductData _data) : IProductService
    {
        public async Task<APIResponse<List<ProductResponse>>> GetAllProducts()
        {
            try
            {
                var products = await _data.GetAllProducts();
                if (products?.Data?.Count is 0)
                {
                    return new APIResponse<List<ProductResponse>>()
                    {
                        Data = [],
                        Success = true,
                        Message = products.Message
                    };
                }

                return products;
            }
            catch (Exception ex)
            {
                return new APIResponse<List<ProductResponse>>()
                {
                    Data = [],
                    Success = false,
                    Message = $"Ha ocurrido un error al procesar la solicitud: {ex.Message}"
                };
            }
        }

        public async Task<APIResponse<List<ProductResponse>>> GetProductsByName(string query)
        {
            try
            {
                var products = await _data.GetProductsByName(query);
                if (products?.Data?.Count is 0)
                {
                    return new APIResponse<List<ProductResponse>>()
                    {
                        Data = [],
                        Success = true,
                        Message = products.Message
                    };
                }

                return products;
            }
            catch (Exception ex)
            {
                return new APIResponse<List<ProductResponse>>()
                {
                    Data = [],
                    Success = false,
                    Message = $"Ha ocurrido un error al procesar la solicitud: {ex.Message}"
                };
            }
        }
    }
}
