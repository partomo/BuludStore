using Bazta.Identity.Application.Commands.Users;
using Bazta.Identity.Application.Interfaces;
using Bazta.Identity.Application.Mapping;
using Bazta.Identity.Domain.Entities;
using Bazta.Identity.Domain.Repositories;
using Bazta.Identity.Infrastructure.Repositories;
using Bazta.Identity.Infrastructure.Services;
using Bulud.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bazta.Identity.Infrastructure
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
                new AppRole { Name = "SuperAdmin", DisplayName = "مدیر کشوری" },
                new AppRole { Name = "ProvinceAdmin", DisplayName = "مدیر استانی" },
                new AppRole { Name = "CountyAdmin", DisplayName = "مدیر شهرستان" },
            };
            
            foreach (var role in roles)
                if (!await roleManager.RoleExistsAsync(role.Name!))
                {
                   await roleManager.CreateAsync(role);
                }

            var users = new List<AppUser>
            {
                new AppUser { FirstName = "شایان", LastName = "خجسته منش", UserName ="09145065451" ,PhoneNumber = "09145065451", NationalCode = "1720129411" },
                new AppUser { FirstName = "مهران", LastName = "نوین", UserName ="11111111111" ,PhoneNumber = "11111111111", NationalCode = "1111111111" },
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