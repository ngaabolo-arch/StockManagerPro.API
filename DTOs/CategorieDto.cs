namespace StockManagerPro.API.DTOs
{
    public class CategorieDto
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class CategorieCreateDto
    {
        public string Nom { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

    }
}
