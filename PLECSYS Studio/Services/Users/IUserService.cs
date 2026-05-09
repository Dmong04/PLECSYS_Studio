using PLECSYS_Studio.Wrappers;
using PLECSYS_Studio.Wrappers.Users;

namespace PLECSYS_Studio.Services.Users
{
    public interface IUserService
    {
        bool IsAuthenticated { get; }

        string? CurrentUserEmail { get; }

        //Task<APIResponse<LoginResponse>> MockLogin(string email, string password);

        Task<APIResponse<LoginResponse>> Login(string email, string password);

        Task<APIResponse<SignUpResponse>> SignUp(SignUpRequest request);

        Task Logout();

        Task<APIResponse<List<UserResponse>>> GetAllUsers();

        Task<APIResponse<List<UserResponse>>> GetUsersByName(string query);
    }
}
