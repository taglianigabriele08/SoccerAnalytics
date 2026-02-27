using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace Cerebotani.GabrieleTagliani.Soccer.Components.Pages;

public partial class BL2_2526 : ComponentBase
{
    [Inject]
    public IWebHostEnvironment Env { get; set; } = default!;

    protected IEnumerable<Dat1> Dati { get; set; } = [];

    protected override void OnInitialized()
    {
        try
        {
            var percorsoFile = Path.Combine(Env.ContentRootPath, "Data", "Bundes2_2526.json");

            if (File.Exists(percorsoFile))
            {
                var jsonString = File.ReadAllText(percorsoFile);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                Dati = JsonSerializer.Deserialize<IEnumerable<Dat1>>(jsonString, options) ?? [];
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Errore: {ex.Message}");
        }
    }
}