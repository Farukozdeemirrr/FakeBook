using Entities;

namespace DTO.Auth
{
    public class AuthResponseDto
    {
        public string Token { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public long UserId { get; set; }
        public UserRole Role { get; set; }
    }

}
