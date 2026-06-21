using Core.Models;

namespace Core.IGateways;

public interface ICategorieGateway
{
    IEnumerable<Categorie> GetAllCategories();
    Categorie? GetCategorieById(int categorieId);
    void AddCategorie(Categorie categorie);
    bool UpdateCategorie(Categorie categorie);
    bool DeleteCategorie(int categorieId);
    bool DeleteCategorieWithReaffectation(int idCategorieASupprimer, int idCategorieDestination);
}