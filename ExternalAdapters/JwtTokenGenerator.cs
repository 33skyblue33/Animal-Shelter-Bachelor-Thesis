using Domain.Entities;
using Domain.Ports;
using Domain.Util;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ExternalAdapters
{
    internal class JwtTokenGenerator : ITokenGenerator
    {
        private readonly IConfiguration _configuration;

        public JwtTokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateAccessToken(User user)
        {
            string secret = _configuration["Jwt:Secret"]
                ?? throw new InvalidOperationException("JWT Secret is not configured.");

            string issuer = _configuration["Jwt:Issuer"]
                ?? throw new InvalidOperationException("JWT Issuer is not configured.");

            string audience = _configuration["Jwt:Audience"]
                ?? throw new InvalidOperationException("JWT Audience is not configured.");

            string expirationMinutesRaw = _configuration["Jwt:AccessTokenExpirationMinutes"]
                ?? throw new InvalidOperationException("JWT Access Token Expiration is not configured.");

            if (!int.TryParse(expirationMinutesRaw, out int expirationMinutes))
            {
                throw new InvalidOperationException("Invalid format for JWT Access Token Expiration.");
            }

            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(secret));
            SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(expirationMinutes),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = credentials
            };

            JwtSecurityTokenHandler tokenHandler = new();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public RefreshToken GenerateRefreshToken(User user)
        {
            string expirationDaysRaw = _configuration["Jwt:RefreshTokenExpirationDays"]
                ?? throw new InvalidOperationException("JWT Refresh Token Expiration is not configured.");

            if (!int.TryParse(expirationDaysRaw, out int expirationDays))
            {
                throw new InvalidOperationException("Invalid format for JWT Refresh Token Expiration.");
            }

            byte[] randomNumber = new byte[64];
            using RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);

            return new RefreshToken
            {
                UserId = user.Id,
                Token = Convert.ToBase64String(randomNumber),
                Expires = DateTime.UtcNow.AddDays(expirationDays),
                Revoked = null
            };
        }

        public VerificationToken GenerateVerificationToken(User user)
        {
            string expirationHoursRaw = _configuration["Jwt:VerificationTokenExpirationHours"]
                ?? throw new InvalidOperationException("JWT Verification Token Expiration is not configured.");

            if (!int.TryParse(expirationHoursRaw, out int expirationHours))
            {
                throw new InvalidOperationException("Invalid format for JWT Verification Token Expiration.");
            }

            return new VerificationToken
            {
                UserId = user.Id,
                Token = Guid.NewGuid().ToString("N"),
                Expires = DateTime.UtcNow.AddHours(expirationHours),
                Revoked = null
            };
        }
    }
}