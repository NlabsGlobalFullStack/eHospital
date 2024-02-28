using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DataAccess.Options;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DataAccess.Services;
public class JwtProvider(UserManager<AppUser> userManager, IOptions<JwtOptions> options)
{
    public async Task<LoginResponseDto> CreateToken(AppUser user, bool rememberMe)
    {
        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.FullName),
            new Claim(ClaimTypes.Email, user.Email ?? ""),
            new Claim("UserName", user.UserName ?? ""),
        };

        var expires = rememberMe ? DateTime.UtcNow.AddMonths(1) : DateTime.UtcNow.AddHours(1);

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.SecretKey));
        JwtSecurityToken jwtSecurityToken = new(
            issuer: options.Value.ToString(),
            audience: options.Value.ToString(),
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: expires,
            signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512)
            );

        JwtSecurityTokenHandler handler = new();
        var token = handler.WriteToken(jwtSecurityToken);
        var refreshToken = Guid.NewGuid().ToString();
        var refreshTokenExpires = expires.AddHours(1);
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpires = refreshTokenExpires;
        await userManager.UpdateAsync(user);
        return new(token, refreshToken, refreshTokenExpires);       
    }
}
