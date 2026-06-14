namespace StockManagerPro.API.Models
{
    public class MouvementStock
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty; // "ENTREE OU SORTIE"
        public int Quantite { get; set; }
        public string Motif { get; set; } = string.Empty;
        public DateTime DateMouvement { get; set; } = DateTime.Now;

        public int ProduitId { get; set; }
        public Produit Produit { get; set; } = null!;

    }
}
