using Bazta.Identity.Domain.Entities;
using BuludStore.Identity.Domain.Repositories;
using BuludStore.Identity.Infrastructure.Repositories;
using Bulud.Base;
using BuludStore.Identity.Application.Commands.Users;
using BuludStore.Identity.Application.Interfaces;
using BuludStore.Identity.Application.Mapping;
using BuludStore.Identity.Domain.Entities;
using BuludStore.Identity.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuludStore.Identity.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBuludStoreIdentity(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(LoginUserCommand).Assembly));
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            services.AddTransient<IJwtService, JwtService>();
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            return services;
        }

        public static async Task SeedIdentity(this IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<AppUser>>();
            var roleManager = services.GetRequiredService<RoleManager<AppRole>>();

            var roles = new List<AppRole>
            {
                new AppRole { Name = "Programmer", DisplayName = "برنامه نویس" },
                new AppRole { Name = "Admin", DisplayName = "ادمین" },
                new AppRole { Name = "User", DisplayName = "کاربر" },
            };
            
            foreach (var role in roles)
                if (!await roleManager.RoleExistsAsync(role.Name!))
                {
                   await roleManager.CreateAsync(role);
                }

            var users = new List<AppUser>
            {
                new AppUser { FirstName = "محمد", LastName = "پرتونیا", UserName ="09388799476" ,PhoneNumber = "09388799476", NationalCode = "1720268355" },
                new AppUser { FirstName = "شایان", LastName = "خجسته منش", UserName ="09145065451" ,PhoneNumber = "09145065451", NationalCode = "1111111111" },
            };

            for (int i = 0; i < users.Count; i++)
            {
                var user = users[i];
                var adminUser = await userManager.FindByNameAsync(user.UserName!);

                if (adminUser == null)
                {
                    var result = await userManager.CreateAsync(user, "Admin@12345");

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, roles[i].Name!);
                    }
                    else
                    {
                        throw new Exception("Failed to seed Identity data");
                    }
                }
            }
        }
    }
}