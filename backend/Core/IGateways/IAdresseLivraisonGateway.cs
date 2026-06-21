using Core.Models;

namespace Core.IGateways;

public interface IAdresseLivraisonGateway
{
    IEnumerable<AdresseLivraison> GetAdressesByUtilisateur(int utilisateurId);
    AdresseLivraison? GetAdresseById(int adresseId);
    void AddAdresse(AdresseLivraison adresse);
    bool UpdateAdresse(AdresseLivraison adresse);
    bool DeleteAdresse(int adresseId);
}