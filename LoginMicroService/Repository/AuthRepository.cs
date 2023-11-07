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

        public async Task<IdentityResult> AddRoles(IdentityUser user, string[] roles)
        {
            return await userManager.AddToRolesAsync(user, roles);
        }

        public async Task<bool> CheckPassword(IdentityUser user, string password)
        {
            return await userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IdentityResult> CreateUser(IdentityUser user, string password)
        {
            return await userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> DeleteUser(IdentityUser user)
        {
            return await userManager.DeleteAsync(user);
        }

        public async Task<IdentityUser?> FindUserByEmail(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }

        public async Task<string> GeneratePasswordResetToken(IdentityUser user)
        {
            return await userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<IList<string>> GetRoles(IdentityUser user)
        {
            return await userManager.GetRolesAsync(user);
        }

        public async Task<IdentityUser?> FindUserById(Guid customerId)
        {
            return await userManager.FindByIdAsync(customerId.ToString());
        }

        public async Task<IdentityResult> ResetPassword(IdentityUser user, string token, string newPassword)
        {
            return await userManager.ResetPasswordAsync(user, token, newPassword);
        }
    }
}
