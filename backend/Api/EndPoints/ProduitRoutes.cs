using Core.Models;
using Core.UseCases.Abstractions;

namespace Api.EndPoints;

public static class ProduitRoutes
{
    public static WebApplication AddProduitRoutes(this WebApplication app)
    {
        var group = app.MapGroup("api/produits").WithTags("Produits");

        group.MapGet("", (IProduitUseCases produitUseCases) =>
        {
            var produits = produitUseCases.GetAllProduits();
            return Results.Ok(produits);
        })
        .WithName("GetAllProduits")
        .Produces<IEnumerable<Produit>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status500InternalServerError);

        group.MapGet("{id:int}", (int id, IProduitUseCases produitUseCases) =>
        {
            var produit = produitUseCases.GetProduitById(id);
            if (produit is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(produit);
        })
        .WithName("GetProduitById")
        .Produces<Produit>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status500InternalServerError);

        group.MapGet("categorie/{categorieId:int}", (int categorieId, IProduitUseCases produitUseCases) =>
        {
            var produits = produitUseCases.GetProduitsByCategorie(categorieId);
            return Results.Ok(produits);
        })
        .WithName("GetProduitsByCategorie")
        .Produces<IEnumerable<Produit>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status500InternalServerError);

        group.MapPost("", (ProduitCreateRequest request, IProduitUseCases produitUseCases) =>
        {
            produitUseCases.AddProduit(request);
            return Results.Created();
        })
        .RequireAuthorization(policy => policy.RequireRole("GESTIONNAIRE_CATALOGUE", "ADMIN"))
        .WithName("AddProduit")
        .Produces(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status500InternalServerError);

        group.MapPut("{id:int}", (int id, ProduitUpdateRequest request, IProduitUseCases produitUseCases) =>
        {
            var updated = produitUseCases.UpdateProduit(id, request);
            if (!updated)
            {
                return Results.NotFound();
            }
            return Results.NoContent();
        })
        .RequireAuthorization(policy => policy.RequireRole("GESTIONNAIRE_CATALOGUE", "ADMIN"))
        .WithName("UpdateProduit")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status500InternalServerError);

        group.MapDelete("{id:int}", (int id, IProduitUseCases produitUseCases) =>
        {
            var deleted = produitUseCases.DeleteProduit(id);
            if (!deleted)
            {
                return Results.NotFound();
            }
            return Results.NoContent();
        })
        .RequireAuthorization(policy => policy.RequireRole("GESTIONNAIRE_CATALOGUE", "ADMIN"))
        .WithName("DeleteProduit")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status500InternalServerError);

        return app;
    }
}