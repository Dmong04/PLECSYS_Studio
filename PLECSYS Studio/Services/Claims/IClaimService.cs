using PLECSYS_Studio.Wrappers.Claims;

namespace PLECSYS_Studio.Services.Claims
{
    public interface IClaimService
    {
        Task<ClaimResponse?> RegisterClaimAsync(ClaimRequest request, IEnumerable<(Stream Stream, string FileName, string ContentType)> attachments, CancellationToken ct = default);   
    }
}