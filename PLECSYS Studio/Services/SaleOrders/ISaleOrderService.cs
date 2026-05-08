
using PLECSYS_Studio.Wrappers;
using PLECSYS_Studio.Wrappers.SaleOrders;

namespace PLECSYS_Studio.Services.SaleOrders
{
    public interface ISaleOrderService
    {
        Task<APIResponse<SaleOrderResponse>> CreateSaleOrder(SaleOrderRequest request);
    }
}
