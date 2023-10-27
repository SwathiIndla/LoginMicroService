using Microsoft.AspNetCore.Identity;

namespace AuthMicroService.Service
{
    public interface IAuthService
    {
        Task<IdentityResult> CreateUser(IdentityUser user, string password);
        Task<IdentityUser?> FindUserByEmail(string email);
        Task<string> GeneratePasswordResetToken(IdentityUser user);
        Task<IdentityResult> ResetPassword(IdentityUser user, string token, string newPassword);
        Task<bool> CheckPassword(IdentityUser user, string password);
        Task<IList<string>> GetRoles(IdentityUser user);
        Task<IdentityResult> AddRoles(IdentityUser user, string[] roles);
        Task<IdentityResult> DeleteUser(IdentityUser user);
    }
}
