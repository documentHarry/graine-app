namespace Core.Models;

public class CategorieUpdateRequest
{
    public int IdCategorie { get; set; }
    public string NomCategorie { get; set; } = string.Empty;
    public string? Descriptif { get; set; }
}