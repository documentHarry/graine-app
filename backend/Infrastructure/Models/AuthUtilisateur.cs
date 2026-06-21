namespace Infrastructure.Models;

public class AuthUtilisateur
{
    public int IdUtilisateur { get; set; }

    public string Nom { get; set; } = string.Empty;

    public string Prenom { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string MotDePasseHash { get; set; } = string.Empty;

    public byte[] MotDePasseSalt { get; set; } = [];
}