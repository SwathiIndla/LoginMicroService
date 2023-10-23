using Microsoft.AspNetCore.Identity;

namespace AuthMicroService.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<IdentityUser> userManager;

        public AuthRepository(UserManager<IdentityUser> userManager) 
        {
            this.userManager = userManager;
        }

        public async Task<IdentityResult> CreateUser(IdentityUser user, string password)
        {
            return await userManager.CreateAsync(user, password);
        }

        public async Task<IdentityUser?> FindUserByEmail(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }
    }
}
