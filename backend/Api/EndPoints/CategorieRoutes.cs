using Core.Models;
using Core.UseCases.Abstractions;

namespace Api.EndPoints;

public static class CategorieRoutes
{
    public static WebApplication AddCategorieRoutes(this WebApplication app)
    {
        var group = app.MapGroup("api/categories").WithTags("Categories");

        group.MapGet("", (ICategorieUseCases categorieUseCases) =>
        {
            var categories = categorieUseCases.GetAllCategories();
            return Results.Ok(categories);
        })
        .WithName("GetAllCategories")
        .Produces<IEnumerable<Categorie>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status500InternalServerError);

        group.MapGet("{id:int}", (int id, ICategorieUseCases categorieUseCases) =>
        {
            var categorie = categorieUseCases.GetCategorieById(id);
            if (categorie is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(categorie);
        })
        .WithName("GetCategorieById")
        .Produces<Categorie>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status500InternalServerError);

        group.MapPost("", (CategorieCreateRequest request, ICategorieUseCases categorieUseCases) =>
        {
            categorieUseCases.AddCategorie(request);
            return Results.Created();
        })
        .RequireAuthorization(policy => policy.RequireRole("GESTIONNAIRE_CATALOGUE", "ADMIN"))
        .WithName("AddCategorie")
        .Produces(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status500InternalServerError);

        group.MapPut("{id:int}", (int id, CategorieUpdateRequest request, ICategorieUseCases categorieUseCases) =>
        {
            var updated = categorieUseCases.UpdateCategorie(id, request);
            if (!updated)
            {
                return Results.NotFound();
            }
            return Results.NoContent();
        })
        .RequireAuthorization(policy => policy.RequireRole("GESTIONNAIRE_CATALOGUE", "ADMIN"))
        .WithName("UpdateCategorie")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status500InternalServerError);

        group.MapDelete("{id:int}", (int id, ICategorieUseCases categorieUseCases) =>
        {
            var deleted = categorieUseCases.DeleteCategorie(id);
            if (!deleted)
            {
                return Results.NotFound();
            }
            return Results.NoContent();
        })
        .RequireAuthorization(policy => policy.RequireRole("GESTIONNAIRE_CATALOGUE", "ADMIN"))
        .WithName("DeleteCategorie")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status500InternalServerError);

        group.MapDelete("{idCategorieASupprimer:int}/reaffectation/{idCategorieDestination:int}",
            (int idCategorieASupprimer, int idCategorieDestination, ICategorieUseCases categorieUseCases) =>
            {
                var deleted = categorieUseCases.DeleteCategorieWithReaffectation(idCategorieASupprimer, idCategorieDestination);
                if (!deleted)
                {
                    return Results.NotFound();
                }
                return Results.NoContent();
            })
        .RequireAuthorization(policy => policy.RequireRole("GESTIONNAIRE_CATALOGUE", "ADMIN"))
        .WithName("DeleteCategorieWithReaffectation")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status500InternalServerError);

        return app;
    }
}