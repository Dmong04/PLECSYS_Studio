
using PLECSYS_Studio.Wrappers;
using PLECSYS_Studio.Wrappers.Products;

namespace PLECSYS_Studio.Services.Products
{
    public interface IProductService
    {
        Task<APIResponse<List<ProductResponse>>> GetAllProducts();

        Task<APIResponse<List<ProductResponse>>> GetProductsByName(string query);
    }
}
