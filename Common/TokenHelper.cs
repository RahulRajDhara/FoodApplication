using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Common
{
    public static class TokenHelper
    {
        public static ClaimsPrincipal ValidateAndReadToken(HttpRequest request, ILogger logger)
        {
            string token = request.Cookies["AuthToken"];
            if (string.IsNullOrEmpty(token))
                throw new UnauthorizedAccessException("AuthToken cookie missing");

            var handler = new JwtSecurityTokenHandler();
            if (!handler.CanReadToken(token))
                throw new ArgumentException("Token cannot be read as JWT: " + token);

            var jwtToken = handler.ReadJwtToken(token);

            // Check expiration manually
            var expClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "exp")?.Value;
            if (expClaim == null)
                throw new UnauthorizedAccessException("Token does not contain 'exp' claim");

            var expUnix = long.Parse(expClaim);
            var expiryDateUtc = DateTimeOffset.FromUnixTimeSeconds(expUnix).UtcDateTime;

            if (DateTime.UtcNow > expiryDateUtc)
                throw new UnauthorizedAccessException("Session expired. Please login again.");

            logger.LogInformation("Token is valid. Expires at {ExpiryDate}", expiryDateUtc);

            return new ClaimsPrincipal(new ClaimsIdentity(jwtToken.Claims, "jwt"));
        }

        public static string ExtractToken(HttpRequest request)
        {
            // 1. Try Authorization header
            string authHeader = request.Headers["Authorization"];
            if (!string.IsNullOrEmpty(authHeader))
            {
                return authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase)
                    ? authHeader.Substring("Bearer ".Length).Trim()
                    : authHeader.Trim();
            }

            // 2. Try cookie
            var cookie = request.Cookies["AuthToken"];
            if (cookie != null && !string.IsNullOrEmpty(cookie))
            {
                return cookie.Trim();
            }
            throw new UnauthorizedAccessException("Authorization header or AuthToken cookie missing");
        }
    }
}
