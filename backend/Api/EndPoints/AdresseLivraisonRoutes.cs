using Core.Models;
using Core.UseCases.Abstractions;

namespace Api.EndPoints;

public static class AdresseLivraisonRoutes
{
    public static WebApplication AddAdresseLivraisonRoutes(this WebApplication app)
    {
        var group = app.MapGroup("api/adresses-livraison")
            .WithTags("AdressesLivraison")
            .RequireAuthorization(policy => policy.RequireRole("ADMIN"));

        group.MapGet("utilisateur/{utilisateurId:int}", (int utilisateurId, IAdresseLivraisonUseCases adresseUseCases) =>
        {
            var adresses = adresseUseCases.GetAdressesByUtilisateur(utilisateurId);
            return Results.Ok(adresses);
        })
        .WithName("GetAdressesByUtilisateur")
        .Produces<IEnumerable<AdresseLivraison>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status500InternalServerError);

        group.MapGet("{id:int}", (int id, IAdresseLivraisonUseCases adresseUseCases) =>
        {
            var adresse = adresseUseCases.GetAdresseById(id);
            if (adresse is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(adresse);
        })
        .WithName("GetAdresseLivraisonById")
        .Produces<AdresseLivraison>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status500InternalServerError);

        group.MapPost("", (AdresseLivraisonCreateRequest request, IAdresseLivraisonUseCases adresseUseCases) =>
        {
            adresseUseCases.AddAdresse(request);
            return Results.Created();
        })
        .WithName("AddAdresseLivraison")
        .Produces(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status500InternalServerError);

        group.MapPut("{id:int}", (int id, AdresseLivraisonUpdateRequest request, IAdresseLivraisonUseCases adresseUseCases) =>
        {
            var updated = adresseUseCases.UpdateAdresse(id, request);
            if (!updated)
            {
                return Results.NotFound();
            }
            return Results.NoContent();
        })
        .WithName("UpdateAdresseLivraison")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status500InternalServerError);

        group.MapDelete("{id:int}", (int id, IAdresseLivraisonUseCases adresseUseCases) =>
        {
            var deleted = adresseUseCases.DeleteAdresse(id);
            if (!deleted)
            {
                return Results.NotFound();
            }
            return Results.NoContent();
        })
        .WithName("DeleteAdresseLivraison")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status500InternalServerError);

        return app;
    }
}