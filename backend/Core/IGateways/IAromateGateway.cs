using Core.Models;

namespace Core.IGateways;

public interface IAromateGateway
{
    IEnumerable<Aromate> GetAllAromates();
    Aromate? GetAromateById(int aromateId);
    void AddAromate(Aromate aromate);
    bool UpdateAromate(Aromate aromate);
    bool DeleteAromate(int aromateId);
}