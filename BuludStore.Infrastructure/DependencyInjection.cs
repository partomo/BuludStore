using Bulud.Base.Exporters;
using Bulud.Base.Infrastructure;
using Bulud.Base.Services;
using Bulud.Communication.Email;
using Bulud.communication.sms.kavenegar;
using Bulud.FileStorage.S3;
using Bulud.Security.InMemoryOtp;
using BuludStore.Application.Interfaces;
using BuludStore.Application.Mapping;
using BuludStore.Application.Queries.Provinces;
using BuludStore.Application.Services;
using BuludStore.Infrastructure.Persistence;
using BuludStore.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Minio;

namespace BuludStore.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration
                .GetConnectionString("SqlConnectionString") ?? throw new InvalidOperationException("Connection string 'SqlConnectionString' not found.");

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped(typeof(IRepository<>),typeof(BaseRepository<>));
            services.AddSingleton<IMinioClient>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<MinioSettings>>().Value;

                return new MinioClient()
                    .WithEndpoint(settings.Endpoint)
                    .WithCredentials(settings.AccessKey, settings.SecretKey)
                    .WithSSL(settings.UseSSL)
                    .Build();
            });
            
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetProvincesListQuery).Assembly));
            
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.Configure<OtpSettings>(configuration.GetSection("OtpSettings"));
            services.Configure<MinioSettings>(configuration.GetSection("MinioSettings"));
            services.AddScoped<IFilesService, S3FileService>();
            services.AddScoped<IOtpService, InMemoryOtpService>();
            services.AddScoped<ISmsService, KavenegarService>();
            services.AddScoped<IAnnouncementService, AnnouncementService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ICurrentUserContext, CurrentUserContext>();
            services.AddScoped<IExporter, CsvExporter>();
            services.AddScoped<IExporter, PdfExporter>();
            services.AddScoped<ExportManager>();
            return services;
        }
        
    }
}
