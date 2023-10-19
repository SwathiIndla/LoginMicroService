using Microsoft.AspNetCore.Identity;

namespace AuthMicroService.Repository
{
    public interface IAuthRepository
    {
        Task<IdentityResult> CreateUser(IdentityUser user, string password);
    }
}
