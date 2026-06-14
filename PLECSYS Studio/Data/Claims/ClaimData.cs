using PLECSYS_Studio.Services;
using PLECSYS_Studio.Wrappers;
using PLECSYS_Studio.Wrappers.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PLECSYS_Studio.Data.Claims
{
    public class ClaimData(IHttpClientFactory factory, SessionService session)
    {
        private readonly HttpClient _http = factory.CreateClient("PLECSYS");

        public async Task<APIResponse<ClaimResponse>> RegisterClaim(ClaimRequest request)
        {
            var token = session.GetAccessToken();

            using var httpRequest = new HttpRequestMessage(HttpMethod.Post, "claim/create");
            httpRequest.Content = new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json");
            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _http.SendAsync(httpRequest);
            var raw = await response.Content.ReadAsStringAsync();

            // El backend devuelve 400 cuando ya hay un reclamo activo,
            // pero igual trae body con success/message útil
            return JsonSerializer.Deserialize<APIResponse<ClaimResponse>>(
                raw, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                ?? new APIResponse<ClaimResponse> { Data = null, Success = false, Message = "Respuesta vacía" };
        }
    }
}
