using Core.IGateways;
using Core.Models;
using Core.UseCases.Abstractions;

namespace Core.UseCases;

public class UtilisateurUseCases : IUtilisateurUseCases
{
    private readonly IUtilisateurGateway _utilisateurGateway;

    public UtilisateurUseCases(IUtilisateurGateway utilisateurGateway)
    {
        if (utilisateurGateway is null)
        {
            throw new Exception("UtilisateurGateway est obligatoire.");
        }
        _utilisateurGateway = utilisateurGateway;
    }

    public IEnumerable<Utilisateur> GetAllUtilisateurs()
    {
        return _utilisateurGateway.GetAllUtilisateurs();
    }

    public Utilisateur? GetUtilisateurById(int utilisateurId)
    {
        return _utilisateurGateway.GetUtilisateurById(utilisateurId);
    }

    public void AddUtilisateur(UtilisateurCreateRequest request)
    {
        ValiderUtilisateur(request.Nom, request.Prenom, request.Email);

        if (string.IsNullOrWhiteSpace(request.MotDePasse))
        {
            throw new Exception("Le mot de passe est obligatoire.");
        }

        var utilisateur = new Utilisateur
        {
            Nom = request.Nom.Trim(),
            Prenom = request.Prenom.Trim(),
            Email = request.Email.Trim(),
            DateInscription = DateTime.Now,
            Actif = 1
        };
        _utilisateurGateway.AddUtilisateur(utilisateur);
    }
    public bool UpdateUtilisateur(int utilisateurId, UtilisateurUpdateRequest request)
    {
        ValiderUtilisateur(request.Nom, request.Prenom, request.Email);

        var utilisateur = new Utilisateur
        {
            IdUtilisateur = utilisateurId,
            Nom = request.Nom.Trim(),
            Prenom = request.Prenom.Trim(),
            Email = request.Email.Trim()
        };
        return _utilisateurGateway.UpdateUtilisateur(utilisateur);
    }

    public bool DeleteUtilisateur(int utilisateurId)
    {
        return _utilisateurGateway.DeleteUtilisateur(utilisateurId);
    }

    private static void ValiderUtilisateur(string nom, string prenom, string email)
    {
        if (string.IsNullOrWhiteSpace(nom))
        {
            throw new ArgumentException("Le nom est obligatoire.", nameof(nom));
        }

        if (string.IsNullOrWhiteSpace(prenom))
        {
            throw new ArgumentException("Le prénom est obligatoire.", nameof(prenom));
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("L'email est obligatoire.", nameof(email));
        }
    }
}