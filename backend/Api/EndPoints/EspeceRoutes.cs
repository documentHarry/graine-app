using Core.Models;
using Core.UseCases.Abstractions;

namespace Api.EndPoints;

public static class EspeceRoutes
{
    public static WebApplication AddEspeceRoutes(this WebApplication app)
    {
        var group = app.MapGroup("api/especes").WithTags("Especes");

        group.MapGet("", (IEspeceUseCases especeUseCases) =>
        {
            var especes = especeUseCases.GetAllEspeces();
            return Results.Ok(especes);
        })
        .WithName("GetAllEspeces")
        .Produces<IEnumerable<Espece>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status500InternalServerError);

        group.MapGet("{id:int}", (int id, IEspeceUseCases especeUseCases) =>
        {
            var espece = especeUseCases.GetEspeceById(id);
            if (espece is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(espece);
        })
        .WithName("GetEspeceById")
        .Produces<Espece>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status500InternalServerError);

        group.MapPost("", (EspeceCreateRequest request, IEspeceUseCases especeUseCases) =>
        {
            especeUseCases.AddEspece(request);
            return Results.Created();
        })
        .RequireAuthorization(policy => policy.RequireRole("GESTIONNAIRE_CATALOGUE", "ADMIN"))
        .WithName("AddEspece")
        .Produces(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status500InternalServerError);

        group.MapPut("{id:int}", (int id, EspeceUpdateRequest request, IEspeceUseCases especeUseCases) =>
        {
            var updated = especeUseCases.UpdateEspece(id, request);
            if (!updated)
            {
                return Results.NotFound();
            }
            return Results.NoContent();
        })
        .RequireAuthorization(policy => policy.RequireRole("GESTIONNAIRE_CATALOGUE", "ADMIN"))
        .WithName("UpdateEspece")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status500InternalServerError);

        group.MapDelete("{id:int}", (int id, IEspeceUseCases especeUseCases) =>
        {
            var deleted = especeUseCases.DeleteEspece(id);
            if (!deleted)
            {
                return Results.NotFound();
            }
            return Results.NoContent();
        })
        .RequireAuthorization(policy => policy.RequireRole("GESTIONNAIRE_CATALOGUE", "ADMIN"))
        .WithName("DeleteEspece")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status500InternalServerError);

        return app;
    }
}