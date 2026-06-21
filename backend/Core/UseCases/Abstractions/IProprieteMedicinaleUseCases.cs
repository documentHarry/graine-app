using Core.Models;

namespace Core.UseCases.Abstractions;

public interface IProprieteMedicinaleUseCases
{
    IEnumerable<ProprieteMedicinale> GetAllProprietesMedicinales();
    ProprieteMedicinale? GetProprieteMedicinaleById(int proprieteId);
    void AddProprieteMedicinale(ProprieteMedicinaleCreateRequest request);
    bool UpdateProprieteMedicinale(int proprieteId, ProprieteMedicinaleUpdateRequest request);
    bool DeleteProprieteMedicinale(int proprieteId);
}