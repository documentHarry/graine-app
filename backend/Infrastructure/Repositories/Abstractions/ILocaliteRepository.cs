using Infrastructure.Models;

namespace Infrastructure.Repositories.Abstractions;

public interface ILocaliteRepository
{
    IEnumerable<Localite> GetAllLocalites();
    Localite? GetLocaliteById(int localiteId);
    void AddLocalite(Localite localite);
    bool UpdateLocalite(Localite localite);
    bool DeleteLocalite(int localiteId);
}