using Microsoft.AspNetCore.Identity;

namespace AuthMicroService.Models.Request
{
    public class ResetPasswordRequestDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
