namespace Cerebotani.GabrieleTagliani.Soccer.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Stagione> Stagioni { get; set; }
    public DbSet<Campionato> Campionati { get; set; }
    public DbSet<Squadra> Squadre { get; set; }
    public DbSet<SquadraCampionato> SquadreCampionati { get; set; }
    public DbSet<Partita> Partite { get; set; }
    public DbSet<Marcatore> Marcatori { get; set; }
}
