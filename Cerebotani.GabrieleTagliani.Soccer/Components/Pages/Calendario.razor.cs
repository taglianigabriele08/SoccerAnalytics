using Microsoft.AspNetCore.Components;
using Cerebotani.GabrieleTagliani.Soccer.Components.Pages;

namespace Cerebotani.GabrieleTagliani.Soccer
{
    public partial class CalendarioBase : ComponentBase
    {
        [Parameter] public int CampionatoId { get; set; }

        [Inject] protected ApplicationDbContext db { get; set; } = default!;
        [Inject] protected NavigationManager NavigationManager { get; set; } = default!;

        protected List<Partita>? Partite;

        protected override async Task OnInitializedAsync()
        {
            // Uso il DB direttamente: più semplice e niente errori HTTP
            Partite = await db.Partite
                .Include(p => p.Marcatori)
                .Where(p => p.CampionatoId == CampionatoId)
                .OrderBy(p => p.Giornata)
                .ToListAsync();
        }

        protected void TornaIndietro()
        {
            NavigationManager.NavigateTo("/campionati");
        }

        public async Task DettagliPartita(Partita partita)
        {
            await DialogService.OpenAsync<DettagliPartitaDialog>(
                $"Dettagli Match",
                new Dictionary<string, object?>() { { "Match", partita } },
                new DialogOptions() { Width = "600px", Height = "auto" }
            );
        }
    }
}