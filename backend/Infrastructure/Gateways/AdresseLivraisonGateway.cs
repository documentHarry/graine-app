using Core.IGateways;
using Infrastructure.Repositories.Abstractions;

namespace Infrastructure.Gateways;

public class AdresseLivraisonGateway : IAdresseLivraisonGateway
{
    private readonly IAdresseLivraisonRepository _adresseRepository;

    public AdresseLivraisonGateway(IAdresseLivraisonRepository adresseRepository)
    {
        if (adresseRepository is null)
        {
            throw new Exception("AdresseLivraisonRepository est obligatoire.");
        }
        _adresseRepository = adresseRepository;
    }

    public IEnumerable<Core.Models.AdresseLivraison> GetAdressesByUtilisateur(int utilisateurId)
    {
        var infraAdresses = _adresseRepository.GetAdressesByUtilisateur(utilisateurId);
        var coreAdresses = new List<Core.Models.AdresseLivraison>();

        foreach (var adresse in infraAdresses)
        {
            coreAdresses.Add(MapToCore(adresse));
        }

        return coreAdresses;
    }

    public Core.Models.AdresseLivraison? GetAdresseById(int adresseId)
    {
        var infraAdresse = _adresseRepository.GetAdresseById(adresseId);

        if (infraAdresse == null)
        {
            return null;
        }

        return MapToCore(infraAdresse);
    }

    public void AddAdresse(Core.Models.AdresseLivraison adresse)
    {
        _adresseRepository.AddAdresse(MapToInfrastructure(adresse));
    }

    public bool UpdateAdresse(Core.Models.AdresseLivraison adresse)
    {
        return _adresseRepository.UpdateAdresse(MapToInfrastructure(adresse));
    }

    public bool DeleteAdresse(int adresseId)
    {
        return _adresseRepository.DeleteAdresse(adresseId);
    }

    private static Core.Models.AdresseLivraison MapToCore(
        Infrastructure.Models.AdresseLivraison adresse)
    {
        return new Core.Models.AdresseLivraison
        {
            IdAdresse = adresse.IdAdresse,
            Rue = adresse.Rue,
            Numero = adresse.Numero,
            ParDefaut = adresse.ParDefaut,
            UtilisateurId = adresse.UtilisateurId,
            LocaliteId = adresse.LocaliteId,
            Localite = new Core.Models.Localite
            {
                IdLocalite = adresse.Localite.IdLocalite,
                CodePostal = adresse.Localite.CodePostal,
                NomLocalite = adresse.Localite.NomLocalite
            }
        };
    }

    private static Infrastructure.Models.AdresseLivraison MapToInfrastructure(Core.Models.AdresseLivraison adresse)
    {
        return new Infrastructure.Models.AdresseLivraison
        {
            IdAdresse = adresse.IdAdresse,
            Rue = adresse.Rue,
            Numero = adresse.Numero,
            ParDefaut = adresse.ParDefaut,
            UtilisateurId = adresse.UtilisateurId,
            LocaliteId = adresse.LocaliteId
        };
    }

}