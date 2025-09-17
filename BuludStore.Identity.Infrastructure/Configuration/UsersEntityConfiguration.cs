using Bazta.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bazta.Identity.Infrastructure.Configuration;

public class UsersEntityConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.ToTable("Users");
        builder.HasIndex(x => x.UserName)
            .IsUnique();
        builder.HasIndex(x=>x.PhoneNumber)
            .IsUnique();
        builder.HasIndex(x=>x.NationalCode)
            .IsUnique();
            
    }
}