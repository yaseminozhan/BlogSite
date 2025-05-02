using BlogSite.UI.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogSite.UI.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet'ler
        public DbSet<Makale> Makaleler { get; set; }
        public DbSet<Konu> Konular { get; set; }
        public DbSet<MakaleKonu> MakaleKonular { get; set; }
        public DbSet<TakipEdilenKonu> TakipEdilenKonular { get; set; }
        public DbSet<TakipEdilenYazar> TakipEdilenYazarlar { get; set; }
        public DbSet<Yorum> Yorumlar { get; set; }
        public DbSet<Begeni> Begeniler { get; set; }
        public DbSet<Bildirim> Bildirimler { get; set; }
        public DbSet<Mesaj> Mesajlar { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MakaleKonu>()
                .HasKey(mk => new { mk.MakaleId, mk.KonuId });

            modelBuilder.Entity<TakipEdilenYazar>()
                .HasIndex(x => new { x.TakipEdenId, x.TakipEdilenId })
                .IsUnique();

            modelBuilder.Entity<TakipEdilenYazar>()
                .HasOne(x => x.TakipEden)
                .WithMany()
                .HasForeignKey(x => x.TakipEdenId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TakipEdilenYazar>()
                .HasOne(x => x.TakipEdilen)
                .WithMany()
                .HasForeignKey(x => x.TakipEdilenId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Mesaj>()
                .HasOne(m => m.Gonderen)
                .WithMany()
                .HasForeignKey(m => m.GonderenId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Mesaj>()
                .HasOne(m => m.Alici)
                .WithMany()
                .HasForeignKey(m => m.AliciId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Yorum>()
                .HasOne(y => y.Kullanici)
                .WithMany()
                .HasForeignKey(y => y.KullaniciId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Yorum>()
                .HasOne(y => y.Makale)
                .WithMany()
                .HasForeignKey(y => y.MakaleId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Begeni>()
                .HasOne(b => b.Kullanici)
                .WithMany()
                .HasForeignKey(b => b.KullaniciId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Begeni>()
                .HasOne(b => b.Makale)
                .WithMany()
                .HasForeignKey(b => b.MakaleId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Bildirim>()
                .HasOne(b => b.Kullanici)
                .WithMany()
                .HasForeignKey(b => b.KullaniciId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TakipEdilenKonu>()
                .HasOne(t => t.Kullanici)
                .WithMany()
                .HasForeignKey(t => t.KullaniciId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TakipEdilenKonu>()
                .HasOne(t => t.Konu)
                .WithMany()
                .HasForeignKey(t => t.KonuId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Makale>()
                .HasOne(m => m.Yazar)
                .WithMany()
                .HasForeignKey(m => m.YazarId)
                .OnDelete(DeleteBehavior.NoAction);
        }


    }
}
