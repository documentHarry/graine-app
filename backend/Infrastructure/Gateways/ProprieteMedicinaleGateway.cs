using Core.IGateways;
using Infrastructure.Repositories.Abstractions;

namespace Infrastructure.Gateways;

public class ProprieteMedicinaleGateway
    : IProprieteMedicinaleGateway
{
    private readonly IProprieteMedicinaleRepository _proprieteMedicinaleRepository;

    public ProprieteMedicinaleGateway(
        IProprieteMedicinaleRepository proprieteMedicinaleRepository)
    {
        _proprieteMedicinaleRepository = proprieteMedicinaleRepository
            ?? throw new ArgumentNullException(
                nameof(proprieteMedicinaleRepository));
    }

    public IEnumerable<Core.Models.ProprieteMedicinale>
        GetAllProprietesMedicinales()
    {
        var infraProprietes =
            _proprieteMedicinaleRepository.GetAllProprietesMedicinales();

        var coreProprietes =
            new List<Core.Models.ProprieteMedicinale>();

        foreach (var p in infraProprietes)
        {
            coreProprietes.Add(ToCoreProprieteMedicinale(p));
        }

        return coreProprietes;
    }

    public Core.Models.ProprieteMedicinale?
        GetProprieteMedicinaleById(int proprieteId)
    {
        var infraPropriete =
            _proprieteMedicinaleRepository.GetProprieteMedicinaleById(proprieteId);

        if (infraPropriete == null)
        {
            return null;
        }

        return ToCoreProprieteMedicinale(infraPropriete);
    }

    public void AddProprieteMedicinale(
        Core.Models.ProprieteMedicinale proprieteMedicinale)
    {
        var infraPropriete =
            ToInfrastructureProprieteMedicinale(proprieteMedicinale);

        _proprieteMedicinaleRepository
            .AddProprieteMedicinale(infraPropriete);
    }

    public bool UpdateProprieteMedicinale(
        Core.Models.ProprieteMedicinale proprieteMedicinale)
    {
        var infraPropriete =
            ToInfrastructureProprieteMedicinale(proprieteMedicinale);

        return _proprieteMedicinaleRepository
            .UpdateProprieteMedicinale(infraPropriete);
    }

    public bool DeleteProprieteMedicinale(int proprieteId)
    {
        return _proprieteMedicinaleRepository
            .DeleteProprieteMedicinale(proprieteId);
    }

    private static Core.Models.ProprieteMedicinale ToCoreProprieteMedicinale(
        Infrastructure.Models.ProprieteMedicinale proprieteMedicinale)
    {
        return new Core.Models.ProprieteMedicinale
        {
            IdPropriete = proprieteMedicinale.IdPropriete,
            NomPropriete = proprieteMedicinale.NomPropriete
        };
    }

    private static Infrastructure.Models.ProprieteMedicinale ToInfrastructureProprieteMedicinale(
        Core.Models.ProprieteMedicinale proprieteMedicinale)
    {
        return new Infrastructure.Models.ProprieteMedicinale
        {
            IdPropriete = proprieteMedicinale.IdPropriete,
            NomPropriete = proprieteMedicinale.NomPropriete
        };
    }
}