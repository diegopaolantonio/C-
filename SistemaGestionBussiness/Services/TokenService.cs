using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

public class TokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(string username, string role)
    {
        // Crear los claims (información del usuario en el token)
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(ClaimTypes.Role, role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        // Crear la clave de seguridad
        var secretKey = _configuration["JwtSettings:Key"];
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        int TokenValidityInMinutes = int.Parse(_configuration["JwtSettings:DurationInMinutes"]);
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Crear el token
        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:issuer"],
            audience: _configuration["JwtSettings:audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(TokenValidityInMinutes),
            signingCredentials: credentials
        );

        // Retornar el token como string
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public void ProcessToken(string token)
    {
        try
        {
            var claimsPrincipal = GetClaimsFromToken(token);

            // Extraer información de los claims
            var username = claimsPrincipal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            var role = claimsPrincipal.FindFirst(ClaimTypes.Role)?.Value;

            Console.WriteLine($"Username: {username}");
            Console.WriteLine($"Role: {role}");
        }
        catch (SecurityTokenException ex)
        {
            Console.WriteLine($"Error al procesar el token: {ex.Message}");
        }
    }

    public ClaimsPrincipal GetClaimsFromToken(string token)
    {
        var secretKey = _configuration["JwtSettings:Key"];
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _configuration["JwtSettings:issuer"],
            ValidAudience = _configuration["JwtSettings:audience"],
            IssuerSigningKey = key,
            ClockSkew = TimeSpan.Zero // Para evitar margen de tiempo en la expiración
        };

        try
        {
            // Validar el token y obtener los claims
            var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
            return principal; // Devuelve los claims si el token es válido
        }
        catch (Exception ex)
        {
            // Manejar errores de validación
            throw new SecurityTokenException("Token inválido o expirado.", ex);
        }
    }

    public bool ValidateToken(string token, out JwtSecurityToken validatedToken)
    {
        validatedToken = null;

        var secretKey = _configuration["JwtSettings:Key"];
        var key = Encoding.UTF8.GetBytes(secretKey);

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = _configuration["JwtSettings:Issuer"],

            ValidateAudience = true,
            ValidAudience = _configuration["JwtSettings:Audience"],

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),

            ValidateLifetime = true, // Validar que no esté expirado
            ClockSkew = TimeSpan.Zero // Sin tolerancia para tokens expirados
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            // Validar el token
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            // Verificar que el token es de tipo JWT
            if (securityToken is JwtSecurityToken jwtToken)
            {
                validatedToken = jwtToken;
            }

            return true; // Token válido
        }
        catch (SecurityTokenExpiredException)
        {
            Console.WriteLine("El token está expirado.");
            return false; // Token expirado
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al validar el token: {ex.Message}");
            return false; // Token inválido
        }
    }
}
