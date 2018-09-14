using KomunikatyRSO.Web.Infrastructure.DTO;
using KomunikatyRSO.Web.Infrastructure.Extensions;
using KomunikatyRSO.Web.Infrastructure.Settings;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KomunikatyRSO.Web.Infrastructure.Services
{
    public class JwtService
    {
        private readonly JwtSettings jwtSettings;

        public JwtService(JwtSettings jwtSettings)
        {
            this.jwtSettings = jwtSettings;
        }

        public JwtDto CreateToken(Guid userId)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.Now.AddMinutes(jwtSettings.ExpiryMinutes);

            var token = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                claims: claims,
                expires: expires,
                signingCredentials: creds);

            return new JwtDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expires = expires.ToTimestamp()
            };
        }
    }
}
