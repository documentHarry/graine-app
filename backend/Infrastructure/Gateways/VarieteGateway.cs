using Core.IGateways;
using Infrastructure.Repositories.Abstractions;

namespace Infrastructure.Gateways;

public class VarieteGateway : IVarieteGateway
{
    private readonly IVarieteRepository _varieteRepository;

    public VarieteGateway(IVarieteRepository varieteRepository)
    {
        _varieteRepository = varieteRepository
            ?? throw new ArgumentNullException(nameof(varieteRepository));
    }

    public IEnumerable<Core.Models.Variete> GetAllVarietes()
    {
        var infraVarietes = _varieteRepository.GetAllVarietes();

        var coreVarietes = new List<Core.Models.Variete>();

        foreach (var v in infraVarietes)
        {
            coreVarietes.Add(MapToCore(v));
        }

        return coreVarietes;
    }

    public Core.Models.Variete? GetVarieteById(int varieteId)
    {
        var infraVariete = _varieteRepository.GetVarieteById(varieteId);

        if (infraVariete == null)
        {
            return null;
        }

        return MapToCore(infraVariete);
    }

    public void AddVariete(Core.Models.Variete variete)
    {
        var infraVariete = new Infrastructure.Models.Variete
        {
            IdVariete = variete.IdVariete,
            Nom = variete.Nom,
            Descriptif = variete.Descriptif,
            Bio = variete.Bio,
            CycleJours = variete.CycleJours,
            CouleurLegume = variete.CouleurLegume,
            TailleFixeLegume = variete.TailleFixeLegume,
            TailleMinLegume = variete.TailleMinLegume,
            TailleMaxLegume = variete.TailleMaxLegume,
            EspacementEntreLesPlants = variete.EspacementEntreLesPlants,
            EspacementEntreLesLignes = variete.EspacementEntreLesLignes,
            TypeEnsoleillement = variete.TypeEnsoleillement,
            TypeFeuillage = variete.TypeFeuillage,
            HauteurAdulteMin = variete.HauteurAdulteMin,
            HauteurAdulteMax = variete.HauteurAdulteMax,
            DureeDeGermination = variete.DureeDeGermination,
            TemperatureMinDeGermination = variete.TemperatureMinDeGermination,
            CycleDeVie = variete.CycleDeVie,
            RusticitePlante = variete.RusticitePlante,
            DateSemisMin = variete.DateSemisMin,
            DateSemisMax = variete.DateSemisMax,
            DureeAvantRecolte = variete.DureeAvantRecolte,
            TypeDeSol = variete.TypeDeSol,
            ConseilPlantation = variete.ConseilPlantation,
            EspeceId = variete.EspeceId
        };

        _varieteRepository.AddVariete(infraVariete);
    }

    public bool UpdateVariete(Core.Models.Variete variete)
    {
        var infraVariete = MapToInfrastructure(variete);

        return _varieteRepository.UpdateVariete(infraVariete);
    }

    public bool DeleteVariete(int varieteId)
    {
        return _varieteRepository.DeleteVariete(varieteId);
    }

    private static Infrastructure.Models.Variete MapToInfrastructure(Core.Models.Variete variete)
    {
        return new Infrastructure.Models.Variete
        {
            IdVariete = variete.IdVariete,
            Nom = variete.Nom,
            Descriptif = variete.Descriptif,
            Bio = variete.Bio,
            CycleJours = variete.CycleJours,
            CouleurLegume = variete.CouleurLegume,
            TailleFixeLegume = variete.TailleFixeLegume,
            TailleMinLegume = variete.TailleMinLegume,
            TailleMaxLegume = variete.TailleMaxLegume,
            EspacementEntreLesPlants = variete.EspacementEntreLesPlants,
            EspacementEntreLesLignes = variete.EspacementEntreLesLignes,
            TypeEnsoleillement = variete.TypeEnsoleillement,
            TypeFeuillage = variete.TypeFeuillage,
            HauteurAdulteMin = variete.HauteurAdulteMin,
            HauteurAdulteMax = variete.HauteurAdulteMax,
            DureeDeGermination = variete.DureeDeGermination,
            TemperatureMinDeGermination = variete.TemperatureMinDeGermination,
            CycleDeVie = variete.CycleDeVie,
            RusticitePlante = variete.RusticitePlante,
            DateSemisMin = variete.DateSemisMin,
            DateSemisMax = variete.DateSemisMax,
            DureeAvantRecolte = variete.DureeAvantRecolte,
            TypeDeSol = variete.TypeDeSol,
            ConseilPlantation = variete.ConseilPlantation,
            EspeceId = variete.EspeceId
        };
    }

    private static Core.Models.Variete MapToCore(Infrastructure.Models.Variete variete)
    {
        return new Core.Models.Variete
        {
            IdVariete = variete.IdVariete,
            Nom = variete.Nom,
            Descriptif = variete.Descriptif,
            Bio = variete.Bio,
            CycleJours = variete.CycleJours,
            CouleurLegume = variete.CouleurLegume,
            TailleFixeLegume = variete.TailleFixeLegume,
            TailleMinLegume = variete.TailleMinLegume,
            TailleMaxLegume = variete.TailleMaxLegume,
            EspacementEntreLesPlants = variete.EspacementEntreLesPlants,
            EspacementEntreLesLignes = variete.EspacementEntreLesLignes,
            TypeEnsoleillement = variete.TypeEnsoleillement,
            TypeFeuillage = variete.TypeFeuillage,
            HauteurAdulteMin = variete.HauteurAdulteMin,
            HauteurAdulteMax = variete.HauteurAdulteMax,
            DureeDeGermination = variete.DureeDeGermination,
            TemperatureMinDeGermination = variete.TemperatureMinDeGermination,
            CycleDeVie = variete.CycleDeVie,
            RusticitePlante = variete.RusticitePlante,
            DateSemisMin = variete.DateSemisMin,
            DateSemisMax = variete.DateSemisMax,
            DureeAvantRecolte = variete.DureeAvantRecolte,
            TypeDeSol = variete.TypeDeSol,
            ConseilPlantation = variete.ConseilPlantation,
            EspeceId = variete.EspeceId,
            NombreProduits = variete.NombreProduits,

            Espece = new Core.Models.Espece
            {
                IdEspece = variete.Espece.IdEspece,
                NomCommun = variete.Espece.NomCommun,
                NomScientifique = variete.Espece.NomScientifique,
                NombreVarietes = variete.Espece.NombreVarietes
            }
        };
    }


}