using Core.IGateways;
using Core.Models;
using Core.UseCases.Abstractions;

namespace Core.UseCases;

public class CategorieUseCases : ICategorieUseCases
{
    private readonly ICategorieGateway _categorieGateway;

    public CategorieUseCases(ICategorieGateway categorieGateway)
    {
        if (categorieGateway is null)
        {
            throw new Exception("CategorieGateway est obligatoire.");
        }
        _categorieGateway = categorieGateway;
    }

    public IEnumerable<Categorie> GetAllCategories()
    {
        return _categorieGateway.GetAllCategories();
    }

    public Categorie? GetCategorieById(int categorieId)
    {
        return _categorieGateway.GetCategorieById(categorieId);
    }

    public void AddCategorie(CategorieCreateRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.NomCategorie))
        {
            throw new Exception("Le nom de la catégorie est obligatoire.");
        }

        string? descriptif = null;
        if (!string.IsNullOrWhiteSpace(request.Descriptif))
        {
            descriptif = request.Descriptif.Trim();
        }

        var categorie = new Categorie
        {
            NomCategorie = request.NomCategorie.Trim(),
            Descriptif = descriptif
        };
        _categorieGateway.AddCategorie(categorie);
    }

    public bool UpdateCategorie(int categorieId, CategorieUpdateRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.NomCategorie))
        {
            throw new Exception("Le nom de la catégorie est obligatoire.");
        }

        string? descriptif = null;
        if (!string.IsNullOrWhiteSpace(request.Descriptif))
        {
            descriptif = request.Descriptif.Trim();
        }

        var categorie = new Categorie
        {
            IdCategorie = categorieId,
            NomCategorie = request.NomCategorie.Trim(),
            Descriptif = descriptif
        };
        return _categorieGateway.UpdateCategorie(categorie);
    }

    public bool DeleteCategorie(int categorieId)
    {
        return _categorieGateway.DeleteCategorie(categorieId);
    }

    public bool DeleteCategorieWithReaffectation(int idCategorieASupprimer, int idCategorieDestination)
    {
        if (idCategorieASupprimer == idCategorieDestination)
        {
            throw new ArgumentException("La catégorie de destination doit être différente de la catégorie à supprimer.");
        }
        return _categorieGateway.DeleteCategorieWithReaffectation(idCategorieASupprimer, idCategorieDestination);
    }
}