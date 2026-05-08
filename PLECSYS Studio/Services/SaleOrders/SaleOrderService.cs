using PLECSYS_Studio.Data.SaleOrders;
using PLECSYS_Studio.Wrappers;
using PLECSYS_Studio.Wrappers.SaleOrders;

namespace PLECSYS_Studio.Services.SaleOrders
{
    public class SaleOrderService(SaleOrderData _data) : ISaleOrderService
    {
        public async Task<APIResponse<SaleOrderResponse>> CreateSaleOrder(SaleOrderRequest request)
        {
            try
            {
                var new_sale_order = await _data.CreateSaleOrder(request);
                if (!new_sale_order.Success)
                {
                    return new APIResponse<SaleOrderResponse>()
                    {
                        Data = null,
                        Success = false,
                        Message = new_sale_order.Message
                    };
                }

                return new_sale_order;
            }
            catch (Exception ex)
            {
                return new APIResponse<SaleOrderResponse>()
                {
                    Data = null,
                    Success = false,
                    Message = $"Hubo un problema al procesar la solicitud: {ex.Message}"
                };
            }
        }
    }
}
