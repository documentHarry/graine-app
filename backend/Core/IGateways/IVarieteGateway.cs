using Core.Models;

namespace Core.IGateways;

public interface IVarieteGateway
{
    IEnumerable<Variete> GetAllVarietes();
    Variete? GetVarieteById(int varieteId);
    void AddVariete(Variete variete);
    bool UpdateVariete(Variete variete);
    bool DeleteVariete(int varieteId);
}