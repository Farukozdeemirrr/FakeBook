using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? ProfilePicture { get; set; }
        public string? Bio { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public UserRole userRole { get; set; }  

        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }

    public enum UserRole
    {
        User = 0,
        Admin = 1
    }

}
