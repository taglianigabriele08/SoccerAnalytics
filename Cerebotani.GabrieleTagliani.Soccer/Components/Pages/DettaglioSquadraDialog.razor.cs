using Microsoft.AspNetCore.Components;

namespace Cerebotani.GabrieleTagliani.Soccer.Components.Pages;

public partial class DettaglioSquadraDialog
{
    [Inject] private DialogService ds { get; set; } = default!;

    [Parameter] public Squadra Squadra { get; set; } = new();

    private Squadra interfaccia = new();

    protected override void OnInitialized()
    {
        interfaccia.Id = Squadra.Id;
        interfaccia.Nome = Squadra.Nome;
        interfaccia.Citta = Squadra.Citta;
        interfaccia.Stadio = Squadra.Stadio;
        interfaccia.LogoUrl = Squadra.LogoUrl; 
    }

    private void OnSubmit()
    {
        ds.Close(interfaccia);
    }

    private void Cancel()
    {
        ds.Close(null);
    }
}