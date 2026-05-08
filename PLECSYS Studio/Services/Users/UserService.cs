using PLECSYS_Studio.Data.Users;
using PLECSYS_Studio.Models;
using PLECSYS_Studio.Wrappers;
using PLECSYS_Studio.Wrappers.Users;

namespace PLECSYS_Studio.Services.Users
{
    internal class UserService(UserData _data) : IUserService
    {
        public bool IsAuthenticated { get; private set; }

        public string? CurrentUserEmail { get; private set; }

        public async Task<APIResponse<List<UserResponse>>> GetAllUsers()
        {
            try
            {
                var users = await _data.GetAllUsers();
                if (users?.Data?.Count is 0)
                {
                    return new APIResponse<List<UserResponse>>()
                    {
                        Data = [],
                        Success = true,
                        Message = users.Message,
                    };
                }

                return users;
            }
            catch (Exception ex)
            {
                return new APIResponse<List<UserResponse>>()
                {
                    Data = [],
                    Success = true,
                    Message = $"Hubo un error al obtener el listado: {ex.Message}"
                };
            }
        }

        public async Task<APIResponse<List<UserResponse>>> GetUsersByName(string query)
        {
            try
            {
                var users = await _data.GetUsersByName(query);
                if (users?.Data?.Count is 0)
                {
                    return new APIResponse<List<UserResponse>>()
                    {
                        Data = [],
                        Success = true,
                        Message = users.Message,
                    };
                }

                return users;
            }
            catch (Exception ex)
            {
                return new APIResponse<List<UserResponse>>()
                {
                    Data = [],
                    Success = true,
                    Message = $"Hubo un error al obtener el listado: {ex.Message}"
                };
            }
        }

        public async Task<APIResponse<LoginResponse>> MockLogin(string email, string password)
        {
            try
            {
                var request = new LoginRequest()
                {
                    email = email,
                    password = password,
                };

                var isLogged = await _data.MockLogin(request);
                if (!isLogged.Is_logged)
                {
                    return new APIResponse<LoginResponse>
                    {
                        Data = null,
                        Success = false,
                        Message = "Inicio de sesión fallido",
                    };
                }
                ;

                IsAuthenticated = true;
                CurrentUserEmail = isLogged.Email;

                var loginResponse = new LoginResponse()
                {
                    Email = isLogged.Email,
                    Name = isLogged.Name
                };

                return new APIResponse<LoginResponse>()
                {
                    Data = loginResponse,
                    Success = true,
                    Message = "Login exitoso"
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<LoginResponse>()
                {
                    Data = null,
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<APIResponse<LoginResponse>> Login(string email, string password)
        {
            try
            {
                var request = new LoginRequest()
                {
                    email = email,
                    password = password
                };

                var tokenResponse = await _data.Login(request);

                if (string.IsNullOrEmpty(tokenResponse.AccessToken))
                {
                    IsAuthenticated = false;

                    return new APIResponse<LoginResponse>()
                    {
                        Data = new LoginResponse(),
                        Success = false,
                        Message = "No se pudo obtener el token."
                    };
                }

                IsAuthenticated = true;
                CurrentUserEmail = tokenResponse.Email;

                var loginResponse = new LoginResponse()
                {
                    Email = tokenResponse.Email,
                    Name = tokenResponse.UserName
                };

                return new APIResponse<LoginResponse>()
                {
                    Data = loginResponse,
                    Success = true,
                    Message = "Login exitoso"
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<LoginResponse>()
                {
                    Data = new LoginResponse(),
                    Success = false,
                    Message = $"Hubo un error en el inicio de sesión: {ex.Message}"
                };
            }
        }

        public async Task Logout()
        {
            IsAuthenticated = false;
            await Shell.Current.GoToAsync("//LoginPage");
        }

        public async Task<APIResponse<SignUpResponse>> SignUp(SignUpRequest request)
        {
            try
            {
                var signed = await _data.CreateUser(request);
                if (!signed.Success)
                {
                    return new APIResponse<SignUpResponse>()
                    {
                        Data = new SignUpResponse(),
                        Success = false,
                        Message = signed.Message
                    };
                }

                return signed;
            }
            catch (Exception ex)
            {
                return new APIResponse<SignUpResponse>()
                {
                    Data = new SignUpResponse(),
                    Success = false,
                    Message = $"Ha ocurrido un error al procesar la solicitud: {ex.Message}"
                };
            }
        }
    }
}
