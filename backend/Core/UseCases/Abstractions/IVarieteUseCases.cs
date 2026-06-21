using Core.Models;

namespace Core.UseCases.Abstractions;

public interface IVarieteUseCases
{
    IEnumerable<Variete> GetAllVarietes();
    Variete? GetVarieteById(int varieteId);
    void AddVariete(VarieteCreateRequest request);
    bool UpdateVariete(int varieteId, VarieteUpdateRequest request);
    bool DeleteVariete(int varieteId);
}