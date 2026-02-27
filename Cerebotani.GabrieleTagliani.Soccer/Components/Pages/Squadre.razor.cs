using System.Text.Json;
namespace Cerebotani.GabrieleTagliani.Soccer.Components.Pages;

public partial class Squadre(DialogService ds,
                            NotificationService notificationService,
                            ApplicationDbContext db)
{
    private RadzenDataGrid<Squadra> grid = default!;
    private IEnumerable<Squadra> Items = default!;

    private string? RicercaSquadra;
    private List<string> NomiSquadre = new();

    protected override async Task OnInitializedAsync()
    {
        await RefreshDataAsync();
    }

    async Task RefreshDataAsync()
    {
        var query = db.Squadre.AsQueryable();

        if (!string.IsNullOrEmpty(RicercaSquadra))
        {
            query = query.Where(s => s.Nome.Contains(RicercaSquadra));
        }

        Items = await query.ToListAsync();

        NomiSquadre = await db.Squadre.Select(s => s.Nome).OrderBy(n => n).ToListAsync();
    }

    async Task OnSearchChange(object value)
    {
        await RefreshDataAsync();
    }

    async Task ResetFiltro()
    {
        RicercaSquadra = string.Empty;
        await RefreshDataAsync();
    }

    async Task InsertRow()
    {
        var nuova = new Squadra();
        var result = await ds.OpenAsync<DettaglioSquadraDialog>(
            "Aggiungi Squadra",
            new Dictionary<string, object?>() { { "Squadra", nuova } }
        );

        if (result is Squadra daSalvare)
        {
            db.Squadre.Add(daSalvare);
            await db.SaveChangesAsync();
            await RefreshDataAsync();
            await grid.Reload();
        }
    }

    async Task DeleteAll()
    {
        var confirm = await ds.Confirm(
            "Sei sicuro di voler eliminare tutte le squadre presenti nel Database?",
            "Attenzione: Cancellazione Totale!",
            new ConfirmOptions() { OkButtonText = "Sì", CancelButtonText = "Annulla" });
        
        if (confirm == true)
        {
            try
            {
                var squadre = await db.Squadre.ToListAsync();

                if (squadre.Any())
                {
                    db.Squadre.RemoveRange(squadre);
                    await db.SaveChangesAsync();
                    notificationService.Notify(new NotificationMessage
                    {
                        Severity = NotificationSeverity.Success,
                        Summary = "Cancellazione Completata.",
                        Detail = "Tutte le squadre presenti nel Database, sono state eliminate"
                    });

                    await RefreshDataAsync();
                    await grid.Reload();
                }
            }
            catch
            {
                notificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = "Errore durante la cancellazione",
                    Detail = "Impossibile eliminare le squadre."
                });
            }
        }
    }

    async Task OpenDetails(Squadra squadraSelezionata)
    {
        var result = await ds.OpenAsync<DettaglioSquadraDialog>(
            "Modifica Squadra",
            new Dictionary<string, object?>() { { "Squadra", squadraSelezionata } }
        );

        if (result is Squadra modificata)
        {
            db.Entry(squadraSelezionata).CurrentValues.SetValues(modificata);
            await db.SaveChangesAsync();
            await RefreshDataAsync();
            await grid.Reload();
        }
    }

    async Task OnUploadInfo(UploadChangeEventArgs args)
    {
        var file = args.Files.FirstOrDefault();
        if (file == null) return;

        using var stream = file.OpenReadStream();
        var dati = await JsonSerializer.DeserializeAsync<List<Dat1>>(stream, 
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (dati == null) return;

        foreach (var item in dati)
        {
            var squadraDb = await db.Squadre
                    .FirstOrDefaultAsync(s => s.Nome.ToLower() == item.Squadra.ToLower());
            
            if (squadraDb != null)
            {
                squadraDb.Anno = item.Anno;
                squadraDb.Citta = item.Citta;
                squadraDb.Stadio = item.Stadio;

                if (!string.IsNullOrEmpty(item.LogoUrl))
                {
                    squadraDb.LogoUrl = item.LogoUrl;
                }

                db.Entry(squadraDb).State = EntityState.Modified;
            }
            else
            {
                db.Squadre.Add(new Squadra
                {
                    Nome = item.Squadra,
                    Anno = item.Anno,
                    Citta = item.Citta,
                    Stadio = item.Stadio,
                    LogoUrl = item.LogoUrl
                });
            }
        }

        await db.SaveChangesAsync();
        await RefreshDataAsync();
        notificationService.Notify(
            NotificationSeverity.Success,
            "Informazioni Aggiornate"
        );
    }
    async Task ConfirmDelete(Squadra squadra)
    {
        var confirm = await ds.Confirm(
            $"Eliminare definitivamente la squadra {squadra.Nome}?",
            "Conferma Eliminazione");

        if (confirm == true)
        {
            db.Squadre.Remove(squadra);
            await db.SaveChangesAsync();
            await RefreshDataAsync();
            await grid.Reload();
        }
    }
}