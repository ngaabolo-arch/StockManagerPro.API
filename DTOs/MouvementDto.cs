namespace StockManagerPro.API.DTOs
{
    public class MouvementDto
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public int Quantite { get; set; }
        public string Motif { get; set; } = string.Empty;
        public DateTime DateMouvement { get; set; }
        public string ProduitNom { get; set; } = string.Empty;
    }

    public class MouvementCreateDto
    {
        public int ProduitId { get; set; }
        public int Quantite { get; set; }
        public string Motif { get; set; } = string.Empty;
    }
}