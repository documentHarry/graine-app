namespace Infrastructure.Models;

public class AromatePropriete
{
    public int AromateId { get; set; }

    public int ProprieteId { get; set; }

    public ProprieteMedicinale ProprieteMedicinale { get; set; } = new();
}