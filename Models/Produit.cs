using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockManagerPro.API.Models
{
    public class Produit
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,3)")]
        public decimal Prix { get; set; }
        public int QuantiteEnStock { get; set; }
        public int SeuilAlerte { get; set; }
        public DateTime DateCreation { get; set; } = DateTime.Now;
        public DateTime DateModification { get; set; } = DateTime.Now;

        //cle etrangeres
        public int CategorieId { get; set; }
        public Categorie Categorie { get; set; } = null!;
        public int FournisseurId { get; set; }
        public Fournisseur Fournisseur { get; set; } = null!;
    }
}
