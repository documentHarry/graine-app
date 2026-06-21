using Core.IGateways;
using Core.Models;
using Core.UseCases.Abstractions;

namespace Core.UseCases;

public class ProduitUseCases : IProduitUseCases
{
    private readonly IProduitGateway _produitGateway;

    public ProduitUseCases(IProduitGateway produitGateway)
    {
        if (produitGateway is null)
        {
            throw new Exception("ProduitGateway est obligatoire.");
        }
        _produitGateway = produitGateway;
    }

    public IEnumerable<Produit> GetAllProduits()
    {
        return _produitGateway.GetAllProduits();
    }

    public Produit? GetProduitById(int produitId)
    {
        return _produitGateway.GetProduitById(produitId);
    }

    public IEnumerable<Produit> GetProduitsByCategorie(int categorieId)
    {
        return _produitGateway.GetProduitsByCategorie(categorieId);
    }

    public void AddProduit(ProduitCreateRequest request)
    {
        ValiderProduit(request.Intitule, request.PrixUnitaire, request.Quantite, request.CategorieId, request.VarieteId);

        var produit = new Produit
        {
            Intitule = request.Intitule.Trim(),
            PrixUnitaire = request.PrixUnitaire,
            Quantite = request.Quantite,
            CategorieId = request.CategorieId,
            VarieteId = request.VarieteId,
            DateAjout = DateTime.Now
        };
        _produitGateway.AddProduit(produit);
    }

    public bool UpdateProduit(int produitId, ProduitUpdateRequest request)
    {
        ValiderProduit(request.Intitule, request.PrixUnitaire, request.Quantite, request.CategorieId, request.VarieteId);

        var produit = new Produit
        {
            IdProduit = produitId,
            Intitule = request.Intitule.Trim(),
            PrixUnitaire = request.PrixUnitaire,
            Quantite = request.Quantite,
            CategorieId = request.CategorieId,
            VarieteId = request.VarieteId
        };
        return _produitGateway.UpdateProduit(produit);
    }

    public bool DeleteProduit(int produitId)
    {
        return _produitGateway.DeleteProduit(produitId);
    }

    private static void ValiderProduit(string intitule, decimal prixUnitaire, int quantite, int categorieId, int varieteId)
    {
        if (string.IsNullOrWhiteSpace(intitule))
        {
            throw new ArgumentException("L'intitulé du produit est obligatoire.", nameof(intitule));
        }

        if (prixUnitaire <= 0)
        {
            throw new ArgumentException("Le prix unitaire doit être supérieur à 0.", nameof(prixUnitaire));
        }

        if (quantite < 0)
        {
            throw new ArgumentException("La quantité doit être supérieure ou égale à 0.", nameof(quantite));
        }

        if (categorieId <= 0)
        {
            throw new ArgumentException("La catégorie est obligatoire.", nameof(categorieId));
        }

        if (varieteId <= 0)
        {
            throw new ArgumentException("La variété est obligatoire.", nameof(varieteId));
        }
    }
}