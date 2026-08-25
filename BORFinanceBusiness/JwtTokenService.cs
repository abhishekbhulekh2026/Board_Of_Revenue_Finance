using BORFinanceCommon.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using BORFinanceCommon.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace BORFinanceBusiness
{
    public interface IJwtTokenService
    {
        TokenResponse GenerateToken(
            long userId,
            string username,
            string fullName,
            int roleId,
            string roleCode,
            string roleName);
    }

    public class JwtTokenService : IJwtTokenService
    {
        private readonly JwtSettings _jwtSettings;

        public JwtTokenService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public TokenResponse GenerateToken(
            long userId,
            string username,
            string fullName,
            int roleId,
            string roleCode,
            string roleName)
        {
            var now = DateTime.UtcNow;

            var accessTokenExpiresAt =
                now.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes);

            var refreshTokenExpiresAt =
                now.AddDays(_jwtSettings.RefreshTokenExpirationDays);

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Name, username),

            new Claim(Claims.UserId, userId.ToString()),
            new Claim(Claims.Username, username),
            new Claim(Claims.FullName, fullName),

            new Claim(Claims.RoleId, roleId.ToString()),
            new Claim(Claims.RoleCode, roleCode),
            new Claim(Claims.RoleName, roleName),

            new Claim(ClaimTypes.Role, roleCode),

        };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                notBefore: now,
                expires: accessTokenExpiresAt,
                signingCredentials: credentials);

            var accessToken =
                new JwtSecurityTokenHandler().WriteToken(token);

            var refreshToken = GenerateRefreshToken();

            return new TokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                AccessTokenExpiresAt = accessTokenExpiresAt,
                RefreshTokenExpiresAt = refreshTokenExpiresAt
            };
        }

        private static string GenerateRefreshToken()
        {
            var randomBytes = RandomNumberGenerator.GetBytes(64);

            return Convert.ToBase64String(randomBytes);
        }
    }
}
