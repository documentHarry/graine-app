using Core.IGateways;
using Infrastructure.Repositories.Abstractions;

namespace Infrastructure.Gateways;

public class CategorieGateway : ICategorieGateway
{
    private readonly ICategorieRepository _categorieRepository;

    public CategorieGateway(ICategorieRepository categorieRepository)
    {
        _categorieRepository = categorieRepository
            ?? throw new ArgumentNullException(nameof(categorieRepository));
    }

    public IEnumerable<Core.Models.Categorie> GetAllCategories()
    {
        var infraCategories = _categorieRepository.GetAllCategories();
        var coreCategories = new List<Core.Models.Categorie>();

        foreach (var c in infraCategories)
        {
            coreCategories.Add(ToCoreCategorie(c));
        }

        return coreCategories;
    }

    public Core.Models.Categorie? GetCategorieById(int categorieId)
    {
        var infraCategorie = _categorieRepository.GetCategorieById(categorieId);

        if (infraCategorie == null)
        {
            return null;
        }

        return ToCoreCategorie(infraCategorie);
    }

    public void AddCategorie(Core.Models.Categorie categorie)
    {
        var infraCategorie = ToInfrastructureCategorie(categorie);

        _categorieRepository.AddCategorie(infraCategorie);
    }

    public bool UpdateCategorie(Core.Models.Categorie categorie)
    {
        var infraCategorie = ToInfrastructureCategorie(categorie);

        return _categorieRepository.UpdateCategorie(infraCategorie);
    }

    public bool DeleteCategorie(int categorieId)
    {
        return _categorieRepository.DeleteCategorie(categorieId);
    }

    public bool DeleteCategorieWithReaffectation(
        int idCategorieASupprimer,
        int idCategorieDestination
    )
    {
        return _categorieRepository.DeleteCategorieWithReaffectation(
            idCategorieASupprimer,
            idCategorieDestination
        );
    }

    private static Core.Models.Categorie ToCoreCategorie(
        Infrastructure.Models.Categorie categorie
    )
    {
        return new Core.Models.Categorie
        {
            IdCategorie = categorie.IdCategorie,
            NomCategorie = categorie.NomCategorie,
            Descriptif = categorie.Descriptif,
            NombreProduits = categorie.NombreProduits
        };
    }

    private static Infrastructure.Models.Categorie ToInfrastructureCategorie(
        Core.Models.Categorie categorie
    )
    {
        return new Infrastructure.Models.Categorie
        {
            IdCategorie = categorie.IdCategorie,
            NomCategorie = categorie.NomCategorie,
            Descriptif = categorie.Descriptif,
            NombreProduits = categorie.NombreProduits
        };
    }
}