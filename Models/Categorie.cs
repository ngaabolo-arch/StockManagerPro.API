namespace StockManagerPro.API.Models
{
    public class Categorie
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DateCreation { get; set; } = DateTime.Now;
    }
}
