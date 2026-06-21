using Core.Models;

namespace Core.UseCases.Abstractions;

public interface ILocaliteUseCases
{
    IEnumerable<Localite> GetAllLocalites();
    Localite? GetLocaliteById(int localiteId);
    void AddLocalite(LocaliteCreateRequest request);
    bool UpdateLocalite(int localiteId, LocaliteUpdateRequest request);
    bool DeleteLocalite(int localiteId);
}