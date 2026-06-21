using Core.IGateways;
using Infrastructure.Repositories.Abstractions;

namespace Infrastructure.Gateways;

public class EspeceGateway : IEspeceGateway
{
    private readonly IEspeceRepository _especeRepository;

    public EspeceGateway(IEspeceRepository especeRepository)
    {
        _especeRepository = especeRepository
            ?? throw new ArgumentNullException(nameof(especeRepository));
    }

    public IEnumerable<Core.Models.Espece> GetAllEspeces()
    {
        var infraEspeces = _especeRepository.GetAllEspeces();
        var coreEspeces = new List<Core.Models.Espece>();

        foreach (var e in infraEspeces)
        {
            coreEspeces.Add(ToCoreEspece(e));
        }

        return coreEspeces;
    }

    public Core.Models.Espece? GetEspeceById(int especeId)
    {
        var infraEspece = _especeRepository.GetEspeceById(especeId);

        if (infraEspece == null)
        {
            return null;
        }

        return ToCoreEspece(infraEspece);
    }

    public void AddEspece(Core.Models.Espece espece)
    {
        var infraEspece = ToInfrastructureEspece(espece);

        _especeRepository.AddEspece(infraEspece);
    }

    public bool UpdateEspece(Core.Models.Espece espece)
    {
        var infraEspece = ToInfrastructureEspece(espece);

        return _especeRepository.UpdateEspece(infraEspece);
    }

    public bool DeleteEspece(int especeId)
    {
        return _especeRepository.DeleteEspece(especeId);
    }

    private static Core.Models.Espece ToCoreEspece(
        Infrastructure.Models.Espece espece
    )
    {
        return new Core.Models.Espece
        {
            IdEspece = espece.IdEspece,
            NomCommun = espece.NomCommun,
            NomScientifique = espece.NomScientifique,
            NombreVarietes = espece.NombreVarietes
        };
    }

    private static Infrastructure.Models.Espece ToInfrastructureEspece(
        Core.Models.Espece espece
    )
    {
        return new Infrastructure.Models.Espece
        {
            IdEspece = espece.IdEspece,
            NomCommun = espece.NomCommun,
            NomScientifique = espece.NomScientifique,
            NombreVarietes = espece.NombreVarietes
        };
    }
}