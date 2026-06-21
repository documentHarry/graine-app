namespace Core.Models;

public class Espece
{
    public int IdEspece { get; set; }
    public string NomCommun { get; set; } = string.Empty;
    public string NomScientifique { get; set; } = string.Empty;
    public int NombreVarietes { get; set; }
}