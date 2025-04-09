using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Auth
{
    public class AuthResponseDto
    {
        public string Token { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public long UserId { get; set; }
    }

}
