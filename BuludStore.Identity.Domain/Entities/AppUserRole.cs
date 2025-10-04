using Bazta.Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace BuludStore.Identity.Domain.Entities;

public class AppUserRole : IdentityUserRole<string>
{
    public AppUser? User { get; set; }
    public AppRole? Role { get; set; }
}