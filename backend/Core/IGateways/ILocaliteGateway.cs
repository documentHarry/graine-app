using Core.Models;

namespace Core.IGateways;

public interface ILocaliteGateway
{
    IEnumerable<Localite> GetAllLocalites();
    Localite? GetLocaliteById(int localiteId);
    void AddLocalite(Localite localite);
    bool UpdateLocalite(Localite localite);
    bool DeleteLocalite(int localiteId);
}