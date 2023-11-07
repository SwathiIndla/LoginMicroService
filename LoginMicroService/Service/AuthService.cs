using AuthMicroService.Repository;
using Microsoft.AspNetCore.Identity;

namespace AuthMicroService.Service
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            this.authRepository = authRepository;
        }

        public async Task<IdentityResult> AddRoles(IdentityUser user, string[] roles)
        {
            return await authRepository.AddRoles(user, roles);
        }

        public async Task<bool> CheckPassword(IdentityUser user, string password)
        {
            return await authRepository.CheckPassword(user, password);
        }

        public async Task<IdentityResult> CreateUser(IdentityUser user, string password)
        {
            return await authRepository.CreateUser(user, password);
        }

        public async Task<IdentityResult> DeleteUser(IdentityUser user)
        {
            return await authRepository.DeleteUser(user);
        }

        public async Task<IdentityUser?> FindUserByEmail(string email)
        {
            return await authRepository.FindUserByEmail(email);
        }

        public async Task<IdentityUser?> FindUserById(Guid customerId)
        {
            return await authRepository.FindUserById(customerId);
        }

        public async Task<string> GeneratePasswordResetToken(IdentityUser user)
        {
            return await authRepository.GeneratePasswordResetToken(user);
        }

        public async Task<IList<string>> GetRoles(IdentityUser user)
        {
            return await authRepository.GetRoles(user);
        }

        public async Task<IdentityResult> ResetPassword(IdentityUser user, string token, string newPassword)
        {
            return await authRepository.ResetPassword(user, token, newPassword);
        }
    }
}
