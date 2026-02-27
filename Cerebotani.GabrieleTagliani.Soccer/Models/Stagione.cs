namespace Cerebotani.GabrieleTagliani.Soccer.Models;

public class Stagione
{
    public int Id { get; set; }
    public string Descrizione { get; set; } = default!;
    public List<Campionato> Campionati { get; set; } = new();
}
