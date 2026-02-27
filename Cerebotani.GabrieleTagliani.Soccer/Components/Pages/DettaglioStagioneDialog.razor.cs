using Microsoft.AspNetCore.Components;

namespace Cerebotani.GabrieleTagliani.Soccer.Components.Pages;

public partial class DettaglioStagioneDialog
{
    [Inject] private DialogService DialogService { get; set; } = default!;
    [Parameter] public Stagione Stagione { get; set; } = new();

    private void OnSubmit(Stagione model)
    {
        DialogService.Close(model);
    }

    private void OnCancel()
    {
        DialogService.Close(null);
    }
}