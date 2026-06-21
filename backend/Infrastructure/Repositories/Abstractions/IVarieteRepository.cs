using Infrastructure.Models;

namespace Infrastructure.Repositories.Abstractions;

public interface IVarieteRepository
{
    IEnumerable<Variete> GetAllVarietes();
    Variete? GetVarieteById(int varieteId);
    void AddVariete(Variete variete);
    bool UpdateVariete(Variete variete);
    bool DeleteVariete(int varieteId);
}