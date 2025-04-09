using DTO.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthService
    {
        AuthResponseDto Login(UserLoginDto userLoginDto);
        AuthResponseDto Register(UserRegisterDto userRegisterDto);
    }
}
