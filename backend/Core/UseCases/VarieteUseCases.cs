using Core.IGateways;
using Core.Models;
using Core.UseCases.Abstractions;

namespace Core.UseCases;

public class VarieteUseCases : IVarieteUseCases
{
    private readonly IVarieteGateway _varieteGateway;

    public VarieteUseCases(IVarieteGateway varieteGateway)
    {
        if (varieteGateway is null)
        {
            throw new Exception("VarieteGateway est obligatoire.");
        }
        _varieteGateway = varieteGateway;
    }

    public IEnumerable<Variete> GetAllVarietes()
    {
        return _varieteGateway.GetAllVarietes();
    }

    public Variete? GetVarieteById(int varieteId)
    {
        return _varieteGateway.GetVarieteById(varieteId);
    }

    public void AddVariete(VarieteCreateRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Nom))
        {
            throw new Exception("Le nom de la variété est obligatoire.");
        }

        if (request.EspeceId <= 0)
        {
            throw new Exception("L'espèce est obligatoire.");
        }
        var variete = CreateVarieteFromRequest(request);
        _varieteGateway.AddVariete(variete);
    }

    public bool UpdateVariete(int varieteId, VarieteUpdateRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Nom))
        {
            throw new Exception("Le nom de la variété est obligatoire.");
        }

        if (request.EspeceId <= 0)
        {
            throw new Exception("L'espèce est obligatoire.");
        }
        var variete = CreateVarieteFromRequest(request);
        variete.IdVariete = varieteId;
        return _varieteGateway.UpdateVariete(variete);
    }

    public bool DeleteVariete(int varieteId)
    {
        return _varieteGateway.DeleteVariete(varieteId);
    }

    private static Variete CreateVarieteFromRequest(VarieteCreateRequest request)
    {
        return new Variete
        {
            EspeceId = request.EspeceId,
            Nom = request.Nom.Trim(),
            Descriptif = string.IsNullOrWhiteSpace(request.Descriptif) ? null : request.Descriptif.Trim(),
            Bio = request.Bio,
            CycleJours = request.CycleJours,
            CouleurLegume = string.IsNullOrWhiteSpace(request.CouleurLegume) ? null : request.CouleurLegume.Trim(),
            TailleFixeLegume = request.TailleFixeLegume,
            TailleMinLegume = request.TailleMinLegume,
            TailleMaxLegume = request.TailleMaxLegume,
            EspacementEntreLesPlants = request.EspacementEntreLesPlants,
            EspacementEntreLesLignes = request.EspacementEntreLesLignes,
            TypeEnsoleillement = string.IsNullOrWhiteSpace(request.TypeEnsoleillement) ? null : request.TypeEnsoleillement.Trim(),
            TypeFeuillage = string.IsNullOrWhiteSpace(request.TypeFeuillage) ? null : request.TypeFeuillage.Trim(),
            HauteurAdulteMin = request.HauteurAdulteMin,
            HauteurAdulteMax = request.HauteurAdulteMax,
            DureeDeGermination = string.IsNullOrWhiteSpace(request.DureeDeGermination) ? null : request.DureeDeGermination.Trim(),
            TemperatureMinDeGermination = request.TemperatureMinDeGermination,
            CycleDeVie = string.IsNullOrWhiteSpace(request.CycleDeVie) ? null : request.CycleDeVie.Trim(),
            RusticitePlante = string.IsNullOrWhiteSpace(request.RusticitePlante) ? null : request.RusticitePlante.Trim(),
            DateSemisMin = string.IsNullOrWhiteSpace(request.DateSemisMin) ? null : request.DateSemisMin.Trim(),
            DateSemisMax = string.IsNullOrWhiteSpace(request.DateSemisMax) ? null : request.DateSemisMax.Trim(),
            DureeAvantRecolte = string.IsNullOrWhiteSpace(request.DureeAvantRecolte) ? null : request.DureeAvantRecolte.Trim(),
            TypeDeSol = string.IsNullOrWhiteSpace(request.TypeDeSol) ? null : request.TypeDeSol.Trim(),
            ConseilPlantation = string.IsNullOrWhiteSpace(request.ConseilPlantation) ? null : request.ConseilPlantation.Trim()
        };
    }

    private static Variete CreateVarieteFromRequest(VarieteUpdateRequest request)
    {
        return new Variete
        {
            IdVariete = request.IdVariete,
            EspeceId = request.EspeceId,
            Nom = request.Nom.Trim(),
            Descriptif = string.IsNullOrWhiteSpace(request.Descriptif) ? null : request.Descriptif.Trim(),
            Bio = request.Bio,
            CycleJours = request.CycleJours,
            CouleurLegume = string.IsNullOrWhiteSpace(request.CouleurLegume) ? null : request.CouleurLegume.Trim(),
            TailleFixeLegume = request.TailleFixeLegume,
            TailleMinLegume = request.TailleMinLegume,
            TailleMaxLegume = request.TailleMaxLegume,
            EspacementEntreLesPlants = request.EspacementEntreLesPlants,
            EspacementEntreLesLignes = request.EspacementEntreLesLignes,
            TypeEnsoleillement = string.IsNullOrWhiteSpace(request.TypeEnsoleillement) ? null : request.TypeEnsoleillement.Trim(),
            TypeFeuillage = string.IsNullOrWhiteSpace(request.TypeFeuillage) ? null : request.TypeFeuillage.Trim(),
            HauteurAdulteMin = request.HauteurAdulteMin,
            HauteurAdulteMax = request.HauteurAdulteMax,
            DureeDeGermination = string.IsNullOrWhiteSpace(request.DureeDeGermination) ? null : request.DureeDeGermination.Trim(),
            TemperatureMinDeGermination = request.TemperatureMinDeGermination,
            CycleDeVie = string.IsNullOrWhiteSpace(request.CycleDeVie) ? null : request.CycleDeVie.Trim(),
            RusticitePlante = string.IsNullOrWhiteSpace(request.RusticitePlante) ? null : request.RusticitePlante.Trim(),
            DateSemisMin = string.IsNullOrWhiteSpace(request.DateSemisMin) ? null : request.DateSemisMin.Trim(),
            DateSemisMax = string.IsNullOrWhiteSpace(request.DateSemisMax) ? null : request.DateSemisMax.Trim(),
            DureeAvantRecolte = string.IsNullOrWhiteSpace(request.DureeAvantRecolte) ? null : request.DureeAvantRecolte.Trim(),
            TypeDeSol = string.IsNullOrWhiteSpace(request.TypeDeSol) ? null : request.TypeDeSol.Trim(),
            ConseilPlantation = string.IsNullOrWhiteSpace(request.ConseilPlantation) ? null : request.ConseilPlantation.Trim()
        };
    }
}