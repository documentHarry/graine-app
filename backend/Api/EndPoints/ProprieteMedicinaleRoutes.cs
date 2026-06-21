using Core.Models;
using Core.UseCases.Abstractions;

namespace Api.EndPoints;

public static class ProprieteMedicinaleRoutes
{
    public static WebApplication AddProprieteMedicinaleRoutes(this WebApplication app)
    {
        var group = app.MapGroup("api/proprietes-medicinales")
            .WithTags("ProprietesMedicinales")
            .RequireAuthorization(policy => policy.RequireRole("GESTIONNAIRE_CATALOGUE", "ADMIN"));

        group.MapGet("", (IProprieteMedicinaleUseCases proprieteMedicinaleUseCases) =>
        {
            var proprietes = proprieteMedicinaleUseCases.GetAllProprietesMedicinales();
            return Results.Ok(proprietes);
        })
        .WithName("GetAllProprietesMedicinales")
        .Produces<IEnumerable<ProprieteMedicinale>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status500InternalServerError);

        group.MapGet("{id:int}", (int id, IProprieteMedicinaleUseCases proprieteMedicinaleUseCases) =>
        {
            var propriete = proprieteMedicinaleUseCases.GetProprieteMedicinaleById(id);
            if (propriete is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(propriete);
        })
        .WithName("GetProprieteMedicinaleById")
        .Produces<ProprieteMedicinale>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status500InternalServerError);

        group.MapPost("", (ProprieteMedicinaleCreateRequest request, IProprieteMedicinaleUseCases proprieteMedicinaleUseCases) =>
        {
            proprieteMedicinaleUseCases.AddProprieteMedicinale(request);
            return Results.Created();
        })
        .WithName("AddProprieteMedicinale")
        .Produces(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status500InternalServerError);

        group.MapPut("{id:int}", (int id, ProprieteMedicinaleUpdateRequest request, IProprieteMedicinaleUseCases proprieteMedicinaleUseCases) =>
        {
            var updated = proprieteMedicinaleUseCases.UpdateProprieteMedicinale(id, request);
            if (!updated)
            {
                return Results.NotFound();
            }
            return Results.NoContent();
        })
        .WithName("UpdateProprieteMedicinale")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status500InternalServerError);

        group.MapDelete("{id:int}", (int id, IProprieteMedicinaleUseCases proprieteMedicinaleUseCases) =>
        {
            var deleted = proprieteMedicinaleUseCases.DeleteProprieteMedicinale(id);
            if (!deleted)
            {
                return Results.NotFound();
            }
            return Results.NoContent();
        })
        .WithName("DeleteProprieteMedicinale")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status500InternalServerError);

        return app;
    }
}