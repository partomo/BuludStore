using Bazta.Identity.Application.Authorization;
using Bazta.Identity.Domain.Entities;
using BuludStore.Identity.Application;
using BuludStore.Identity.Domain.Entities;
using BuludStore.Identity.Domain.Repositories;
using BuludStore.Identity.Infrastructure;
using BuludStore.Identity.Infrastructure.Repositories;
using Bulud.Base.Extensions;
using Bulud.Base.Middlewares;
using BuludStore.Infrastructure;
using BuludStore.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using QuestPDF.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();
builder.Host.UseSerilog();


var config = builder.Configuration;

QuestPDF.Settings.License = LicenseType.Community;

builder.Services.AddScoped<IAuthorizationHandler, PermissionHandler>();
builder.Services.AddScoped<IUserRolesRepository, UserRolesRepository>();
builder.Services.AddCore(config);
builder.Services.AddBuludStoreIdentity(config);
builder.Services.AddMemoryCache();

builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddErrorDescriber<PersianIdentityErrorDescriber>()
    .AddDefaultTokenProviders();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Bazta API",
        Version = "v1",
        Description = "API documentation for Bazta project",
        Contact = new OpenApiContact
        {
            Name = "Bazta Support",
            Email = "support@Bazta.com",
            Url = new Uri("https://boostan.com")
        }
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token.\nExample: 'Bearer eyJhbGciOiJIU...'"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
    options.SchemaFilter<EnumSchemaFilter>();
});

builder.Services.AddJwtAuthentication(config);
builder.Services.AddControllers();

builder.Services.AddAuthorization();
builder.Services.AddCors(options =>
{
    options.AddPolicy("DevOrigins", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseStaticFiles();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
    var services = scope.ServiceProvider;
    await services.SeedIdentity();
}


// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("DevOrigins");

app.UseAuthentication();
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();


app.Run();
public class EnumSchemaFilter : Swashbuckle.AspNetCore.SwaggerGen.ISchemaFilter
{
    public void Apply(OpenApiSchema schema, Swashbuckle.AspNetCore.SwaggerGen.SchemaFilterContext context)
    {
        if (context.Type.IsEnum)
        {
            schema.Enum = Enum.GetNames(context.Type).Select(name => new OpenApiString(name)).ToList<IOpenApiAny>();
            schema.Type = "string"; // Show enum as string in Swagger
        }
    }
}
