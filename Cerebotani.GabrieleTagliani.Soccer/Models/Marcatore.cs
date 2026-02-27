namespace Cerebotani.GabrieleTagliani.Soccer.Models;

public class Marcatore
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public int Minuto { get; set; }
    public int? Recupero { get; set; }
    public bool Rigore { get; set; }
    public string? SquadraNome { get; set; }
}
