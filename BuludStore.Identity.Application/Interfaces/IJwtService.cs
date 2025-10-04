using System.Security.Claims;
using Bazta.Identity.Domain.Entities;
using BuludStore.Identity.Domain.Entities;

namespace BuludStore.Identity.Application.Interfaces;

public interface IJwtService
{
    string GenerateToken(AppUser user, List<Claim>? additionalClaims = null);
}