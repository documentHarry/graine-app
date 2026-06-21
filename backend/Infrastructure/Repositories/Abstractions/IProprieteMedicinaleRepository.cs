using Infrastructure.Models;

namespace Infrastructure.Repositories.Abstractions;

public interface IProprieteMedicinaleRepository
{
    IEnumerable<ProprieteMedicinale> GetAllProprietesMedicinales();
    ProprieteMedicinale? GetProprieteMedicinaleById(int proprieteId);
    void AddProprieteMedicinale(ProprieteMedicinale proprieteMedicinale);
    bool UpdateProprieteMedicinale(ProprieteMedicinale proprieteMedicinale);
    bool DeleteProprieteMedicinale(int proprieteId);
}