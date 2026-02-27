namespace Cerebotani.GabrieleTagliani.Soccer.Models;

public class Squadra
{
    public int Id { get; set; }
    public string Nome { get; set; } = default!;
    public int Anno { get; set; } = default!;
    public string Citta { get; set; } = default!;
    public string Stadio { get; set; } = default!;
    public string? LogoUrl { get; set; }
}
