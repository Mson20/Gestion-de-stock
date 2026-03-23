using Microsoft.EntityFrameworkCore;
using GestionStock.API.Models;

namespace GestionStock.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Produit> Produits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Données de test au démarrage
            modelBuilder.Entity<Produit>().HasData(
                new Produit { Id = 1, Nom = "Câble USB-C", Categorie = "Électronique", Stock = 142, SeuilAlerte = 10, Prix = 9.99m, Fournisseur = "TechPro" },
                new Produit { Id = 2, Nom = "Hub HDMI", Categorie = "Électronique", Stock = 3, SeuilAlerte = 5, Prix = 29.99m, Fournisseur = "TechPro" },
                new Produit { Id = 3, Nom = "Webcam HD", Categorie = "Périphériques", Stock = 0, SeuilAlerte = 5, Prix = 49.99m, Fournisseur = "Vision+" },
                new Produit { Id = 4, Nom = "Souris sans fil", Categorie = "Périphériques", Stock = 38, SeuilAlerte = 8, Prix = 24.99m, Fournisseur = "Vision+" },
                new Produit { Id = 5, Nom = "Clavier mécanique", Categorie = "Périphériques", Stock = 12, SeuilAlerte = 5, Prix = 89.99m, Fournisseur = "KeyMaster" }
            );
        }
    }
}