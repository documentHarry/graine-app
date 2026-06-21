using Core.Models;
using Core.UseCases.Abstractions;

namespace Api.EndPoints;

public static class UtilisateurRoutes
{
    public static WebApplication AddUtilisateurRoutes(this WebApplication app)
    {
        var group = app.MapGroup("api/utilisateurs")
            .WithTags("Utilisateurs")
            .RequireAuthorization(policy => policy.RequireRole("ADMIN"));

        group.MapGet("", (IUtilisateurUseCases utilisateurUseCases) =>
        {
            var utilisateurs = utilisateurUseCases.GetAllUtilisateurs();
            return Results.Ok(utilisateurs);
        })
        .WithName("GetAllUtilisateurs")
        .Produces<IEnumerable<Utilisateur>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status500InternalServerError);

        group.MapGet("{id:int}", (int id, IUtilisateurUseCases utilisateurUseCases) =>
        {
            var utilisateur = utilisateurUseCases.GetUtilisateurById(id);
            if (utilisateur is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(utilisateur);
        })
        .WithName("GetUtilisateurById")
        .Produces<Utilisateur>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status500InternalServerError);

        group.MapPost("", (UtilisateurCreateRequest request, IUtilisateurUseCases utilisateurUseCases) =>
        {
            utilisateurUseCases.AddUtilisateur(request);
            return Results.Created();
        })
        .WithName("AddUtilisateur")
        .Produces(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status500InternalServerError);

        group.MapPut("{id:int}", (int id, UtilisateurUpdateRequest request, IUtilisateurUseCases utilisateurUseCases) =>
        {
            var updated = utilisateurUseCases.UpdateUtilisateur(id, request);
            if (!updated)
            {
                return Results.NotFound();
            }
            return Results.NoContent();
        })
        .WithName("UpdateUtilisateur")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status500InternalServerError);

        group.MapDelete("{id:int}", (int id, IUtilisateurUseCases utilisateurUseCases) =>
        {
            var deleted = utilisateurUseCases.DeleteUtilisateur(id);
            if (!deleted)
            {
                return Results.NotFound();
            }
            return Results.NoContent();
        })
        .WithName("DeleteUtilisateur")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status500InternalServerError);

        return app;
    }
}