namespace Cerebotani.GabrieleTagliani.Soccer.Components.Pages;

public partial class Stagioni(DialogService dialogService,
                              ApplicationDbContext db)
{
    private RadzenDataGrid<Stagione> Grid { get; set; } = default!;
    private IEnumerable<Stagione> Items { get; set; } = default!;
    private RadzenUpload? UploadComponent { get; set; }

    protected override async Task OnInitializedAsync() => await RefreshDataAsync();

    async Task RefreshDataAsync() => Items = await db.Stagioni.ToListAsync();

    async Task InsertRow()
    {
        var nuovaStagione = new Stagione { Descrizione = "Nuova Stagione" };
        var result = await dialogService.OpenAsync<DettaglioStagioneDialog>(
            "Aggiungi Stagione",
            new Dictionary<string, object?>() { { "Stagione", nuovaStagione } }
        );

        if (result is Stagione daSalvare)
        {
            db.Stagioni.Add(daSalvare);
            await db.SaveChangesAsync();
            await RefreshDataAsync();
            await Grid.Reload();
        }
    }

    async Task OpenDetails(Stagione stagioneSelezionata)
    {
        var result = await dialogService.OpenAsync<DettaglioStagioneDialog>(
            "Modifica Stagione",
            new Dictionary<string, object?>() { { "Stagione", stagioneSelezionata } }
        );

        if (result is Stagione modificata)
        {
            db.Entry(stagioneSelezionata).CurrentValues.SetValues(modificata);
            await db.SaveChangesAsync();
            await RefreshDataAsync();
        }
    }

    async Task ConfirmDelete(Stagione stagione)
    {
        var confirm = await dialogService.Confirm(
            $"Eliminando la stagione {stagione.Descrizione} potresti eliminare i campionati collegati. Continuare?",
            "Conferma Eliminazione");

        if (confirm == true)
        {
            db.Stagioni.Remove(stagione);
            await db.SaveChangesAsync();
            await RefreshDataAsync();
            await Grid.Reload();
        }
    }
}
