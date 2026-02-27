using Microsoft.AspNetCore.Components;
using System.Reflection.Metadata;

namespace Cerebotani.GabrieleTagliani.Soccer.Components.Pages;

public partial class DettaglioCampionatoDialog
{
    [Inject] private DialogService dialogService { get; set; } = default!;
    [Inject] private ApplicationDbContext db { get; set; } = default!;

    private List<Squadra> squadre = new();
    private IEnumerable<int> idsSquadreSelezionate = new List<int>();

    [Parameter] public Campionato Campionato { get; set; } = new();
    private Campionato interfaccia = new();
    private List<Stagione> listaStagioni = new();

    protected override async Task OnInitializedAsync()
    {
        listaStagioni = await db.Stagioni.ToListAsync();
        squadre = await db.Squadre.ToListAsync();

        interfaccia.Id = Campionato.Id;
        interfaccia.Descrizione = Campionato.Descrizione;
        interfaccia.Nazione = Campionato.Nazione;
        interfaccia.Categoria = Campionato.Categoria;
        interfaccia.Inizio = Campionato.Inizio;
        interfaccia.Fine = Campionato.Fine;
        interfaccia.StagioneId = Campionato.StagioneId;

        if (interfaccia.Id != 0)
        {
            idsSquadreSelezionate = await db.SquadreCampionati
                .Where(sc => sc.CampionatoId == interfaccia.Id)
                .Select(sc => sc.SquadraId)
                .ToListAsync();
        }
    }

    void OnSubmit()
    {
        // passaggio degli ID selezionati dalla listbox all'oggetto interfaccia
        interfaccia.SquadreSelezionateIds = idsSquadreSelezionate;
        
        dialogService.Close(interfaccia);
    }
    void Cancel() => dialogService.Close(null);
}