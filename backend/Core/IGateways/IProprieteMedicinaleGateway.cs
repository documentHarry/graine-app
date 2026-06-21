using Core.Models;

namespace Core.IGateways;

public interface IProprieteMedicinaleGateway
{
    IEnumerable<ProprieteMedicinale> GetAllProprietesMedicinales();
    ProprieteMedicinale? GetProprieteMedicinaleById(int proprieteId);
    void AddProprieteMedicinale(ProprieteMedicinale proprieteMedicinale);
    bool UpdateProprieteMedicinale(ProprieteMedicinale proprieteMedicinale);
    bool DeleteProprieteMedicinale(int proprieteId);
}