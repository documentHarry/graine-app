using Core.IGateways;
using Core.Models;
using Core.UseCases.Abstractions;

namespace Core.UseCases;

public class LocaliteUseCases : ILocaliteUseCases
{
    private readonly ILocaliteGateway _localiteGateway;

    public LocaliteUseCases(ILocaliteGateway localiteGateway)
    {
        if (localiteGateway is null)
        {
            throw new Exception("Le gateway Localite est obligatoire.");
        }
        _localiteGateway = localiteGateway;
    }

    public IEnumerable<Localite> GetAllLocalites()
    {
        return _localiteGateway.GetAllLocalites();
    }

    public Localite? GetLocaliteById(int localiteId)
    {
        return _localiteGateway.GetLocaliteById(localiteId);
    }

    public void AddLocalite(LocaliteCreateRequest request)
    {
        ValiderLocalite(request.CodePostal, request.NomLocalite);

        var localite = new Localite
        {
            CodePostal = request.CodePostal.Trim(),
            NomLocalite = request.NomLocalite.Trim()
        };
        _localiteGateway.AddLocalite(localite);
    }

    public bool UpdateLocalite(int localiteId, LocaliteUpdateRequest request)
    {
        ValiderLocalite(request.CodePostal, request.NomLocalite);

        var localite = new Localite
        {
            IdLocalite = localiteId,
            CodePostal = request.CodePostal.Trim(),
            NomLocalite = request.NomLocalite.Trim()
        };
        return _localiteGateway.UpdateLocalite(localite);
    }

    public bool DeleteLocalite(int localiteId)
    {
        return _localiteGateway.DeleteLocalite(localiteId);
    }

    private static void ValiderLocalite(string codePostal, string nomLocalite)
    {
        if (string.IsNullOrWhiteSpace(codePostal))
        {
            throw new ArgumentException("Le code postal est obligatoire.", nameof(codePostal));
        }

        if (string.IsNullOrWhiteSpace(nomLocalite))
        {
            throw new ArgumentException("Le nom de la localité est obligatoire.", nameof(nomLocalite));
        }
    }
}