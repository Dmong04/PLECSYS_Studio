using PLECSYS_Studio.Wrappers;
using PLECSYS_Studio.Wrappers.SaleOrderDetails;

namespace PLECSYS_Studio.Services.SaleOrderDetails
{
    public interface ISaleOrderDetailService
    {
        Task<APIResponse<List<DetailsResponse>>> CreateSaleOrderDetails(List<DetailsRequest> request);
    }
}
