using Business.Security.Abstarct;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Security.Concrate
{
    public class UserClaim : IUserClaim
    {
        public long UserId { get; }
        public string Role { get; }
        public string? Email { get; }

        public UserClaim(IHttpContextAccessor httpContextAccessor)
        {
            var user = httpContextAccessor.HttpContext?.User;

            if (user == null || !user.Identity?.IsAuthenticated == true)
                throw new UnauthorizedAccessException("Kullanıcı doğrulanmamış.");

            UserId = long.Parse(user.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            Role = user.FindFirst(ClaimTypes.Role)!.Value;
            Email = user.FindFirst(ClaimTypes.Email)?.Value;
        }
    }
}
