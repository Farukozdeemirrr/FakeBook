using DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        UserDto GetByUserId(long id);
        List<UserDto> GetAllUser();
        UserDto UserUpdate(long id, UserUpdateDto userUpdate);
    }
}
