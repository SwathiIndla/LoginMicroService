namespace AuthMicroService.Models.Request
{
    public class AddRolesRequestDto
    {
        public string Email { get; set; } = null!;
        public string[] Roles { get; set; } = null!;
    }
}
