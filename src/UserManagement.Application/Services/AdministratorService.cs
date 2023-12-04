using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UserManagement.Application.Contracts;
using UserManagement.Application.DTOs.Administrator;
using UserManagement.Application.Notifications;

namespace UserManagement.Application.Services;

public class AdministratorService : BaseService, IAdministratorService
{
    private readonly IConfiguration _configuration; 
    
    public AdministratorService(
        IMapper mapper, 
        INotificator notificator, 
        IConfiguration configuration) 
        : base(mapper, notificator)
    {
        _configuration = configuration;
    }
    
    public AdministratorTokenDto? Login(AdministratorLoginDto dto)
    {
        var tokenLogin = _configuration["Jwt:Login"];
        var tokenPassword = _configuration["Jwt:Password"];

        if (dto.Login == tokenLogin && dto.Password == tokenPassword) 
            return GenerateToken();

        Notificator.Handle("Login ou senha estão incorretos.");
        return null;
    }
    
    private AdministratorTokenDto GenerateToken()
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"] ?? string.Empty);

        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.Role, "User"),
                new(ClaimTypes.Name, _configuration["Jwt:Login"] ?? string.Empty)
            }),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Expires = DateTime.UtcNow.AddHours(int.Parse(_configuration["Jwt:HoursToExpire"] ?? string.Empty))
        });

        var encodedToken = tokenHandler.WriteToken(token);

        return new AdministratorTokenDto
        { 
            Token = encodedToken
        };
    }
}