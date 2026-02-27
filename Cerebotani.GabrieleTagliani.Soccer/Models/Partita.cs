public class Partita
{
    public int Id { get; set; }
    public int CampionatoId { get; set; }
    public int Giornata { get; set; }
    public DateTime Data { get; set; }
    public string SquadraCasa { get; set; } = string.Empty;
    public string SquadraTrasferta { get; set; } = string.Empty;
    public int? GolCasa { get; set; }
    public int? GolTrasferta { get; set; }

    // Relazione opzionale
    public Campionato? Campionato { get; set; }

    public List<Marcatore> Marcatori { get; set; } = new();

    public DateTime Fine => Data.AddHours(2); 
    public string CalendarioMatches => $"{SquadraCasa} {GolCasa}-{GolTrasferta} {SquadraTrasferta}";
}