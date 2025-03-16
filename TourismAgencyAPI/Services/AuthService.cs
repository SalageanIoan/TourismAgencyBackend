using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TourismAgencyAPI.Models;
using TourismAgencyAPI.Repositories;

namespace TourismAgencyAPI.Services;

public class AuthService(IUserRepository userRepository, IConfiguration configuration)
    : IAuthService
{
    public async Task<string> RegisterAsync(User user, string password)
    {
        var hashService = new HashService();
        user.Password = hashService.Hash(password);
        await userRepository.AddAsync(user);
        return GenerateJwtToken(user);
    }

    public async Task<string> LoginAsync(string email, string password)
    {
        var user = await userRepository.GetByEmailAsync(email);

        var hashService = new HashService();
        if (!hashService.Matches(password, user.Password))
            return null!;

        return GenerateJwtToken(user);
    }

    private string GenerateJwtToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? string.Empty));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var token = new JwtSecurityToken(
            configuration["Jwt:Issuer"],
            configuration["Jwt:Audience"],
            claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}