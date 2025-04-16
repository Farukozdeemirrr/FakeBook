using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Security.Abstarct
{
    public interface IUserClaim
    {
        public long UserId { get; }
        public string Role { get; }
        public string? Email { get; }
    }
}
