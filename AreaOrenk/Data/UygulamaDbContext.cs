using Microsoft.EntityFrameworkCore;
using AreaOrenk.Models;

namespace AreaOrenk.Data
{
    public class UygulamaDbContext : DbContext
    {
        public UygulamaDbContext()
        {
        }

        public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options) : base(options)
        {
        }
        public DbSet<Slayt> Slaytlar { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Slayt>().HasData(
            new Slayt() { Id = 1, ResimYolu = "cilekliCup.jpg", Baslik = "Çilekli Cupcake", Aciklama = "Lezzetli çilekli cupcake", Sira = 1 },
            new Slayt() { Id = 2, ResimYolu = "cikolataliDondurma.jpg", Baslik = "Çikolatalı Dondurma", Aciklama = "Bol çikolatalı dondurma", Sira = 2 },
            new Slayt() { Id = 3, ResimYolu = "pancake.jpg", Baslik = "Pancake", Aciklama = "Yumuşacık pancake", Sira = 3 }
            );

        }

    }
}
