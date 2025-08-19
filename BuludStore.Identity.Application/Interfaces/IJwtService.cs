using System.Security.Claims;
using Bazta.Identity.Domain.Entities;

namespace Bazta.Identity.Application.Interfaces;

public interface IJwtService
{
    string GenerateToken(AppUser user, List<Claim>? additionalClaims = null);
}