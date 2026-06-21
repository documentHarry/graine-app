using Core.Models;

namespace Core.UseCases.Abstractions;

public interface IProduitUseCases
{
    IEnumerable<Produit> GetAllProduits();
    Produit? GetProduitById(int produitId);
    IEnumerable<Produit> GetProduitsByCategorie(int categorieId);
    void AddProduit(ProduitCreateRequest request);
    bool UpdateProduit(int produitId, ProduitUpdateRequest request);
    bool DeleteProduit(int produitId);
}