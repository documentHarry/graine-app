using Core.IGateways;
using Core.Models;
using Core.UseCases.Abstractions;

namespace Core.UseCases;

public class EspeceUseCases : IEspeceUseCases
{
    private readonly IEspeceGateway _especeGateway;

    public EspeceUseCases(IEspeceGateway especeGateway)
    {
        if (especeGateway is null)
        {
            throw new Exception("EspeceGateway est obligatoire.");
        }
        _especeGateway = especeGateway;
    }

    public IEnumerable<Espece> GetAllEspeces()
    {
        return _especeGateway.GetAllEspeces();
    }

    public Espece? GetEspeceById(int especeId)
    {
        return _especeGateway.GetEspeceById(especeId);
    }

    public void AddEspece(EspeceCreateRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.NomCommun))
        {
            throw new Exception("Le nom commun est obligatoire.");
        }

        if (string.IsNullOrWhiteSpace(request.NomScientifique))
        {
            throw new Exception("Le nom scientifique est obligatoire.");
        }

        var espece = new Espece
        {
            NomCommun = request.NomCommun.Trim(),
            NomScientifique = request.NomScientifique.Trim()
        };
        _especeGateway.AddEspece(espece);
    }

    public bool UpdateEspece(int especeId, EspeceUpdateRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.NomCommun))
        {
            throw new Exception("Le nom commun est obligatoire.");
        }

        if (string.IsNullOrWhiteSpace(request.NomScientifique))
        {
            throw new Exception("Le nom scientifique est obligatoire.");
        }

        var espece = new Espece
        {
            IdEspece = especeId,
            NomCommun = request.NomCommun.Trim(),
            NomScientifique = request.NomScientifique.Trim()
        };
        return _especeGateway.UpdateEspece(espece);
    }

    public bool DeleteEspece(int especeId)
    {
        return _especeGateway.DeleteEspece(especeId);
    }
}