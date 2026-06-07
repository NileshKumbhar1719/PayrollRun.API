using Auth.Data;
using Microsoft.AspNetCore.Identity;

namespace Auth.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<UserRegister> _userManager;
        private readonly SignInManager<UserRegister> _signInmanager;
        private readonly RoleManager<IdentityRole> _Rolemanager;

        public AuthRepository(UserManager<UserRegister>userManager,
            SignInManager<UserRegister> signInManager,
            RoleManager<IdentityRole> roleManager) 
        { 
            _userManager = userManager;
            _signInmanager = signInManager;
            _Rolemanager = roleManager;
        
        }
        public Task<IdentityResult> AddToRoleAsync(UserRegister user, string role)
        {
           return _userManager.AddToRoleAsync(user, role);
        }

        public Task<bool> CheckPasswordAsync(UserRegister user, string password)
        {
            return _userManager.CheckPasswordAsync(user, password);
        }

        public Task<IdentityResult> CreateRoleAsync(string role)
        {
            return _Rolemanager.CreateAsync(new IdentityRole(role));
        }

        public Task<IdentityResult> CreateUserAsync(UserRegister user, string password)
        {
            return _userManager.CreateAsync(user, password);
        }

        public Task<UserRegister> FindByEmailAsync(string email)
        {
            return _userManager.FindByEmailAsync(email);
        }

        public Task<UserRegister> FindByUsernameAsync(string username)
        {
           return _userManager.FindByNameAsync(username);
        }

        public Task<IList<string>> GetRolesAsync(UserRegister user)
        {
            return _userManager.GetRolesAsync(user);
        }

        public Task<bool> RoleExistsAsync(string role)
        {
           return _Rolemanager.RoleExistsAsync(role);
        }

        public Task SignOutAsync()
        {
           return _signInmanager.SignOutAsync();
        }
    }
}
