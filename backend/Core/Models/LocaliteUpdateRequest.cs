namespace Core.Models;

public class LocaliteUpdateRequest
{
    public int IdLocalite { get; set; }
    public string CodePostal { get; set; } = string.Empty;
    public string NomLocalite { get; set; } = string.Empty;
}