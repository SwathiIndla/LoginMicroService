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

        public async Task<IdentityResult> CreateUser(IdentityUser user, string password)
        {
            return await authRepository.CreateUser(user, password);
        }
    }
}
