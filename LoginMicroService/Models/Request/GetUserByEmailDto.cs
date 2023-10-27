using System.ComponentModel.DataAnnotations;

namespace AuthMicroService.Models.Request
{
    public class GetUserByEmailDto
    {
        [Required]
        public string Email { get; set; } = null!;
    }
}
