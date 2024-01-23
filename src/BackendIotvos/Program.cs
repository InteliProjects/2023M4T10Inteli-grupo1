using BackendIotvos.Data.Context;
using Microsoft.EntityFrameworkCore;
using BackendIotvos.Authentication.Configurations;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using BackendIotvos.Startup;
using BackendIotvos.Authentication.Data;

var builder = WebApplication.CreateBuilder(args);

#region Configure Services

builder.Services.RegisterServices();

builder.Configuration.AddJsonFile("appsettings.json");

builder.Services.AddDbContext<IoTvosContext>(
    o => o.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
    );
#endregion

#region Configure Identity
builder.Services.RegisterIdentityServices();

builder.Services.AddDbContext<IdentityDataContext>(
    o => o.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
    );

// Configure authentication
IConfigurationSection jwtAppSettingOptions = builder.Configuration.GetSection(nameof(JwtOptions));
SymmetricSecurityKey securityKey = new(Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JwtOptions:SecurityKey").Value));

builder.Services.RegisterJwtOptions(jwtAppSettingOptions, securityKey);

builder.Services.RegisterJwtParameters(builder.Configuration.GetSection("JwtOptions:Issuer").Value, 
    builder.Configuration.GetSection("JwtOptions:Audience").Value, 
    securityKey);
#endregion

WebApplication app = builder.Build();

app.UseCors("CorsPolicy");

app.ConfigureSwagger();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
