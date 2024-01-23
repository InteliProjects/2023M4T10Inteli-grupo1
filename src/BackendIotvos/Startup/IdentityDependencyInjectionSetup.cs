using BackendIotvos.Authentication.Interfaces;
using BackendIotvos.Authentication;
using Microsoft.AspNetCore.Identity;
using BackendIotvos.Authentication.Configurations;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using BackendIotvos.Authentication.Services;
using BackendIotvos.Authentication.Data;

namespace BackendIotvos.Startup
{
    public static class IdentityDependencyInjectionSetup
    {
        public static IServiceCollection RegisterIdentityServices(this IServiceCollection services)
        {
            services.AddDefaultIdentity<User>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IdentityDataContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IIdentityRepository, IdentityRepository>();
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }

        public static IServiceCollection RegisterJwtOptions(this IServiceCollection services, IConfigurationSection jwtAppSettingOptions, SymmetricSecurityKey securityKey)
        {
            services.Configure<JwtOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
                options.Expiration = int.Parse(jwtAppSettingOptions[nameof(JwtOptions.Expiration)] ?? "0");
            });

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
            });
            return services;
        }

        public static IServiceCollection RegisterJwtParameters(this IServiceCollection services, string issuer, string audience, SymmetricSecurityKey securityKey)
        {
            TokenValidationParameters tokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidIssuer = issuer,

                ValidateAudience = true,
                ValidAudience = audience,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = securityKey,

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = tokenValidationParameters;
            });


            return services;
        }
    }
}
