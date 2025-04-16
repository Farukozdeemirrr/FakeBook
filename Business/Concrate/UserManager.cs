using AutoMapper;
using Business.Abstract;
using DataAccess.Abstract;
using DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrate
{
    public class UserManager : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserManager(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public List<UserDto> GetAllUser()
        {
            using (var context = new FakeBookDbContext())
            {
                var users = _userRepository.GetAll(context);
                return _mapper.Map<List<UserDto>>(users);
            }
        }
        public UserDto GetByUserId(long id)
        {
            using (var context = new FakeBookDbContext())
            {
                var user = _userRepository.GetById(context, id);
                if (user == null)
                    throw new Exception("Kullanıcı bulunamadı");

                return _mapper.Map<UserDto>(user);
            }
        }
        public UserDto UserDelete(long id)
        {
            using (var context = new FakeBookDbContext())
            {
                var user = _userRepository.GetById(context, id);
                if (user == null)
                    throw new Exception("Kullanıcı bulunamadı");

                _userRepository.Delete(context, id); //  Sadece id ile sil
                context.SaveChanges();

                return _mapper.Map<UserDto>(user); //  Silinen bilgiyi döndür
            }
        }


        public UserDto UserUpdate(long id, UserUpdateDto userUpdate)
        {
            using (var context = new FakeBookDbContext())
            {
                var user = _userRepository.GetById(context, id);
                if (user == null)
                    throw new Exception("Kullanıcı bulunamadı");

                //güncelleme
                _mapper.Map(userUpdate, user);

                _userRepository.Update(context, user);
                context.SaveChanges();

                return _mapper.Map<UserDto>(user);
            }

        }
    }

}
