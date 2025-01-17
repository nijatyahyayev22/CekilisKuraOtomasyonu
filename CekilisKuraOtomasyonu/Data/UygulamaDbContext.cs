
using Microsoft.EntityFrameworkCore;

public class UygulamaDbContext : DbContext
{
    public DbSet<Kullanici> Kullanici { get; set; }
    public DbSet<Cekilis> Cekilis { get; set; }
    public DbSet<Katilimci> Katilimci { get; set; }
    public DbSet<Sonuc> Sonuc { get; set; }
    public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options)
            : base(options)
        {
        }
}
