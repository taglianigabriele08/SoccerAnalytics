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

        // In CalendarioVisivoBase.cs
    protected DateTime DataSelezionata { get; set; } = DateTime.Now;

    // In CalendarioVisivoBase.cs
    protected override async Task OnInitializedAsync()
    {
        // Rimuoviamo il filtro .Where per un momento
        var tutteLePartite = await db.Partite.ToListAsync();
        Console.WriteLine($"DEBUG: Partite totali nel database: {tutteLePartite.Count}");
        
        partite = tutteLePartite; 
        
        if (partite.Any()) {
            DataSelezionata = partite.First().Data;
        }
        StateHasChanged();
    }

        // Il metodo OnAppointmentMouseEnter rimane vuoto o rimosso qui 
        // perché lo gestiremo direttamente nel file .razor per evitare il Builder
        protected void OnAppointmentMouseLeave(SchedulerAppointmentMouseEventArgs<Partita> args)
        {
            TooltipService.Close();
        }
    }
}