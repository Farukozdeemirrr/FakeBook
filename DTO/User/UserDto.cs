using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.User
{
    public class UserDto
    {
        public long Id { get; set; }
        public string FullName { get; set; } = null!;
        public string? ProfilePicture { get; set; }
        public string? Bio { get; set; }
        public UserRole Role { get; set; }
    }

}
