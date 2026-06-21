using Core.IGateways;
using Core.Models;
using Core.UseCases.Abstractions;

namespace Core.UseCases;

public class AdresseLivraisonUseCases : IAdresseLivraisonUseCases
{
    private readonly IAdresseLivraisonGateway _adresseGateway;

    public AdresseLivraisonUseCases(IAdresseLivraisonGateway adresseGateway)
    {
        if (adresseGateway is null)
        {
            throw new Exception("Le gateway des adresses est obligatoire.");
        }
        _adresseGateway = adresseGateway;
    }

    public IEnumerable<AdresseLivraison> GetAdressesByUtilisateur(int utilisateurId)
    {
        return _adresseGateway.GetAdressesByUtilisateur(utilisateurId);
    }

    public AdresseLivraison? GetAdresseById(int adresseId)
    {
        return _adresseGateway.GetAdresseById(adresseId);
    }

    public void AddAdresse(AdresseLivraisonCreateRequest request)
    {
        ValiderAdresse(request.Rue, request.Numero, request.LocaliteId);
        var adresse = new AdresseLivraison
        {
            Rue = request.Rue.Trim(),
            Numero = request.Numero.Trim(),
            ParDefaut = request.ParDefaut,
            UtilisateurId = request.UtilisateurId,
            LocaliteId = request.LocaliteId
        };
        _adresseGateway.AddAdresse(adresse);
    }

    public bool UpdateAdresse(int adresseId, AdresseLivraisonUpdateRequest request)
    {
        ValiderAdresse(request.Rue, request.Numero, request.LocaliteId);
        var adresse = new AdresseLivraison
        {
            IdAdresse = adresseId,
            Rue = request.Rue.Trim(),
            Numero = request.Numero.Trim(),
            ParDefaut = request.ParDefaut,
            LocaliteId = request.LocaliteId
        };
        return _adresseGateway.UpdateAdresse(adresse);
    }

    public bool DeleteAdresse(int adresseId)
    {
        return _adresseGateway.DeleteAdresse(adresseId);
    }

    private static void ValiderAdresse(string rue, string numero, int localiteId)
    {
        if (string.IsNullOrWhiteSpace(rue))
        {
            throw new ArgumentException("La rue est obligatoire.", nameof(rue));
        }

        if (string.IsNullOrWhiteSpace(numero))
        {
            throw new ArgumentException("Le numéro est obligatoire.", nameof(numero));
        }

        if (localiteId <= 0)
        {
            throw new ArgumentException("La localité est obligatoire.", nameof(localiteId));
        }
    }
}