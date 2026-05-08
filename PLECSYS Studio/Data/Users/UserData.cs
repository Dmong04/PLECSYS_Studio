using PLECSYS_Studio.Models;
using PLECSYS_Studio.Wrappers;
using PLECSYS_Studio.Wrappers.Users;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace PLECSYS_Studio.Data.Users
{
    public class UserData
    {
        private readonly HttpClient _http;

        public UserData(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("PLECSYS");

            var token = Preferences.Get("access_token", string.Empty);

            if (!string.IsNullOrEmpty(token))
            {
                _http.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<LoginResponse> MockLogin(LoginRequest request)
        {
            var response = await _http.PostAsJsonAsync("user/login", request);
            response.EnsureSuccessStatusCode();
            var login_result = await response.Content.ReadFromJsonAsync<APIResponse<LoginResponse>>();
            return login_result.Data;
        }

        public async Task<TokenResponse> Login(LoginRequest request)
        {
            var payload = new
            {
                username = request.email,
                password = request.password
            };

            var response = await _http.PostAsJsonAsync("api/auth/login", payload);

            var raw = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();

            var result = JsonSerializer.Deserialize<TokenResponse>(
                raw,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            if (result == null || string.IsNullOrWhiteSpace(result.AccessToken))
                throw new Exception("Respuesta inválida del backend (sin AccessToken).");

            Preferences.Set("access_token", result.AccessToken);

            _http.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", result.AccessToken);

            return result;
        }


        public async Task<APIResponse<List<UserResponse>>> GetAllUsers()
        {
            var response = await _http.GetFromJsonAsync<APIResponse<List<User>>>("user/all");
            var success = response?.Data?.Select(u => new UserResponse()
            {
                Email = u.Email,
                Name = u.Name,
                First_lastname = u.First_lastname,
                Second_lastname = u.Second_lastname,
                Phone = u.Phone
            }).ToList();
            return new APIResponse<List<UserResponse>>()
            {
                Data = success,
                Success = response.Success,
                Message = response?.Message,
            };
        }


        public async Task<APIResponse<List<UserResponse>>> GetUsersByName(string query)
        {
            var response = await _http.GetFromJsonAsync<APIResponse<List<User>>>($"user/search/{query}");
            var success = response?.Data?.Select(u => new UserResponse()
            {
                Email = u.Email,
                Name = u.Name,
                First_lastname = u.First_lastname,
                Second_lastname = u.Second_lastname,
                Phone = u.Phone
            }).ToList();
            return new APIResponse<List<UserResponse>>()
            {
                Data = success,
                Success = response.Success,
                Message = response?.Message,
            };
        }


        public async Task<APIResponse<SignUpResponse>> CreateUser(SignUpRequest request)
        {
            var response = await _http.PostAsJsonAsync("user/signup", request);
            response.EnsureSuccessStatusCode();

            var created = await response.Content.ReadFromJsonAsync<APIResponse<SignUpResponse>>();

            var success = new SignUpResponse()
            {
                Email = created.Data.Email,
                Name = created.Data.Name,
                First_lastname = created.Data.First_lastname,
                Second_lastname = created.Data.Second_lastname,
                Phone = created.Data.Phone,
                Created_at = created.Data.Created_at,
                Is_created = created.Data.Is_created,
            };

            return new APIResponse<SignUpResponse>()
            {
                Data = success,
                Success = created.Success,
                Message = created.Message
            };
        }
    }
}
