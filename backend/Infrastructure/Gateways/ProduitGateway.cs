using Core.IGateways;
using Infrastructure.Repositories.Abstractions;

namespace Infrastructure.Gateways;

public class ProduitGateway : IProduitGateway
{
    private readonly IProduitRepository _produitRepository;

    public ProduitGateway(IProduitRepository produitRepository)
    {
        _produitRepository = produitRepository
            ?? throw new ArgumentNullException(nameof(produitRepository));
    }

    public IEnumerable<Core.Models.Produit> GetAllProduits()
    {
        var infraProduits = _produitRepository.GetAllProduits();

        var coreProduits = new List<Core.Models.Produit>();

        foreach (var produit in infraProduits)
        {
            coreProduits.Add(MapToCore(produit));
        }

        return coreProduits;
    }

    public Core.Models.Produit? GetProduitById(int produitId)
    {
        var infraProduit = _produitRepository.GetProduitById(produitId);

        if (infraProduit == null)
        {
            return null;
        }

        return MapToCore(infraProduit);
    }

    public IEnumerable<Core.Models.Produit> GetProduitsByCategorie(int categorieId)
    {
        var infraProduits =
            _produitRepository.GetProduitsByCategorie(categorieId);

        var coreProduits = new List<Core.Models.Produit>();

        foreach (var produit in infraProduits)
        {
            coreProduits.Add(MapToCore(produit));
        }

        return coreProduits;
    }

    public void AddProduit(Core.Models.Produit produit)
    {
        var infraProduit = new Infrastructure.Models.Produit
        {
            IdProduit = produit.IdProduit,
            Intitule = produit.Intitule,
            PrixUnitaire = produit.PrixUnitaire,
            Quantite = produit.Quantite,
            ImageProduit = produit.ImageProduit,
            DateAjout = produit.DateAjout,
            CategorieId = produit.CategorieId,
            VarieteId = produit.VarieteId
        };

        _produitRepository.AddProduit(infraProduit);
    }

    private static Core.Models.Produit MapToCore(
        Infrastructure.Models.Produit produit)
    {
        return new Core.Models.Produit
        {
            IdProduit = produit.IdProduit,
            Intitule = produit.Intitule,
            PrixUnitaire = produit.PrixUnitaire,
            Quantite = produit.Quantite,
            ImageProduit = produit.ImageProduit,
            DateAjout = produit.DateAjout,
            CategorieId = produit.CategorieId,
            VarieteId = produit.VarieteId,

            Categorie = new Core.Models.Categorie
            {
                IdCategorie = produit.Categorie.IdCategorie,
                NomCategorie = produit.Categorie.NomCategorie,
                Descriptif = produit.Categorie.Descriptif
            },

            Variete = new Core.Models.Variete
            {
                IdVariete = produit.Variete.IdVariete,
                Nom = produit.Variete.Nom,
                EspeceId = produit.Variete.EspeceId,

                Espece = new Core.Models.Espece
                {
                    IdEspece = produit.Variete.Espece.IdEspece,
                    NomCommun = produit.Variete.Espece.NomCommun,
                    NomScientifique = produit.Variete.Espece.NomScientifique
                }
            }
        };
    }

    public bool UpdateProduit(Core.Models.Produit produit)
    {
        var infraProduit = new Infrastructure.Models.Produit
        {
            IdProduit = produit.IdProduit,
            Intitule = produit.Intitule,
            PrixUnitaire = produit.PrixUnitaire,
            Quantite = produit.Quantite,
            ImageProduit = produit.ImageProduit,
            DateAjout = produit.DateAjout,
            CategorieId = produit.CategorieId,
            VarieteId = produit.VarieteId
        };

        return _produitRepository.UpdateProduit(infraProduit);
    }

    public bool DeleteProduit(int produitId)
    {
        return _produitRepository.DeleteProduit(produitId);
    }
}