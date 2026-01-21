using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Fonte> Fontes => Set<Fonte>();
    public DbSet<Bloco> Blocos => Set<Bloco>();
    public DbSet<Banco> Bancos => Set<Banco>();
    public DbSet<TipoAto> TiposAto => Set<TipoAto>();
}
