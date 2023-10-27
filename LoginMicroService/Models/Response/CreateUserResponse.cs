namespace AuthMicroService.Models.Response
{
    public class CreateUserResponse
    {
        public bool Succeeded { get; set; }
        public Guid CustomerId { get; set; }
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
    }
}
