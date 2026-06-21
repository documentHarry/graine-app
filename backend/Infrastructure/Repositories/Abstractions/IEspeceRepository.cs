using Infrastructure.Models;

namespace Infrastructure.Repositories.Abstractions;

public interface IEspeceRepository
{
    IEnumerable<Espece> GetAllEspeces();
    Espece? GetEspeceById(int especeId);
    void AddEspece(Espece espece);
    bool UpdateEspece(Espece espece);
    bool DeleteEspece(int especeId);
}