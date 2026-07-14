namespace StockManagerPro.API.Services;

public class DashboardStats
{
    public int TotalProduits { get; set; }
    public int TotalCategories { get; set; }
    public int ProduitsEnAlerte { get; set; }
    public int MouvementsAujourdhui { get; set; }
}

public interface IDashboardService
{
    Task<DashboardStats> GetStatsAsync();
}