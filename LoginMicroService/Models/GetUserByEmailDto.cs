using System.ComponentModel.DataAnnotations;

namespace AuthMicroService.Models
{
    public class GetUserByEmailDto
    {
        [Required]
        public string Email { get; set; } = null!;
    }
}
