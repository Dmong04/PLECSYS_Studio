using PLECSYS_Studio.Wrappers.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PLECSYS_Studio.Data.Claims
{
    public class ClaimData(IHttpClientFactory factory)
    {

        private readonly HttpClient _http = factory.CreateClient("PLECSYS");
        public async Task<ClaimResponse> RegisterClaimASync(ClaimRequest request, CancellationToken ct = default)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("claims/register", request, ct); //Homologar con la ruta correcta
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<ClaimResponse?>(cancellationToken: ct);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"RegisterClaim error: {ex.Message}");
                return new ClaimResponse { Success = false, Message = "No se pudo registrar el reclamo." };
            }
        }

        public async Task<bool> UploadClaimAttachmentAsync(int claimId, IEnumerable<(Stream Stream, string FileName, string ContentType)> files, CancellationToken ct = default)
        {
            using var content = new MultipartFormDataContent();

            foreach (var file in files)
            {
                var sc = new StreamContent(file.Stream);
                sc.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                content.Add(sc, "files", file.FileName);
            }

            try
            {
                var resp = await _http.PostAsync($"claims/{claimId}/attachments", content, ct);
                return resp.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"UploadClaimAttachments error: {ex.Message}");
                return false;
            }
        }
    }
}
