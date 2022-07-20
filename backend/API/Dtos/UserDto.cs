namespace API.Dtos
{
    public class UserDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public CartDto Cart { get; set; }
    }
}
