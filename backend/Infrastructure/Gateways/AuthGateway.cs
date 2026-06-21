using System.Security.Cryptography;
using System.Text;
using Core.IGateways;
using Core.Models;
using Infrastructure.Repositories.Abstractions;

namespace Infrastructure.Gateways;

public class AuthGateway : IAuthGateway
{
    private readonly IAuthRepository _authRepository;

    public AuthGateway(IAuthRepository authRepository)
    {
        _authRepository = authRepository
            ?? throw new ArgumentNullException(nameof(authRepository));
    }

    public LoginResponse? Login(AuthenticationRequest request)
    {
        var utilisateur = _authRepository.GetUtilisateurByEmail(request.Email);

        if (utilisateur is null)
        {
            return null;
        }

        var hashCalcule = CalculerHash(request.MotDePasse, utilisateur.MotDePasseSalt);

        if (!string.Equals(hashCalcule, utilisateur.MotDePasseHash, StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }

        return new LoginResponse
        {
            IdUtilisateur = utilisateur.IdUtilisateur,
            Nom = utilisateur.Nom,
            Prenom = utilisateur.Prenom,
            Email = utilisateur.Email,
            Roles = _authRepository.GetRolesUtilisateur(utilisateur.IdUtilisateur).ToList()
        };
    }

    private static string CalculerHash(string motDePasse, byte[] salt)
    {
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
            password: Encoding.UTF8.GetBytes(motDePasse),
            salt: salt,
            iterations: 100000,
            hashAlgorithm: HashAlgorithmName.SHA512,
            outputLength: 64);

        return Convert.ToHexString(hash).ToLowerInvariant();
    }
}