using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Bazta.Identity.Domain.Entities;
using Bulud.Base;
using BuludStore.Identity.Application.Interfaces;
using BuludStore.Identity.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BuludStore.Identity.Infrastructure.Services;

public class JwtService(IOptions<JwtSettings> settings) : IJwtService
{
    public string GenerateToken(AppUser user, List<Claim>? additionalClaims = null)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Value.SecretKey));

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(ClaimTypes.NameIdentifier, user.Id!),
            new Claim("fullname", user.FirstName + " " + user.LastName)
        };
        
        if (additionalClaims != null)
            claims.AddRange(additionalClaims);
        
        var token = new JwtSecurityToken(
            issuer: settings.Value.Issuer,
            audience: settings.Value.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(settings.Value.ExpireMinutes),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}