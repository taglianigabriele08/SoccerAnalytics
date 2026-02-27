using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace Cerebotani.GabrieleTagliani.Soccer.Components.Pages;

public partial class SC2526 : ComponentBase
{
    [Inject]
    public IWebHostEnvironment Env { get; set; } = default!;

    protected class GironeInfo
    {
        public string Nome { get; set; } = "";
        public IEnumerable<Dat1> Dati { get; set; } = [];
    }

    protected List<GironeInfo> Gironi { get; set; } = new();

    protected override void OnInitialized()
    {
        try
        {
            var percorsoFile = Path.Combine(Env.ContentRootPath, "Data", "SerieC2526.json");

            if (File.Exists(percorsoFile))
            {
                var jsonString = File.ReadAllText(percorsoFile);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var datiCompleti = JsonSerializer.Deserialize<SerieCWrapper>(jsonString, options);

                if (datiCompleti != null)
                {
                    Gironi.Add(new GironeInfo { Nome = "Girone A", Dati = datiCompleti.GironeA});
                    Gironi.Add(new GironeInfo { Nome = "Girone B", Dati = datiCompleti.GironeB});
                    Gironi.Add(new GironeInfo { Nome = "Girone C", Dati = datiCompleti.GironeC});
                }
            }
        }
        catch (Exception ex) { Console.WriteLine($"Errore: {ex.Message}"); }
    }
}

public class SerieCWrapper
{
    public List<Dat1> GironeA { get; set; } = [];
    public List<Dat1> GironeB { get; set; } = [];
    public List<Dat1> GironeC { get; set; } = [];
}