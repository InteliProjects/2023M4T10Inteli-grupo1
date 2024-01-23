using BackendIotvos.Authentication.Configurations;
using BackendIotvos.Authentication.DTOs;
using BackendIotvos.Authentication.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BackendIotvos.Authentication.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtOptions _jwtOptions;
        private readonly UserManager<User> _userManager;

        public TokenService(IOptions<JwtOptions> jwtOptions, UserManager<User> userManager)
        {
            _jwtOptions = jwtOptions.Value;
            _userManager = userManager;
        }

        public async Task<TokenResponseDto> GenerateToken(User user)
        {
            IList<Claim> tokenClaims = await GetClaims(user);

            DateTime expireDate = DateTime.Now.AddSeconds(_jwtOptions.Expiration);

            JwtSecurityToken jwt = new(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: tokenClaims,
                notBefore: DateTime.Now,
                expires: expireDate,
                signingCredentials: _jwtOptions.SigningCredentials);

            string token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new TokenResponseDto(token, expireDate);
        }

        private async Task<IList<Claim>> GetClaims(User user)
        {
            IList<Claim> claims = await _userManager.GetClaimsAsync(user);
            IList<string> roles = await _userManager.GetRolesAsync(user);

            //Claims padrões para ter no token
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, DateTime.Now.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()));

            foreach (string role in roles)
                claims.Add(new Claim("role", role));

            return claims;
        }
    }
}
