namespace Cerebotani.GabrieleTagliani.Soccer.Models;
using System.ComponentModel.DataAnnotations.Schema;  // [NotMapped]
public class Campionato
{
    [NotMapped]
    public IEnumerable<int> SquadreSelezionateIds { get; set; } = new List<int>();
    public int Id { get; set; }
    public string Descrizione { get; set; } = default!;
    public string? Nazione { get; set; }
    public string? Categoria { get; set; }
    public DateTime Inizio { get; set; }
    public DateTime Fine { get; set; }
    public int StagioneId { get; set; }
    public Stagione? Stagione { get; set; }
    public List<SquadraCampionato> SquadreCampionato { get; set; } = new();
}