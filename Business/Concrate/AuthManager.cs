using AutoMapper;
using Business.Abstract;
using Business.Security.Abstarct;
using Business.Validators.Auth;
using DataAccess.Abstract;
using DTO.Auth;
using DTO.User;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrate
{
   
    public class AuthManager : IAuthService
    {

        private IUserRepository _userRepository;
        private IMapper _mapper;
        private ITokenService _tokenService;
        private readonly UserLoginDtoValidator _userLoginDtoValidator;
        private readonly UserRegisterDtoValidator _userRegisterDtoValidator;
        private IPasswordHasher _passwordHasher; 

        public AuthManager(
            IUserRepository userRepository,
            IMapper mapper,
            UserLoginDtoValidator userLoginDtoValidator,
            UserRegisterDtoValidator userRegisterDtoValidator,
            ITokenService tokenService,
            IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userLoginDtoValidator = userLoginDtoValidator;
            _userRegisterDtoValidator = userRegisterDtoValidator;
            _tokenService = tokenService;
            _passwordHasher = passwordHasher;
        }


        public AuthResponseDto Login(long id)
        {
            using (var context = new FakeBookDbContext())
            {
                var user = _userRepository.GetById(context, id);

                if (user == null)
                    throw new Exception("Kullanıcı bulunamadı.");

                // AuthResponseDto'ya maplemek gerekiyor
                return _mapper.Map<AuthResponseDto>(user);
            }
        }


        public AuthResponseDto Register(UserRegisterDto userRegisterDto)
        {
            using (var context = new FakeBookDbContext())
            {
                

                // DTO -> Entity
                var user = _mapper.Map<User>(userRegisterDto);

                // Şifre hashle
                user.Password = _passwordHasher.HashPassword(userRegisterDto.Password);
                user.CreatedAt = DateTime.UtcNow;
                user.userRole = userRegisterDto.Role ?? UserRole.User;

                // Kaydet
                _userRepository.Add(context, user);
                context.SaveChanges(); // Veritabanına işle

                // Token oluştur
                var token = _tokenService.GenerateToken(user);

                return new AuthResponseDto
                {
                    Token = token,
                    FullName = $"{user.FirstName} {user.LastName}",
                    UserId = user.Id,
                    Role = user.userRole
                };
            }
        }


        public AuthResponseDto Login(UserLoginDto userLoginDto)
        {
            using (var context = new FakeBookDbContext())
            {
                var user = _userRepository
                    .GetAll(context)
                    .FirstOrDefault(x => x.Email == userLoginDto.Email);

                if (user == null)
            throw new Exception("Kullanıcı bulunamadı.");

        // 2. Şifre doğrulama
        var isPasswordValid = _passwordHasher.VerifyPassword(userLoginDto.Password, user.Password);
        if (!isPasswordValid)
            throw new Exception("Geçersiz şifre.");

        // 3. Token oluştur
        var token = _tokenService.GenerateToken(user);

        // 4. DTO oluştur ve dön
        return new AuthResponseDto
        {
            Token = token,
            FullName = $"{user.FirstName} {user.LastName}",
            UserId = user.Id,
            Role = user.userRole
        };
            }
        }
    }
}
