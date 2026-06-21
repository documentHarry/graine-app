using Core.IGateways;
using Core.Models;
using Core.UseCases.Abstractions;

namespace Core.UseCases;

public class AromateUseCases : IAromateUseCases
{
    private readonly IAromateGateway _aromateGateway;

    public AromateUseCases(IAromateGateway aromateGateway)
    {
        if (aromateGateway is null)
        {
            throw new Exception("AromateGateway est obligatoire.");
        }
        _aromateGateway = aromateGateway;
    }

    public IEnumerable<Aromate> GetAllAromates()
    {
        return _aromateGateway.GetAllAromates();
    }

    public Aromate? GetAromateById(int aromateId)
    {
        return _aromateGateway.GetAromateById(aromateId);
    }

    public void AddAromate(AromateCreateRequest request)
    {
        if (request.VarieteId <= 0)
        {
            throw new Exception("La variété est obligatoire.");
        }

        var aromate = CreateAromateFromRequest(request);
        _aromateGateway.AddAromate(aromate);
    }

    public bool UpdateAromate(int aromateId, AromateUpdateRequest request)
    {
        if (request.VarieteId <= 0)
        {
            throw new Exception("La variété est obligatoire.");
        }

        var aromate = CreateAromateFromRequest(request);
        aromate.IdAromate = aromateId;
        return _aromateGateway.UpdateAromate(aromate);
    }

    public bool DeleteAromate(int aromateId)
    {
        return _aromateGateway.DeleteAromate(aromateId);
    }

    private static Aromate CreateAromateFromRequest(AromateCreateRequest request)
    {
        List<int> proprietesIds = [];

        if (request.ProprietesIds is not null)
        {
            proprietesIds = request.ProprietesIds.ToList();
        }

        return new Aromate
        {
            VarieteId = request.VarieteId,
            PartieUtilisee = string.IsNullOrWhiteSpace(request.PartieUtilisee) ? null : request.PartieUtilisee.Trim(),
            Propriete = string.IsNullOrWhiteSpace(request.Propriete) ? null : request.Propriete.Trim(),
            UsageCulinaire = string.IsNullOrWhiteSpace(request.UsageCulinaire) ? null : request.UsageCulinaire.Trim(),
            AromateProprietes = proprietesIds.Distinct()
                .Select(id => new AromatePropriete { ProprieteId = id })
                .ToList()
        };
    }

    private static Aromate CreateAromateFromRequest(AromateUpdateRequest request)
    {
        List<int> proprietesIds = [];

        if (request.ProprietesIds is not null)
        {
            proprietesIds = request.ProprietesIds.ToList();
        }

        return new Aromate
        {
            IdAromate = request.IdAromate,
            VarieteId = request.VarieteId,
            PartieUtilisee = string.IsNullOrWhiteSpace(request.PartieUtilisee) ? null : request.PartieUtilisee.Trim(),
            Propriete = string.IsNullOrWhiteSpace(request.Propriete) ? null : request.Propriete.Trim(),
            UsageCulinaire = string.IsNullOrWhiteSpace(request.UsageCulinaire) ? null : request.UsageCulinaire.Trim(),
            AromateProprietes = proprietesIds.Distinct()
                .Select(id => new AromatePropriete { AromateId = request.IdAromate, ProprieteId = id })
                .ToList()
        };
    }
}