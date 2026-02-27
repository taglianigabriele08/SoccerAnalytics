using Microsoft.AspNetCore.Components;

namespace Cerebotani.GabrieleTagliani.Soccer.Components.Pages
{
    public partial class CalendarioVisivoBase : ComponentBase
    {
        [Parameter] public int CampionatoId { get; set; }

        [Inject] protected ApplicationDbContext db { get; set; } = default!;
        [Inject] protected TooltipService TooltipService { get; set; } = default!;

        protected RadzenScheduler<Partita> scheduler = default!;
        protected IList<Partita> partite = new List<Partita>();

        protected override async Task OnInitializedAsync()
        {
            // Carichiamo solo i dati essenziali della partita
            partite = await db.Partite
                .Where(p => p.CampionatoId == CampionatoId)
                .OrderBy(p => p.Data)
                .ToListAsync();
        }

        // Il metodo OnAppointmentMouseEnter rimane vuoto o rimosso qui 
        // perché lo gestiremo direttamente nel file .razor per evitare il Builder
        protected void OnAppointmentMouseLeave(SchedulerAppointmentMouseEventArgs<Partita> args)
        {
            TooltipService.Close();
        }
    }
}