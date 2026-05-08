using PLECSYS_Studio.Data.SaleOrderDetails;
using PLECSYS_Studio.Wrappers;
using PLECSYS_Studio.Wrappers.SaleOrderDetails;

namespace PLECSYS_Studio.Services.SaleOrderDetails
{
    public class SaleOrderDetailService(SaleOrderDetailData _data) : ISaleOrderDetailService
    {
        public async Task<APIResponse<List<DetailsResponse>>> CreateSaleOrderDetails(List<DetailsRequest> requests)
        {
            try
            {
                var new_sale_order_detail = await _data.CreateSaleOrderDetails(requests);
                if (!new_sale_order_detail.Success)
                {
                    return new APIResponse<List<DetailsResponse>>()
                    {
                        Data = null,
                        Success = false,
                        Message = new_sale_order_detail.Message
                    };
                }

                return new_sale_order_detail;
            }
            catch (Exception ex)
            {
                return new APIResponse<List<DetailsResponse>>()
                {
                    Data = null,
                    Success = false,
                    Message = $"Hubo un error al procesar la solicitud: {ex.Message}"
                };
            }
        }
    }
}
