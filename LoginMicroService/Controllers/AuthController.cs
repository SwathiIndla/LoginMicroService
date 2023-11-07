using AuthMicroService.Models.Request;
using AuthMicroService.Models.Response;
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

        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userData)
        {
            try
            {
                var result = new CreateUserResponse { Succeeded = false, Email = userData.Email, PasswordHash = "", CustomerId = Guid.Empty };
                var user = new IdentityUser { Email = userData.Email, UserName = userData.Email };
                var response = await authService.CreateUser(user, userData.Password);
                if (response.Succeeded)
                {
                    result = new CreateUserResponse
                    {
                        Succeeded = true,
                        Email = user.Email,
                        PasswordHash = user.PasswordHash ?? "",
                        CustomerId = Guid.Parse(user.Id)
                    };
                }
                return result.Succeeded ? Ok(result) : BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message });
            }
        }

        [HttpPost("find-user-by-email")]
        public async Task<IActionResult> FindUser([FromBody] GetUserByEmailDto userDetails)
        {
            try
            {
                var user = await authService.FindUserByEmail(userDetails.Email);
                return user != null ? Ok(user) : NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message });
            }
        }

        [HttpPut("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDto requestDetails)
        {
            try
            {
                var user = await authService.FindUserByEmail(requestDetails.Email);
                IdentityResult? result = null;
                if(user != null)
                {
                    var token = await authService.GeneratePasswordResetToken(user);
                    result = await authService.ResetPassword(user, token, requestDetails.Password);
                }
                return result != null && result.Succeeded ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message });
            }
        }

        [HttpPost("check-password")]
        public async Task<IActionResult> CheckPassword([FromBody] LoginRequestDto loginDetails)
        {
            try
            {
                var user = await authService.FindUserByEmail(loginDetails.Email);
                var result = false;
                if (user != null)
                {
                    result = await authService.CheckPassword(user, loginDetails.Password);
                }
                return result ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message });
            }
        }

        [HttpPost("get-roles")]
        public async Task<IActionResult> GetRoles([FromBody] GetUserByEmailDto userDetails)
        {
            try
            {
                var user = await authService.FindUserByEmail(userDetails.Email);
                IList<string>? roles = null;
                if (user != null)
                {
                    roles = await authService.GetRoles(user);
                }
                return roles!= null && roles.Count > 0 ? Ok(roles.ToList()) : BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message });
            }
        }

        [HttpPost("get-user-id")]
        public async Task<IActionResult> GetUserId([FromBody] GetUserByEmailDto userDetails)
        {
            try
            {
                var user = await authService.FindUserByEmail(userDetails.Email);
                return user != null ? Ok(new {Id = user.Id, Email = user.Email}) : BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message });
            }
        }

        [HttpPost("add-roles")]
        public async Task<IActionResult> AddRoles([FromBody] AddRolesRequestDto addRolesDetails)
        {
            try
            {
                var user = await authService.FindUserByEmail(addRolesDetails.Email);
                var identityResult = IdentityResult.Failed(new IdentityError { Description = "Failed to add Roles to the user" });
                if (user != null)
                {
                    identityResult = await authService.AddRoles(user, addRolesDetails.Roles);
                }
                return identityResult.Succeeded ? Ok() : BadRequest(identityResult);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message });
            }
        }

        [HttpPost("delete-user")]
        public async Task<IActionResult> DeleteUser([FromBody] GetUserByEmailDto userDetails)
        {
            try
            {
                var user = await authService.FindUserByEmail(userDetails.Email);
                var result = IdentityResult.Failed(new IdentityError { Description = "User Deletion Failed" });
                if (user != null)
                {
                    result = await authService.DeleteUser(user);
                }
                return result.Succeeded ? Ok() : BadRequest();
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message });
            }
        }

        [HttpPut("add-role/{customerId}")]
        public async Task<IActionResult> AddRole([FromRoute] Guid customerId)
        {
            try
            {
                var user = await authService.FindUserById(customerId);
                var result = IdentityResult.Failed(new IdentityError { Description = "Role addition failed..!!" });
                var role = new string[] { "Seller"};
                if (user != null)
                {
                    result = await authService.AddRoles(user, role);
                }
                return result.Succeeded ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message });
            }
        }
    }
}
