namespace Cerebotani.GabrieleTagliani.Soccer.Models;

public class SquadraCampionato
{
    public int Id { get; set; }
    public int CampionatoId { get; set; }
    public Campionato? Campionato { get; set; }
    public int SquadraId { get; set; }
    public Squadra? Squadra { get; set; }
    public int PartiteGiocate { get; set; } // PG
    public int Vittorie { get; set; } // V
    public int Pareggi { get; set; } // P
    public int Sconfitte { get; set; } // S
    public int GolFatti { get; set; } // GF
    public int GolSubiti { get; set; } // GS
    public int DifferenzaReti { get; set; } // DR
    public int Punti { get; set; } // PT
    public int Posizione { get; set; }

    // public int DifferenzaReti => GolFatti - GolSubiti; // DR
    // public int Punti => (Vittorie * 3) + Pareggi; // PT
}
