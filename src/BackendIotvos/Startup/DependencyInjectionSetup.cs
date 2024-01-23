using BackendIotvos.Repository.Interfaces;
using BackendIotvos.Repository;
using BackendIotvos.Services.Interfaces;
using BackendIotvos.Services;
using Microsoft.OpenApi.Models;

namespace BackendIotvos.Startup
{
    public static class DependencyInjectionSetup
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddControllers();

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"Cabeçalho de autorização JWT usando o esquema Bearer.
                        Insira 'Bearer' [espaço] e, em seguida, seu token no campo de texto abaixo.
                        Exemplo: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });
            });

            services.AddAutoMapper(typeof(Program).Assembly);

            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IItemService, ItemService>();

            services.AddScoped<IOrdemServicoRepository, OrdemServicoRepository>();
            services.AddScoped<IOrdemServicoService, OrdemServicoService>();

            services.AddScoped<IConjuntoRepository, ConjuntoRepository>();
            services.AddScoped<IConjuntoService, ConjuntoService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            return services;
        }
    }
}
