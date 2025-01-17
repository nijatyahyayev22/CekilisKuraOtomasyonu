
using Microsoft.EntityFrameworkCore;
public class CekilisOtomasyonuDbContext : DbContext
{
    public DbSet<Kullanici> Kullanici { get; set; }
    public DbSet<Cekilis> Cekilis { get; set; }
    public DbSet<Katilimci> Katilimci { get; set; }
    public DbSet<Sonuc> Sonuc { get; set; }
    public CekilisOtomasyonuDbContext(DbContextOptions<CekilisOtomasyonuDbContext> options)
            : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cekilis>()
            .HasOne(c => c.Kullanici)
            .WithMany(k => k.Cekilisler)
            .HasForeignKey(c => c.KullaniciID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Katilimci>()
            .HasOne(k => k.Cekilis)
            .WithMany(c => c.Katilimcilar)
            .HasForeignKey(k => k.CekilisID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Sonuc>()
            .HasOne(s => s.Cekilis)
            .WithMany(c=> c.Sonuclar) 
            .HasForeignKey(s => s.CekilisID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Sonuc>()
            .HasOne(s => s.Katilimci)
            .WithMany()
            .HasForeignKey(s => s.KatilimciID)
            .OnDelete(DeleteBehavior.Restrict);
    }

}
