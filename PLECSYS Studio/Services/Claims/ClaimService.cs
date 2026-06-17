using PLECSYS_Studio.Data.Claims;
using PLECSYS_Studio.Wrappers;
using PLECSYS_Studio.Wrappers.Claims;

namespace PLECSYS_Studio.Services.Claims
{
    public class ClaimService : IClaimService
    {

        private readonly ClaimData _data;
        public ClaimService(ClaimData data)
        {
            _data = data;
        }

        public async Task<APIResponse<ClaimResponse>?> RegisterClaimAsync(ClaimRequest request)
        {
            try
            {
                var newClaim = await _data.RegisterClaim(request);
                if (!newClaim.Success)
                {
                    return new APIResponse<ClaimResponse>()
                    {
                        Data = null,
                        Success = false,
                        Message = newClaim.Message ?? "No se ha registrado el reclamo"
                    };
                }

                return new APIResponse<ClaimResponse>()
                {
                    Data = newClaim.Data,
                    Success = true,
                    Message = newClaim.Message,
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error registrando el reclamo: {ex.Message}");
                return new APIResponse<ClaimResponse>()
                {
                    Data = null,
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}