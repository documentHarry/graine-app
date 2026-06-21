using Core.IGateways;
using Core.Models;
using Core.UseCases.Abstractions;

namespace Core.UseCases;

public class ProprieteMedicinaleUseCases : IProprieteMedicinaleUseCases
{
    private readonly IProprieteMedicinaleGateway _proprieteMedicinaleGateway;

    public ProprieteMedicinaleUseCases(IProprieteMedicinaleGateway proprieteMedicinaleGateway)
    {
        if (proprieteMedicinaleGateway is null)
        {
            throw new Exception("ProprieteMedicinaleGateway est obligatoire.");
        }
        _proprieteMedicinaleGateway = proprieteMedicinaleGateway;
    }

    public IEnumerable<ProprieteMedicinale> GetAllProprietesMedicinales()
    {
        return _proprieteMedicinaleGateway.GetAllProprietesMedicinales();
    }

    public ProprieteMedicinale? GetProprieteMedicinaleById(int proprieteId)
    {
        return _proprieteMedicinaleGateway.GetProprieteMedicinaleById(proprieteId);
    }

    public void AddProprieteMedicinale(ProprieteMedicinaleCreateRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.NomPropriete))
        {
            throw new Exception("Le nom de la propriété médicinale est obligatoire.");
        }

        var proprieteMedicinale = new ProprieteMedicinale
        {
            NomPropriete = request.NomPropriete.Trim()
        };
        _proprieteMedicinaleGateway.AddProprieteMedicinale(proprieteMedicinale);
    }

    public bool UpdateProprieteMedicinale(int proprieteId, ProprieteMedicinaleUpdateRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.NomPropriete))
        {
            throw new Exception("Le nom de la propriété médicinale est obligatoire.");
        }

        var proprieteMedicinale = new ProprieteMedicinale
        {
            IdPropriete = proprieteId,
            NomPropriete = request.NomPropriete.Trim()
        };
        return _proprieteMedicinaleGateway.UpdateProprieteMedicinale(proprieteMedicinale);
    }

    public bool DeleteProprieteMedicinale(int proprieteId)
    {
        return _proprieteMedicinaleGateway.DeleteProprieteMedicinale(proprieteId);
    }
}