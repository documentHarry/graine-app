using Core.IGateways;
using Infrastructure.Repositories.Abstractions;

namespace Infrastructure.Gateways;

public class LocaliteGateway : ILocaliteGateway
{
    private readonly ILocaliteRepository _localiteRepository;

    public LocaliteGateway(ILocaliteRepository localiteRepository)
    {
        _localiteRepository = localiteRepository
            ?? throw new ArgumentNullException(nameof(localiteRepository));
    }

    public IEnumerable<Core.Models.Localite> GetAllLocalites()
    {
        var infraLocalites = _localiteRepository.GetAllLocalites();
        var coreLocalites = new List<Core.Models.Localite>();

        foreach (var localite in infraLocalites)
        {
            coreLocalites.Add(new Core.Models.Localite
            {
                IdLocalite = localite.IdLocalite,
                CodePostal = localite.CodePostal,
                NomLocalite = localite.NomLocalite
            });
        }

        return coreLocalites;
    }

    public Core.Models.Localite? GetLocaliteById(int localiteId)
    {
        var infraLocalite = _localiteRepository.GetLocaliteById(localiteId);

        if (infraLocalite == null)
        {
            return null;
        }

        return new Core.Models.Localite
        {
            IdLocalite = infraLocalite.IdLocalite,
            CodePostal = infraLocalite.CodePostal,
            NomLocalite = infraLocalite.NomLocalite
        };
    }

    public void AddLocalite(Core.Models.Localite localite)
    {
        var infraLocalite = new Infrastructure.Models.Localite
        {
            IdLocalite = localite.IdLocalite,
            CodePostal = localite.CodePostal,
            NomLocalite = localite.NomLocalite
        };

        _localiteRepository.AddLocalite(infraLocalite);
    }

    public bool UpdateLocalite(Core.Models.Localite localite)
    {
        var infraLocalite = new Infrastructure.Models.Localite
        {
            IdLocalite = localite.IdLocalite,
            CodePostal = localite.CodePostal,
            NomLocalite = localite.NomLocalite
        };

        return _localiteRepository.UpdateLocalite(infraLocalite);
    }

    public bool DeleteLocalite(int localiteId)
    {
        return _localiteRepository.DeleteLocalite(localiteId);
    }
}