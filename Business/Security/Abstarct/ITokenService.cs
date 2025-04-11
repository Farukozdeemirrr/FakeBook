using DTO.User;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Security.Abstarct
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }

}
