
using Business.Abstract;
using Business.Concrate;
using Business.MappinProfile;
using Business.Security.Abstarct;
using Business.Security.Concrate;
using Business.Validators.Auth;
using Business.Validators.Comment;
using DataAccess.Abstract;
using DataAccess.Concrate;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            

            // appsettings.json + environment vs. otomatik okunur
            var configuration = builder.Configuration;

            builder.Services.AddSingleton<IConfiguration>(configuration);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
           

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

                    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
                };
            });


            // Program.cs veya Startup.cs içinde


            builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

            builder.Services.AddScoped<ITokenService, TokenManager>();
            builder.Services.AddScoped<IAuthService, AuthManager>();
            
            builder.Services.AddScoped<ICommentService, CommentManager>();
            builder.Services.AddScoped<ICommentRepository, CommentRepository>();

            builder.Services.AddScoped<IPostService, PostManager>();
            builder.Services.AddScoped<IPostRepository, PostRepository>();

            builder.Services.AddScoped<IUserService, UserManager>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddAutoMapper(typeof(MappingProfile)); //Mapleme iþlemi gerçekleþtiriliyor.

            //FLUENT VALÝDATÝON
            // Program.cs içine þunlarý ekle:
          
            builder.Services.AddValidatorsFromAssemblyContaining<UserLoginDtoValidator>(); // OtelValidator otomatik bulunur
            builder.Services.AddFluentValidationAutoValidation();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
