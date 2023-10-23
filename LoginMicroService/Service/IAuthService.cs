using Microsoft.AspNetCore.Identity;

namespace AuthMicroService.Service
{
    public interface IAuthService
    {
        Task<IdentityResult> CreateUser(IdentityUser user, string password);
        Task<IdentityUser?> FindUserByEmail(string email);
    }
}
