using Core.Models;

namespace Core.UseCases.Abstractions;

public interface IAromateUseCases
{
    IEnumerable<Aromate> GetAllAromates();
    Aromate? GetAromateById(int aromateId);
    void AddAromate(AromateCreateRequest request);
    bool UpdateAromate(int aromateId, AromateUpdateRequest request);
    bool DeleteAromate(int aromateId);
}