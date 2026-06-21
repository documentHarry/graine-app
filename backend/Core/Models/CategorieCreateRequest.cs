namespace Core.Models;

public class CategorieCreateRequest
{
    public string NomCategorie { get; set; } = string.Empty;
    public string? Descriptif { get; set; }
}