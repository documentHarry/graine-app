using Infrastructure.Models;

namespace Infrastructure.Repositories.Abstractions;

public interface IAromateRepository
{
    IEnumerable<Aromate> GetAllAromates();
    Aromate? GetAromateById(int aromateId);
    void AddAromate(Aromate aromate);
    bool UpdateAromate(Aromate aromate);
    bool DeleteAromate(int aromateId);
}