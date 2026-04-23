using LibraryManager.DTOs.Account;
using Microsoft.AspNetCore.Identity;

namespace LibraryManager.Services.Contracts
{
    public interface IAccountService
    {
        Task<IdentityResult> RegisterAsync(RegisterDto model);
        Task<SignInResult> LoginAsync(LoginDto model);
        Task LogoutAsync();
        Task<bool> CreateRoleAsync(string roleName);
    }
}
