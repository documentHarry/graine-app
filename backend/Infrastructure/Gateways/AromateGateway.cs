using Core.IGateways;
using Infrastructure.Repositories.Abstractions;

namespace Infrastructure.Gateways;

public class AromateGateway : IAromateGateway
{
    private readonly IAromateRepository _aromateRepository;

    public AromateGateway(IAromateRepository aromateRepository)
    {
        if (aromateRepository is null)
        {
            throw new Exception("AromateRepository est obligatoire.");
        }
        _aromateRepository = aromateRepository;
    }

    public IEnumerable<Core.Models.Aromate> GetAllAromates()
    {
        var infraAromates = _aromateRepository.GetAllAromates();
        var coreAromates = new List<Core.Models.Aromate>();

        foreach (var a in infraAromates)
        {
            coreAromates.Add(MapToCore(a));
        }

        return coreAromates;
    }

    public Core.Models.Aromate? GetAromateById(int aromateId)
    {
        var infraAromate = _aromateRepository.GetAromateById(aromateId);

        if (infraAromate == null)
        {
            return null;
        }

        return MapToCore(infraAromate);
    }

    public void AddAromate(Core.Models.Aromate aromate)
    {
        if (aromate is null)
        {
            throw new Exception("Aromate est obligatoire.");
        }

        List<Infrastructure.Models.AromatePropriete> aromateProprietes =
            new List<Infrastructure.Models.AromatePropriete>();

        if (aromate.AromateProprietes is not null)
        {
            aromateProprietes = aromate.AromateProprietes
                .Select(ap => new Infrastructure.Models.AromatePropriete
                {
                    AromateId = ap.AromateId,
                    ProprieteId = ap.ProprieteId
                })
                .ToList();
        }

        var infraAromate = new Infrastructure.Models.Aromate
        {
            IdAromate = aromate.IdAromate,
            VarieteId = aromate.VarieteId,
            PartieUtilisee = aromate.PartieUtilisee,
            Propriete = aromate.Propriete,
            UsageCulinaire = aromate.UsageCulinaire,
            AromateProprietes = aromateProprietes
        };

        _aromateRepository.AddAromate(infraAromate);
    }

    private static Core.Models.Aromate MapToCore(Infrastructure.Models.Aromate aromate)
    {
        return new Core.Models.Aromate
        {
            IdAromate = aromate.IdAromate,
            VarieteId = aromate.VarieteId,
            PartieUtilisee = aromate.PartieUtilisee,
            Propriete = aromate.Propriete,
            UsageCulinaire = aromate.UsageCulinaire,
            Variete = new Core.Models.Variete
            {
                IdVariete = aromate.Variete.IdVariete,
                Nom = aromate.Variete.Nom,
                EspeceId = aromate.Variete.EspeceId,
                Espece = new Core.Models.Espece
                {
                    IdEspece = aromate.Variete.Espece.IdEspece,
                    NomScientifique = aromate.Variete.Espece.NomScientifique,
                    NomCommun = aromate.Variete.Espece.NomCommun
                }
            },
            AromateProprietes = aromate.AromateProprietes.Select(ap => new Core.Models.AromatePropriete
            {
                AromateId = ap.AromateId,
                ProprieteId = ap.ProprieteId,
                ProprieteMedicinale = new Core.Models.ProprieteMedicinale
                {
                    IdPropriete = ap.ProprieteMedicinale.IdPropriete,
                    NomPropriete = ap.ProprieteMedicinale.NomPropriete
                }
            }).ToList()
        };
    }

    public bool UpdateAromate(Core.Models.Aromate aromate)
    {
        var infraAromate = new Infrastructure.Models.Aromate
        {
            IdAromate = aromate.IdAromate,
            VarieteId = aromate.VarieteId,
            PartieUtilisee = aromate.PartieUtilisee,
            Propriete = aromate.Propriete,
            UsageCulinaire = aromate.UsageCulinaire,
            AromateProprietes = aromate.AromateProprietes.Select(ap => new Infrastructure.Models.AromatePropriete
            {
                AromateId = aromate.IdAromate,
                ProprieteId = ap.ProprieteId
            }).ToList()
        };

        return _aromateRepository.UpdateAromate(infraAromate);
    }

    public bool DeleteAromate(int aromateId)
    {
        return _aromateRepository.DeleteAromate(aromateId);
}
}