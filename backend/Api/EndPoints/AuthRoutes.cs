using Core.Models;
using Core.UseCases.Abstractions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.EndPoints;

public static class AuthRoutes
{
    public static WebApplication AddAuthRoutes(this WebApplication app)
    {
        var group = app.MapGroup("api/auth").WithTags("Auth");

        group.MapPost("login", (AuthenticationRequest request, IAuthUseCases authUseCases, IConfiguration configuration) =>
        {
            var utilisateur = authUseCases.Login(request);

            if (utilisateur is null)
            {
                return Results.Unauthorized();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, utilisateur.IdUtilisateur.ToString()),
                new Claim(ClaimTypes.Email, utilisateur.Email),
                new Claim(ClaimTypes.Name, $"{utilisateur.Prenom} {utilisateur.Nom}")
            };

            foreach (var role in utilisateur.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!) );
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Results.Ok(new
            {
                token = tokenString,
                utilisateur.IdUtilisateur,
                utilisateur.Nom,
                utilisateur.Prenom,
                utilisateur.Email,
                utilisateur.Roles
            });
        })
        .AllowAnonymous();

        return app;
    }
}