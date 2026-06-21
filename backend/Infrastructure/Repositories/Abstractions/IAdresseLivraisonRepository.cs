using Infrastructure.Models;

namespace Infrastructure.Repositories.Abstractions;

public interface IAdresseLivraisonRepository
{
    IEnumerable<AdresseLivraison> GetAdressesByUtilisateur(int utilisateurId);
    AdresseLivraison? GetAdresseById(int adresseId);
    void AddAdresse(AdresseLivraison adresse);
    bool UpdateAdresse(AdresseLivraison adresse);
    bool DeleteAdresse(int adresseId);
}