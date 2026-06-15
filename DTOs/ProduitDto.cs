namespace StockManagerPro.API.DTOs
{
    public class ProduitDto
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Prix { get; set; }
        public int QuantiteEnStock { get; set; }
        public int SeuilAlerte { get; set; }
        public string CategorieNom { get; set; } = string.Empty;
        public string FournisseurNom { get; set; } = string.Empty;
    }

    public class ProduitCreateDto
    {
        public string Nom { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Prix { get; set; }
        public int QuantiteEnStock { get; set; }
        public int SeuilAlerte { get; set; }
        public int CategorieId { get; set; }
        public int FournisseurId { get; set; }
    }
}