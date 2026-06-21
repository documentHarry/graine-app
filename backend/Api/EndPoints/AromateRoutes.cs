using Core.Models;
using Core.UseCases.Abstractions;

namespace Api.EndPoints;

public static class AromateRoutes
{
    public static WebApplication AddAromateRoutes(this WebApplication app)
    {
        var group = app.MapGroup("api/aromates").WithTags("Aromates");

        group.MapGet("", (IAromateUseCases aromateUseCases) =>
        {
            var aromates = aromateUseCases.GetAllAromates();
            return Results.Ok(aromates);
        })
        .WithName("GetAllAromates")
        .Produces<IEnumerable<Aromate>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status500InternalServerError);

        group.MapGet("{id:int}", (int id, IAromateUseCases aromateUseCases) =>
        {
            var aromate = aromateUseCases.GetAromateById(id);
            if (aromate is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(aromate);
        })
        .WithName("GetAromateById")
        .Produces<Aromate>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status500InternalServerError);

        group.MapPost("", (AromateCreateRequest request, IAromateUseCases aromateUseCases) =>
        {
            aromateUseCases.AddAromate(request);
            return Results.Created();
        })
        .RequireAuthorization(policy => policy.RequireRole("GESTIONNAIRE_CATALOGUE", "ADMIN"))
        .WithName("AddAromate")
        .Produces(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status500InternalServerError);

        group.MapPut("{id:int}", (int id, AromateUpdateRequest request, IAromateUseCases aromateUseCases) =>
        {
            var updated = aromateUseCases.UpdateAromate(id, request);
            if (!updated)
            {
                return Results.NotFound();
            }
            return Results.NoContent();
        })
        .RequireAuthorization(policy => policy.RequireRole("GESTIONNAIRE_CATALOGUE", "ADMIN"))
        .WithName("UpdateAromate")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status500InternalServerError);

        group.MapDelete("{id:int}", (int id, IAromateUseCases aromateUseCases) =>
        {
            var deleted = aromateUseCases.DeleteAromate(id);
            if (!deleted)
            {
                return Results.NotFound();
            }
            return Results.NoContent();
        })
        .RequireAuthorization(policy => policy.RequireRole("GESTIONNAIRE_CATALOGUE", "ADMIN"))
        .WithName("DeleteAromate")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status500InternalServerError);

        return app;
    }
}