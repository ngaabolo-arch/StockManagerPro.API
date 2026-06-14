using Microsoft.EntityFrameworkCore;
using StockManagerPro.API.Models;

namespace StockManagerPro.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Categorie> Categories { get; set; }
        public DbSet<Fournisseur> Fournisseurs { get; set; }
        public DbSet<Produit> Produits { get; set; }
        public DbSet<MouvementStock> MouvementsStock { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }
    }
}
