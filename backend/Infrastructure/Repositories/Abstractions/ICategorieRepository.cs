using Infrastructure.Models;

namespace Infrastructure.Repositories.Abstractions;

public interface ICategorieRepository
{
    IEnumerable<Categorie> GetAllCategories();
    Categorie? GetCategorieById(int categorieId);
    void AddCategorie(Categorie categorie);
    bool UpdateCategorie(Categorie categorie);
    bool DeleteCategorie(int categorieId);
    bool DeleteCategorieWithReaffectation(int idCategorieASupprimer, int idCategorieDestination);
}