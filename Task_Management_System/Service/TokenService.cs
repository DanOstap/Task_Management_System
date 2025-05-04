using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Task_Management_System.Models;

namespace Task_Management_System.Service;


public interface ITokenService
{
    string CreateToken(Users user);
}
public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string CreateToken( Users user)
    {
        List<Claim> claims = new List<Claim>();
        {
            new Claim("email", user.User_Email);
        }
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(_configuration["JWT_TOKEN_ISSUER"],
            _configuration["JWT_TOKEN_ISSUER"],
            claims : claims,
            expires: DateTime.Now.AddHours(double.Parse(_configuration["JWT_TOKEN_EXPIRY"])),
            signingCredentials: creds);
        var JWToken = new  JwtSecurityTokenHandler().WriteToken(token);
        return JWToken;
    }
}