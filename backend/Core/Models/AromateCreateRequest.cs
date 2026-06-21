namespace Core.Models;

public class AromateCreateRequest
{
    public int VarieteId { get; set; }
    public string? PartieUtilisee { get; set; }
    public string? Propriete { get; set; }
    public string? UsageCulinaire { get; set; }
    public List<int> ProprietesIds { get; set; } = [];
}