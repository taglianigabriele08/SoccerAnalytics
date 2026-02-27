using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace Cerebotani.GabrieleTagliani.Soccer.Components.Pages;

public partial class Home
{
    [Inject] 
    public NavigationManager Nav { get; set; } = default!;
    
    [Inject] 
    public IWebHostEnvironment Env { get; set; } = default!;

    protected string? StagioneSelezionata { get; set; }
    protected string? CampionatoSelezionato { get; set; }
    protected string? SquadraSelezionata { get; set; }

    protected List<string> Stagioni { get; set; } = [];
    protected List<string> Campionati = [ 
        "Serie A", "Serie B", "Serie C",
        "Premier League", "EFL Championship", "EFL League One", "EFL League Two", 
        "LaLiga", "LaLiga 2", 
        "BundesLiga", "BundesLiga 2", "3.Liga", 
        "Ligue 1", "Ligue 2", "Ligue 3", 
        "Liga Portugal", "Liga Portugal 2", 
        "Eredivisie", "Eerst Divisie", 
        "Super Lig", 
        "Jupiler Pro League", "Challenger Pro League", 
        "Chance Liga", "Fotbalova Narodni Liga", 
        "Souper Ligka Ellada", "Souper Ligka Ellada 2",
        "Ekstraklasa"
    ];
    
    protected IEnumerable<Dat1> DatiSquadre { get; set; } = [];

    protected override void OnInitialized()
    {
        PopolaStagioni(2008, 18); 

        StagioneSelezionata = Stagioni.FirstOrDefault();

        try 
        {
            var percorsoFile = Path.Combine(Env.ContentRootPath, "Data", "SerieA2526.json");
            
            if (File.Exists(percorsoFile))
            {
                var jsonString = File.ReadAllText(percorsoFile);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                DatiSquadre = JsonSerializer.Deserialize<IEnumerable<Dat1>>(jsonString, options) ?? [];
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Errore nel caricamento iniziale: {ex.Message}");
        }
    }

    private void PopolaStagioni(int annoPartenza, int numeroAnni)
    {
        for (int i = 0; i < numeroAnni; i++)
        {
            int annoInizio = annoPartenza + i;
            int annoFine = (annoInizio + 1) % 100; 
            Stagioni.Add($"{annoInizio}/{annoFine:D2}");
        }
        Stagioni.Reverse();
    }

    private readonly Dictionary<string, string> RottaCampionati = new(StringComparer.OrdinalIgnoreCase)
    {
        { "Serie A", "serie-a" },
        { "Serie B", "serie-b" },
        { "Serie C", "serie-c" },
        { "Premier League", "premier-league" },
        { "EFL Championship", "championship" },
        { "EFL League One", "league-one" },
        { "EFL League Two", "league-two" },
        { "LaLiga", "laliga" },
        { "LaLiga 2", "laliga2" },
        { "Bundesliga", "bundesliga" },
        { "Bundesliga 2", "bundesliga2" },
        { "3.Liga", "3.liga" },
        { "Ligue 1", "ligue1" },
        { "Ligue 2", "ligue2" },
        { "Ligue 3", "ligue3" },
        { "Liga Portugal", "liga-portugal" },
        { "Liga Portugal 2", "liga-portugal2" },
        { "Eredivisie", "eredivisie" },
        { "Eerst Divisie", "eerst-divisie" },
        { "Super Lig", "super-lig" },
        { "Jupiler Pro League", "jupiler-pro-league" },
        { "Challenger Pro League", "challenger-pro-league" },
        { "Chance Liga", "chance-liga" },
        { "Fotbalova Narodni Liga", "fotbalova-narodni-liga" },
        { "Souper Ligka Ellada", "souper-ligka-ellada" },
        { "Souper Ligka Ellada 2", "souper-ligka-ellada2" },
        { "Ekstraklasa", "ekstraklasa" }
    };

    protected void Naviga()
    {
        if (!string.IsNullOrEmpty(CampionatoSelezionato) && 
            RottaCampionati.TryGetValue(CampionatoSelezionato, out var rotta))
        {
            if (!string.IsNullOrEmpty(StagioneSelezionata))
            {
                var parti = StagioneSelezionata.Split('/');
                string annoBreveInizio = parti[0].Substring(parti[0].Length - 2);
                string stagId = $"{annoBreveInizio}{parti[1]}";
                
                Nav.NavigateTo($"/{rotta}-{stagId}");
            }
        }
    }
}