using System.Text.Json.Serialization;

namespace Cerebotani.GabrieleTagliani.Soccer.Models;

public class Dat1
{
    [JsonPropertyName("posizione")]
    public int Posizione { get; set; }

    [JsonPropertyName("logo_url")]
    public string LogoUrl { get; set; } = string.Empty;

    [JsonPropertyName("squadra")]
    public string Squadra { get; set; } = string.Empty;

    [JsonPropertyName("partite_giocate")]
    public int PartiteGiocate { get; set; }

    [JsonPropertyName("vittorie")]
    public int Vittorie { get; set; }

    [JsonPropertyName("pareggi")]
    public int Pareggi { get; set; }

    [JsonPropertyName("sconfitte")]
    public int Sconfitte { get; set; }

    [JsonPropertyName("gol_fatti")]
    public int GolFatti { get; set; }

    [JsonPropertyName("gol_subiti")]
    public int GolSubiti { get; set; }

    [JsonPropertyName("differenza_reti")]
    public int DifferenzaReti { get; set; }

    [JsonPropertyName("punti")]
    public int Punti { get; set; }

    [JsonPropertyName("anno_fondazione")]
    public int Anno { get; set; }

    [JsonPropertyName("citta")]
    public string Citta { get; set; } = string.Empty;

    [JsonPropertyName("stadio")]
    public string Stadio { get; set; } = string.Empty;
}