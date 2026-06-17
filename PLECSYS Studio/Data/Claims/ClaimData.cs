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
            var statusCode = (int)response.StatusCode;
            var body = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<APIResponse<ClaimResponse>>(
                body,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );
            if (result != null)
            {
                // si el backend no manda mensaje, ponemos un fallback formal
                if (string.IsNullOrWhiteSpace(result.Message))
                {
                    result.Message = $"Error: No se pudo registrar el reclamo.";
                }
                return result;
            }

            // fallback si no se pudo deserializar nada
            return result ?? new APIResponse<ClaimResponse>
            {
                Data = null,
                Success = false,
                Message = $"Error: No se pudo registrar el reclamo.",
            };

            // El backend devuelve 400 cuando ya hay un reclamo activo,
            // pero igual trae body con success/message útil
            //return JsonSerializer.Deserialize<APIResponse<ClaimResponse>>(
            //    raw, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            //    ?? new APIResponse<ClaimResponse> { Data = null, Success = false, Message = "Respuesta vacía" };
        }
    }
}
