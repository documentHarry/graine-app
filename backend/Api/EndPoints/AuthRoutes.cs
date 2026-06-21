using Core.Models;
using Core.UseCases.Abstractions;

namespace Api.EndPoints;

public static class AuthRoutes
{
    public static WebApplication AddAuthRoutes(this WebApplication app)
    {
        var group = app.MapGroup("api/auth").WithTags("Auth");

        group.MapPost("login", (AuthenticationRequest request, IAuthUseCases authUseCases) =>
        {
            var utilisateur = authUseCases.Login(request);
            if (utilisateur is null)
            {
                return Results.Unauthorized();
            }
            return Results.Ok(utilisateur);
        });

        return app;
    }
}