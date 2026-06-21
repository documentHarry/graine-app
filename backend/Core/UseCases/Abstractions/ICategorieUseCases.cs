using Core.Models;

namespace Core.UseCases.Abstractions;

public interface ICategorieUseCases
{
    IEnumerable<Categorie> GetAllCategories();
    Categorie? GetCategorieById(int categorieId);
    void AddCategorie(CategorieCreateRequest request);
    bool UpdateCategorie(int categorieId, CategorieUpdateRequest request);
    bool DeleteCategorie(int categorieId);
    bool DeleteCategorieWithReaffectation(int idCategorieASupprimer, int idCategorieDestination);
}