using AuthMicroService.Models;
using AuthMicroService.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userData)
        {
            var result = new CreateUserResponse { Succeeded = false, Email = userData.Email, PasswordHash = "", CustomerId = Guid.Empty};
            var response = new IdentityResult();
            if (userData != null)
            {
                var user = new IdentityUser { Email = userData.Email, UserName = userData.Email };
                response = await authService.CreateUser(user, userData.Password);
                if(response.Succeeded)
                {
                    result = new CreateUserResponse
                    {
                        Succeeded = true,
                        Email = user.Email,
                        PasswordHash = user.PasswordHash ?? "",
                        CustomerId = Guid.Parse(user.Id)
                    };
                }
            }
            return result.Succeeded ? Ok(result) : BadRequest(response);
        }
    }
}
