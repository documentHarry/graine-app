using Core.Models;

namespace Core.IGateways;

public interface IProduitGateway
{
    IEnumerable<Produit> GetAllProduits();
    Produit? GetProduitById(int produitId);
    IEnumerable<Produit> GetProduitsByCategorie(int categorieId);
    void AddProduit(Produit produit);
    bool UpdateProduit(Produit produit);
    bool DeleteProduit(int produitId);
}