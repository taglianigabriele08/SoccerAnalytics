using Microsoft.AspNetCore.Components;

namespace Cerebotani.GabrieleTagliani.Soccer.Components.Pages;

public partial class Classifica
{
    [Parameter] public int CampionatoId { get; set; }
    [Inject] private NavigationManager Nav { get; set; } = default!;

    private List<SquadraCampionato> classifica = new();
    private string nomeCampionato = "";

    protected override async Task OnInitializedAsync()
    {
        var camp = await db.Campionati
                        .FirstOrDefaultAsync(c => c.Id == CampionatoId);
        nomeCampionato = camp?.Descrizione ?? "Campionato";

        classifica = await db.SquadreCampionati
                    .Include(sc => sc.Squadra)
                    .Where(sc => sc.CampionatoId == CampionatoId)
                    .OrderByDescending(sc => sc.Punti)
                    .ThenByDescending(sc => sc.DifferenzaReti)
                    .ThenByDescending(sc => sc.GolFatti)
                    .ToListAsync();

    }      

    void GoBack() => Nav.NavigateTo("/campionati");
}
