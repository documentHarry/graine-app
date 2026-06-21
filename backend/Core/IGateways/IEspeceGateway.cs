using Core.Models;

namespace Core.IGateways;

public interface IEspeceGateway
{
    IEnumerable<Espece> GetAllEspeces();
    Espece? GetEspeceById(int especeId);
    void AddEspece(Espece espece);
    bool UpdateEspece(Espece espece);
    bool DeleteEspece(int especeId);
}