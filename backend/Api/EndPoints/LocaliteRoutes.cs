using Core.Models;
using Core.UseCases.Abstractions;

namespace Api.EndPoints;

public static class LocaliteRoutes
{
    public static WebApplication AddLocaliteRoutes(this WebApplication app)
    {
        var group = app.MapGroup("api/localites").WithTags("Localites");

        group.MapGet("", (ILocaliteUseCases localiteUseCases) =>
        {
            var localites = localiteUseCases.GetAllLocalites();
            return Results.Ok(localites);
        })
        .WithName("GetAllLocalites")
        .Produces<IEnumerable<Localite>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status500InternalServerError);

        group.MapGet("{id:int}", (int id, ILocaliteUseCases localiteUseCases) =>
        {
            var localite = localiteUseCases.GetLocaliteById(id);
            if (localite is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(localite);
        })
        .WithName("GetLocaliteById")
        .Produces<Localite>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status500InternalServerError);

        group.MapPost("", (LocaliteCreateRequest request, ILocaliteUseCases localiteUseCases) =>
        {
            localiteUseCases.AddLocalite(request);
            return Results.Created();
        })
        .RequireAuthorization(policy => policy.RequireRole("ADMIN"))
        .WithName("AddLocalite")
        .Produces(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status500InternalServerError);

        group.MapPut("{id:int}", (int id, LocaliteUpdateRequest request, ILocaliteUseCases localiteUseCases) =>
        {
            var updated = localiteUseCases.UpdateLocalite(id, request);
            if (!updated)
            {
                return Results.NotFound();
            }
            return Results.NoContent();
        })
        .RequireAuthorization(policy => policy.RequireRole("ADMIN"))
        .WithName("UpdateLocalite")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status500InternalServerError);

        group.MapDelete("{id:int}", (int id, ILocaliteUseCases localiteUseCases) =>
        {
            var deleted = localiteUseCases.DeleteLocalite(id);
            if (!deleted)
            {
                return Results.NotFound();
            }
            return Results.NoContent();
        })
        .RequireAuthorization(policy => policy.RequireRole("ADMIN"))
        .WithName("DeleteLocalite")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status500InternalServerError);

        return app;
    }
}