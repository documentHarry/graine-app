using Core.IGateways;
using Infrastructure.Repositories.Abstractions;

namespace Infrastructure.Gateways;

public class UtilisateurGateway : IUtilisateurGateway
{
    private readonly IUtilisateurRepository _utilisateurRepository;

    public UtilisateurGateway(IUtilisateurRepository utilisateurRepository)
    {
        _utilisateurRepository = utilisateurRepository
            ?? throw new ArgumentNullException(nameof(utilisateurRepository));
    }

    public IEnumerable<Core.Models.Utilisateur> GetAllUtilisateurs()
    {
        var infraUtilisateurs = _utilisateurRepository.GetAllUtilisateurs();
        var coreUtilisateurs = new List<Core.Models.Utilisateur>();

        foreach (var utilisateur in infraUtilisateurs)
        {
            coreUtilisateurs.Add(MapToCore(utilisateur));
        }

        return coreUtilisateurs;
    }

    public Core.Models.Utilisateur? GetUtilisateurById(int utilisateurId)
    {
        var infraUtilisateur = _utilisateurRepository.GetUtilisateurById(utilisateurId);

        if (infraUtilisateur == null)
        {
            return null;
        }

        return MapToCore(infraUtilisateur);
    }

    public void AddUtilisateur(Core.Models.Utilisateur utilisateur)
    {
        var infraUtilisateur = new Infrastructure.Models.Utilisateur
        {
            IdUtilisateur = utilisateur.IdUtilisateur,
            Nom = utilisateur.Nom,
            Prenom = utilisateur.Prenom,
            Email = utilisateur.Email,
            DateInscription = utilisateur.DateInscription,
            Actif = utilisateur.Actif
        };

        _utilisateurRepository.AddUtilisateur(infraUtilisateur);
    }

    private static Core.Models.Utilisateur MapToCore(
        Infrastructure.Models.Utilisateur utilisateur)
    {
        return new Core.Models.Utilisateur
        {
            IdUtilisateur = utilisateur.IdUtilisateur,
            Nom = utilisateur.Nom,
            Prenom = utilisateur.Prenom,
            Email = utilisateur.Email,
            DateInscription = utilisateur.DateInscription,
            Actif = utilisateur.Actif,

            AdressesLivraison = utilisateur.AdressesLivraison.Select(adresse =>
                new Core.Models.AdresseLivraison
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
                }).ToList(),

            UtilisateurRoles = utilisateur.UtilisateurRoles.Select(utilisateurRole =>
                new Core.Models.UtilisateurRole
                {
                    UtilisateurId = utilisateurRole.UtilisateurId,
                    RoleId = utilisateurRole.RoleId,
                    Role = new Core.Models.Role
                    {
                        IdRole = utilisateurRole.Role.IdRole,
                        NomRole = utilisateurRole.Role.NomRole
                    }
                }).ToList()
        };
    }

    public bool UpdateUtilisateur(Core.Models.Utilisateur utilisateur)
    {
        var infraUtilisateur = new Infrastructure.Models.Utilisateur
        {
            IdUtilisateur = utilisateur.IdUtilisateur,
            Nom = utilisateur.Nom,
            Prenom = utilisateur.Prenom,
            Email = utilisateur.Email,
            DateInscription = utilisateur.DateInscription,
            Actif = utilisateur.Actif
        };

        return _utilisateurRepository.UpdateUtilisateur(infraUtilisateur);
    }

    public bool DeleteUtilisateur(int utilisateurId)
    {
        return _utilisateurRepository.DeleteUtilisateur(utilisateurId);
    }
}