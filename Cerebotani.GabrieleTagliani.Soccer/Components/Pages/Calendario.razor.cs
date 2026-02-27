using Microsoft.AspNetCore.Components;

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
                .Where(p => p.CampionatoId == CampionatoId)
                .OrderBy(p => p.Giornata)
                .ToListAsync();
        }

        protected void TornaIndietro()
        {
            NavigationManager.NavigateTo("/campionati");
        }
    }
}