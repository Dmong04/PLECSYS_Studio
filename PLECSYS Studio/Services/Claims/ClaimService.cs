using PLECSYS_Studio.Data.Claims;
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

        public async Task<ClaimResponse?> RegisterClaimAsync(ClaimRequest request, IEnumerable<(Stream Stream, string FileName, string ContentType)> attachments, CancellationToken ct = default)
        {
           var result = await _data.RegisterClaimASync(request, ct);
           if (result is not { Success: true, ClaimId: not null})
                return result;

            if (attachments?.Any() == true)
            {
                var ok = await _data.UploadClaimAttachmentAsync(result.ClaimId!.Value, attachments, ct);
                if (!ok) 
                    result.Message += " (Adjuntos no se pudieron subir)";

            }
            return result;
        }
        
    }
}