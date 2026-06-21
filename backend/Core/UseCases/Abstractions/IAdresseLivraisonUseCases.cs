using Core.Models;

namespace Core.UseCases.Abstractions;

public interface IAdresseLivraisonUseCases
{
    IEnumerable<AdresseLivraison> GetAdressesByUtilisateur(int utilisateurId);
    AdresseLivraison? GetAdresseById(int adresseId);
    void AddAdresse(AdresseLivraisonCreateRequest request);
    bool UpdateAdresse(int adresseId, AdresseLivraisonUpdateRequest request);
    bool DeleteAdresse(int adresseId);
}