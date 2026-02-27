using Microsoft.AspNetCore.Components;
using System.Text.Json; // Usa questo per JsonSerializer
using System.Text.Json.Serialization;


namespace Cerebotani.GabrieleTagliani.Soccer.Components.Pages;

public partial class Campionati(DialogService dialogService,
                                NotificationService notificationService,
                                ApplicationDbContext db)
{
    private RadzenDataGrid<Campionato> Grid { get; set; } = default!;
    private IEnumerable<Campionato> Items { get; set; } = default!;

    private RadzenUpload? UploadComponent { get; set; }

    async Task RefreshDataAsync()
    {
        var dati = await db.Campionati
            .Include(c => c.Stagione)
            .Include(c => c.SquadreCampionato)
            .ThenInclude(sc => sc.Squadra)
            .ToListAsync();

        foreach (var item in dati)
        {

            if (item.Stagione == null)
            {
                item.Stagione = new Stagione { Descrizione = "Senza Stagione" };
            }
        }

        Items = dati;
    }
    protected override async Task OnInitializedAsync()
    {
        await RefreshDataAsync();
    }

    void OnRender(DataGridRenderEventArgs<Campionato> args)
    {
        if (args.FirstRender)
        {
            var descriptor = new GroupDescriptor()
            {
                Title = "Stagione",
                Property = "Stagione.Descrizione",
                SortOrder = SortOrder.Descending
            };

            if (!args.Grid.Groups.Any(g => g.Property == descriptor.Property))
            {
                args.Grid.Groups.Add(descriptor);
            }
        }
    }

    void OnGroupRowRender(GroupRowRenderEventArgs args)
    {
        args.Expanded = true;
    }

    // async Task OnUpload(UploadChangeEventArgs args, Campionato campionatoContext)
    // {
    //     try
    //     {
    //         // recupero del file
    //         var file = args.Files?.FirstOrDefault();
    //         if (file == null) return;

    //         // leggi file
    //         using var stream = file.OpenReadStream(maxAllowedSize: 1024 * 1024 * 2);
    //         var datiRaw = await JsonSerializer.DeserializeAsync<List<Dat1>>(stream,
    //             new JsonSerializerOptions { PropertyNameCaseInsensitive = true, AllowTrailingCommas = true });

    //         if (datiRaw == null) return;
    //         var dati = datiRaw.OrderBy(d => d.Posizione).ToList();

    //         // reset pulsante
    //         UploadComponent?.ClearFiles();

    //         // messaggio
    //         if (dati != null)
    //         {
    //             foreach (var item in dati)
    //             {
    //                 var squadraDb = await db.Squadre
    //                         .FirstOrDefaultAsync(s => s.Nome.ToLower() == item.Squadra.ToLower());

    //                 if (squadraDb == null)
    //                 {
    //                     squadraDb = new Squadra
    //                     {
    //                         Nome = item.Squadra,
    //                         LogoUrl = item.LogoUrl,
    //                         Anno = 0,
    //                         Citta = "N.D.",
    //                         Stadio = "N.D."
    //                     };
    //                     db.Squadre.Add(squadraDb);
    //                     await db.SaveChangesAsync();
    //                 }

    //                 var legame = await db.SquadreCampionati
    //                         .FirstOrDefaultAsync(sc => sc.CampionatoId == campionatoContext.Id
    //                                                     && sc.SquadraId == squadraDb.Id);

    //                 if (legame == null)
    //                 {
    //                     legame = new SquadraCampionato
    //                     {
    //                         // aggiunge al campionato la squadra, con le proprie statistiche
    //                         CampionatoId = campionatoContext.Id,
    //                         SquadraId = squadraDb.Id,
    //                     };
    //                     db.SquadreCampionati.Add(legame);
    //                 }

    //                 legame.PartiteGiocate = item.PartiteGiocate;
    //                 legame.Vittorie = item.Vittorie;
    //                 legame.Pareggi = item.Pareggi;
    //                 legame.Sconfitte = item.Sconfitte;
    //                 legame.GolFatti = item.GolFatti;
    //                 legame.GolSubiti = item.GolSubiti;
    //                 legame.DifferenzaReti = item.DifferenzaReti;
    //                 legame.Punti = item.Punti;
    //                 await db.SaveChangesAsync();
    //             }

    //             await RefreshDataAsync();
    //             if (Grid != null) await Grid.Reload();

    //             notificationService.Notify(new NotificationMessage
    //             {
    //                 Severity = NotificationSeverity.Success,
    //                 Summary = "Importazione Riuscita",
    //                 Detail = $"Aggiornate {dati.Count} squadre per {campionatoContext.Descrizione}"
    //             });
    //         }
    //     }
    //     catch (JsonException jsonEx)
    //     {
    //         notificationService.Notify(new NotificationMessage
    //         {
    //             Severity = NotificationSeverity.Error,
    //             Summary = "Errore Formato JSON",
    //             Detail = $"Il file non è un JSON valido. Errore alla riga {jsonEx.LineNumber}: {jsonEx.Message}"
    //         });
    //     }
    //     catch (Exception ex)
    //     {
    //         notificationService.Notify(new NotificationMessage
    //         {
    //             Severity = NotificationSeverity.Error,
    //             Summary = "Errore Importazione",
    //             Detail = ex.Message
    //         });
    //     }
    // }


    async Task OnUpload(UploadChangeEventArgs args, Campionato campionatoContext)
    {
        try
        {
            var file = args.Files?.FirstOrDefault();
            if (file == null) return;

            // 1. Leggi il file JSON (strutturato come lista di partite)
            using var stream = file.OpenReadStream(maxAllowedSize: 1024 * 1024 * 5);
            var partiteImportate = await JsonSerializer.DeserializeAsync<List<PartitaDTO>>(stream,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true, AllowTrailingCommas = true });

            if (partiteImportate == null || !partiteImportate.Any()) return;

            // cancellazione delle partite precedenti
            var partiteVecchie = await db.Partite
                .Where(p => p.CampionatoId == campionatoContext.Id)
                .ToListAsync();
            db.Partite.RemoveRange(partiteVecchie);

            // reset dei dati
            var legamiClassifica = await db.SquadreCampionati
                .Where(sc => sc.CampionatoId == campionatoContext.Id)
                .ToListAsync();

            foreach (var l in legamiClassifica)
            {
                l.Punti = 0; l.Vittorie = 0; l.Pareggi = 0; l.Sconfitte = 0;
                l.GolFatti = 0; l.GolSubiti = 0; l.DifferenzaReti = 0; l.PartiteGiocate = 0;
            }

            foreach (var p in partiteImportate)
            {
                var nuovaPartita = new Partita
                {
                    CampionatoId = campionatoContext.Id,
                    Giornata = p.Giornata,
                    Data = p.Data,
                    SquadraCasa = p.SquadraCasa,
                    SquadraTrasferta = p.SquadraTrasferta,
                    GolCasa = p.GolCasa,
                    GolTrasferta = p.GolTrasferta,

                    Marcatori = p.Marcatori?.Select(m => new Marcatore
                    {
                        Nome = m.Nome,
                        Minuto = m.Minuto,
                        Recupero = m.Recupero,
                        Rigore = m.Rigore,
                        SquadraNome = m.SquadraNome
                    }).ToList() ?? new List<Marcatore>()
                };
                db.Partite.Add(nuovaPartita);

                // B. Aggiorna Classifica (Casa)
                await AggiornaStatisticheSquadra(campionatoContext.Id, p.SquadraCasa, p.GolCasa, p.GolTrasferta);
                // C. Aggiorna Classifica (Trasferta)
                await AggiornaStatisticheSquadra(campionatoContext.Id, p.SquadraTrasferta, p.GolTrasferta, p.GolCasa);
            }

            await db.SaveChangesAsync();
            UploadComponent?.ClearFiles();
            await RefreshDataAsync();
            if (Grid != null) await Grid.Reload();

            notificationService.Notify(new NotificationMessage
            {
                Severity = NotificationSeverity.Success,
                Summary = "Successo",
                Detail = $"Calendario e Classifica aggiornati per {campionatoContext.Descrizione}"
            });
        }
        catch (Exception ex)
        {
            notificationService.Notify(new NotificationMessage
            {
                Severity = NotificationSeverity.Error,
                Summary = "Errore",
                Detail = ex.Message
            });
        }
    }


    private async Task AggiornaStatisticheSquadra(int campId, string nomeSquadra, int? golFatti, int? golSubiti)
    {
        if (!golFatti.HasValue || !golSubiti.HasValue) return;

        // 1. PULIZIA NOME: Rimuove spazi bianchi e caratteri invisibili
        string nomePulito = nomeSquadra.Trim();

        // 2. CERCA NEL DB (usiamo ToLower e Trim anche qui per sicurezza)
        var squadra = await db.Squadre
            .FirstOrDefaultAsync(s => s.Nome.ToLower() == nomePulito.ToLower());

        // 3. SE NON ESISTE, CREALA
        if (squadra == null)
        {
            squadra = new Squadra
            {
                Nome = nomePulito,
                Citta = "N.D.",
                Stadio = "N.D."
            };
            db.Squadre.Add(squadra);

            // IMPORTANTE: Salviamo subito qui per generare l'ID, 
            // ma solo se la squadra è nuova di zecca.
            await db.SaveChangesAsync();
        }

        // 4. CERCA IL LEGAME CLASSIFICA
        var legame = await db.SquadreCampionati
            .FirstOrDefaultAsync(sc => sc.CampionatoId == campId && sc.SquadraId == squadra.Id);

        if (legame == null)
        {
            legame = new SquadraCampionato
            {
                CampionatoId = campId,
                SquadraId = squadra.Id,
                // Inizializziamo a zero per sicurezza
                Punti = 0,
                Vittorie = 0,
                Pareggi = 0,
                Sconfitte = 0,
                GolFatti = 0,
                GolSubiti = 0,
                DifferenzaReti = 0,
                PartiteGiocate = 0
            };
            db.SquadreCampionati.Add(legame);
            // Salviamo il legame per evitare che la prossima partita ne crei un altro uguale
            await db.SaveChangesAsync();
        }

        // 5. AGGIORNA STATISTICHE
        legame.PartiteGiocate++;
        legame.GolFatti += golFatti.Value;
        legame.GolSubiti += golSubiti.Value;
        legame.DifferenzaReti = legame.GolFatti - legame.GolSubiti;

        if (golFatti.Value > golSubiti.Value)
        {
            legame.Vittorie++;
            legame.Punti += 3;
        }
        else if (golFatti.Value == golSubiti.Value)
        {
            legame.Pareggi++;
            legame.Punti += 1;
        }
        else
        {
            legame.Sconfitte++;
        }
    }


    async Task SalvaAssociazioniSquadre(int campionatoId, IEnumerable<int> squadreIds)
    {
        var vecchie = db.SquadreCampionati.Where(sc => sc.CampionatoId == campionatoId);
        db.SquadreCampionati.RemoveRange(vecchie);

        if (squadreIds != null)
        {
            foreach (var squadraId in squadreIds)
            {
                db.SquadreCampionati.Add(
                    new SquadraCampionato
                    {
                        CampionatoId = campionatoId,
                        SquadraId = squadraId
                    });
            }
        }
        await db.SaveChangesAsync();
    }

    async Task OpenDetails(Campionato campionatoSelezionato)
    {
        var result = await dialogService.OpenAsync<DettaglioCampionatoDialog>(
            $"Modifica {campionatoSelezionato.Descrizione}",
            new Dictionary<string, object?>() { { "Campionato", campionatoSelezionato } },
            new DialogOptions() { Width = "600px" }
        );

        if (result is Campionato campionatoModificato)
        {
            db.Entry(campionatoSelezionato).CurrentValues.SetValues(campionatoModificato);
            await db.SaveChangesAsync();
            await SalvaAssociazioniSquadre(campionatoSelezionato.Id, campionatoModificato.SquadreSelezionateIds);
            await RefreshDataAsync();
            await Grid.Reload();
        }
    }

    async Task InsertRow() => await CreazioneNuovoCampionato();
    async Task InsertAfterRow() => await CreazioneNuovoCampionato();

    async Task CreazioneNuovoCampionato()
    {
        var nuovo = new Campionato { Inizio = DateTime.Now, Fine = DateTime.Now.AddMonths(9) };

        var result = await dialogService.OpenAsync<DettaglioCampionatoDialog>(
            "Nuovo Campionato",
            new Dictionary<string, object?>() { { "Campionato", nuovo } },
            new DialogOptions() { Width = "600px" }
        );

        if (result is Campionato daInserire)
        {
            db.Campionati.Add(daInserire);
            await db.SaveChangesAsync();
            await SalvaAssociazioniSquadre(daInserire.Id, daInserire.SquadreSelezionateIds);
            await RefreshDataAsync();
            await Grid.Reload();
        }
    }

    void NavigaClassifica(int id)
    {
        NavigationManager.NavigateTo($"/classifica/{id}");
    }

    void NavigaCalendario(int campionatoId)
    {
        NavigationManager.NavigateTo($"/calendario/{campionatoId}");
    }
    async Task ConfirmDelete(Campionato campionato)
    {
        var confirm = await dialogService.Confirm(
            $"Sei sicuro di voler eliminare {campionato.Descrizione}?",
            "Conferma");

        if (confirm == true)
        {
            db.Campionati.Remove(campionato);
            await db.SaveChangesAsync();
            await RefreshDataAsync();
            await Grid.Reload();
        }
    }

    // Passi il contenuto del JSON e l'ID del campionato a cui appartiene
    public async Task ImportaDatiCampionato(string jsonContent, int campionatoId)
    {
        try
        {
            // Usa JsonSerializer di System.Text.Json
            var partiteJson = JsonSerializer.Deserialize<List<Partita>>(jsonContent,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (partiteJson != null)
            {
                foreach (var p in partiteJson)
                {
                    p.CampionatoId = campionatoId;
                    db.Partite.Add(p);
                }

                await db.SaveChangesAsync();
                notificationService.Notify(NotificationSeverity.Success, "Successo", "Importazione completata.");
            }
        }
        catch (Exception ex)
        {
            notificationService.Notify(NotificationSeverity.Error, "Errore", ex.Message);
        }
    }
}



public class PartitaDTO
{
    public int Giornata { get; set; }
    public DateTime Data { get; set; }
    public string SquadraCasa { get; set; } = string.Empty;
    public string SquadraTrasferta { get; set; } = string.Empty;
    public int? GolCasa { get; set; }
    public int? GolTrasferta { get; set; }
    public List<MarcatoreDTO>? Marcatori { get; set; } = new();
}

public class MarcatoreDTO
{
    public string? Nome { get; set; }
    public int Minuto { get; set; }
    public int? Recupero { get; set; }
    public bool Rigore { get; set; }
    public string? SquadraNome { get; set; }
}