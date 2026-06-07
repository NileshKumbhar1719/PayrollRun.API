using Auth.Models;

namespace Auth.Service
{
    public interface IAuthService
    {

        Task<AuthResponse> RegisterAsync(Register register);
        Task<AuthResponse> LoginAsync(Login login);
        Task LogoutAsync();
    }
}
