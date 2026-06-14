using PLECSYS_Studio.Wrappers;
using PLECSYS_Studio.Wrappers.Claims;

namespace PLECSYS_Studio.Services.Claims
{
    public interface IClaimService
    {
        Task<APIResponse<ClaimResponse>?> RegisterClaimAsync(ClaimRequest request);   
    }
}