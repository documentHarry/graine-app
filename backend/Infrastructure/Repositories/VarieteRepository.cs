using Dapper;
using Infrastructure.Models;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Database;

namespace Infrastructure.Repositories;

public class VarieteRepository(IDbConnectionFactory connectionFactory) : IVarieteRepository
{
    private readonly IDbConnectionFactory _connectionFactory = connectionFactory;

    public IEnumerable<Variete> GetAllVarietes()
    {
        const string sql = """
            SELECT
                v.*,

                e.IdEspece,
                e.NomCommun,
                e.NomScientifique,

                COUNT(p.IdProduit) AS NombreProduits

            FROM Variete v

            INNER JOIN Espece e
                ON e.IdEspece = v.EspeceId

            LEFT JOIN Produit p
                ON p.VarieteId = v.IdVariete

            GROUP BY
                v.IdVariete,
                v.Nom,
                v.Descriptif,
                v.Bio,
                v.CycleJours,
                v.CouleurLegume,
                v.TailleFixeLegume,
                v.TailleMinLegume,
                v.TailleMaxLegume,
                v.EspacementEntreLesPlants,
                v.EspacementEntreLesLignes,
                v.TypeEnsoleillement,
                v.TypeFeuillage,
                v.HauteurAdulteMin,
                v.HauteurAdulteMax,
                v.DureeDeGermination,
                v.TemperatureMinDeGermination,
                v.CycleDeVie,
                v.RusticitePlante,
                v.DateSemisMin,
                v.DateSemisMax,
                v.DureeAvantRecolte,
                v.TypeDeSol,
                v.ConseilPlantation,
                v.EspeceId,

                e.IdEspece,
                e.NomCommun,
                e.NomScientifique

            ORDER BY v.Nom;
            """;

        using var connection = _connectionFactory.CreateConnection();

        return connection.Query<Variete, Espece, Variete>(
            sql, (variete, espece) =>
            {
                variete.Espece = espece;
                return variete;
            },
            splitOn: "IdEspece"
        );
    }

    public Variete? GetVarieteById(int varieteId)
    {
        return GetAllVarietes().FirstOrDefault(v => v.IdVariete == varieteId);
    }

    public void AddVariete(Variete variete)
    {
        const string sql = """
            INSERT INTO Variete (
                Nom,
                Descriptif,
                Bio,
                CycleJours,
                CouleurLegume,
                TailleFixeLegume,
                TailleMinLegume,
                TailleMaxLegume,
                EspacementEntreLesPlants,
                EspacementEntreLesLignes,
                TypeEnsoleillement,
                TypeFeuillage,
                HauteurAdulteMin,
                HauteurAdulteMax,
                DureeDeGermination,
                TemperatureMinDeGermination,
                CycleDeVie,
                RusticitePlante,
                DateSemisMin,
                DateSemisMax,
                DureeAvantRecolte,
                TypeDeSol,
                ConseilPlantation,
                EspeceId
            )
            VALUES (
                @Nom,
                @Descriptif,
                @Bio,
                @CycleJours,
                @CouleurLegume,
                @TailleFixeLegume,
                @TailleMinLegume,
                @TailleMaxLegume,
                @EspacementEntreLesPlants,
                @EspacementEntreLesLignes,
                @TypeEnsoleillement,
                @TypeFeuillage,
                @HauteurAdulteMin,
                @HauteurAdulteMax,
                @DureeDeGermination,
                @TemperatureMinDeGermination,
                @CycleDeVie,
                @RusticitePlante,
                @DateSemisMin,
                @DateSemisMax,
                @DureeAvantRecolte,
                @TypeDeSol,
                @ConseilPlantation,
                @EspeceId
            );
            """;

        using var connection = _connectionFactory.CreateConnection();
        connection.Execute(sql, variete);
    }

    public bool UpdateVariete(Variete variete)
    {
        const string sql = """
            UPDATE Variete
            SET
                Nom = @Nom,
                Descriptif = @Descriptif,
                Bio = @Bio,
                CycleJours = @CycleJours,
                CouleurLegume = @CouleurLegume,
                TailleFixeLegume = @TailleFixeLegume,
                TailleMinLegume = @TailleMinLegume,
                TailleMaxLegume = @TailleMaxLegume,
                EspacementEntreLesPlants = @EspacementEntreLesPlants,
                EspacementEntreLesLignes = @EspacementEntreLesLignes,
                TypeEnsoleillement = @TypeEnsoleillement,
                TypeFeuillage = @TypeFeuillage,
                HauteurAdulteMin = @HauteurAdulteMin,
                HauteurAdulteMax = @HauteurAdulteMax,
                DureeDeGermination = @DureeDeGermination,
                TemperatureMinDeGermination = @TemperatureMinDeGermination,
                CycleDeVie = @CycleDeVie,
                RusticitePlante = @RusticitePlante,
                DateSemisMin = @DateSemisMin,
                DateSemisMax = @DateSemisMax,
                DureeAvantRecolte = @DureeAvantRecolte,
                TypeDeSol = @TypeDeSol,
                ConseilPlantation = @ConseilPlantation,
                EspeceId = @EspeceId
            WHERE IdVariete = @IdVariete;
            """;

        using var connection = _connectionFactory.CreateConnection();
        int lignesModifiees = connection.Execute(sql, variete);
        return lignesModifiees > 0;
    }

    public bool DeleteVariete(int varieteId)
    {
        const string sql = """
            DELETE FROM Variete
            WHERE IdVariete = @IdVariete;
            """;

        using var connection = _connectionFactory.CreateConnection();
        int lignesSupprimees = connection.Execute(sql, new { IdVariete = varieteId });
        return lignesSupprimees > 0;
    }

}