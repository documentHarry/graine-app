using Core.Models;

namespace Core.UseCases.Abstractions;

public interface IEspeceUseCases
{
    IEnumerable<Espece> GetAllEspeces();
    Espece? GetEspeceById(int especeId);
    void AddEspece(EspeceCreateRequest request);
    bool UpdateEspece(int especeId, EspeceUpdateRequest request);
    bool DeleteEspece(int especeId);
}