using Core.Models;
using Core.UseCases.Abstractions;

namespace Api.EndPoints;

public static class VarieteRoutes
{
    public static WebApplication AddVarieteRoutes(this WebApplication app)
    {
        var group = app.MapGroup("api/varietes").WithTags("Varietes");

        group.MapGet("", (IVarieteUseCases varieteUseCases) =>
        {
            var varietes = varieteUseCases.GetAllVarietes();
            return Results.Ok(varietes);
        })
        .WithName("GetAllVarietes")
        .Produces<IEnumerable<Variete>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status500InternalServerError);

        group.MapGet("{id:int}", (int id, IVarieteUseCases varieteUseCases) =>
        {
            var variete = varieteUseCases.GetVarieteById(id);
            if (variete is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(variete);
        })
        .WithName("GetVarieteById")
        .Produces<Variete>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status500InternalServerError);

        group.MapPost("", (VarieteCreateRequest request, IVarieteUseCases varieteUseCases) =>
        {
            varieteUseCases.AddVariete(request);
            return Results.Created();
        })
        .RequireAuthorization(policy => policy.RequireRole("GESTIONNAIRE_CATALOGUE", "ADMIN"))
        .WithName("AddVariete")
        .Produces(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status500InternalServerError);

        group.MapPut("{id:int}", (int id, VarieteUpdateRequest request, IVarieteUseCases varieteUseCases) =>
        {
            var updated = varieteUseCases.UpdateVariete(id, request);
            if (!updated)
            {
                return Results.NotFound();
            }
            return Results.NoContent();
        })
        .RequireAuthorization(policy => policy.RequireRole("GESTIONNAIRE_CATALOGUE", "ADMIN"))
        .WithName("UpdateVariete")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status500InternalServerError);

        group.MapDelete("{id:int}", (int id, IVarieteUseCases varieteUseCases) =>
        {
            var deleted = varieteUseCases.DeleteVariete(id);
            if (!deleted)
            {
                return Results.NotFound();
            }
            return Results.NoContent();
        })
        .RequireAuthorization(policy => policy.RequireRole("GESTIONNAIRE_CATALOGUE", "ADMIN"))
        .WithName("DeleteVariete")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status500InternalServerError);

        return app;
    }
}