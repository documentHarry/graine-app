namespace Infrastructure.Models;

public class UtilisateurRole
{
    public int UtilisateurId { get; set; }

    public int RoleId { get; set; }

    public Role Role { get; set; } = new();
}