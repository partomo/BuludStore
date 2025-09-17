using Bazta.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bazta.Identity.Infrastructure.Configuration;

public class UserRolesEntityConfigurations : IEntityTypeConfiguration<AppUserRole>
{
    public void Configure(EntityTypeBuilder<AppUserRole> builder)
    {
        builder.ToTable("UserRoles");
        builder.HasOne<AppUser>(x=>x.User)
            .WithMany(x=>x.UserRoles)
            .HasForeignKey(x=>x.UserId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne<AppRole>(x=>x.Role)
            .WithMany()
            .HasForeignKey(x=>x.RoleId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
    }
}