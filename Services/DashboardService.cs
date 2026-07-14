using Microsoft.EntityFrameworkCore;
using StockManagerPro.API.Data;

namespace StockManagerPro.API.Services;

public class DashboardService : IDashboardService
{
    private readonly AppDbContext _context;

    public DashboardService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<DashboardStats> GetStatsAsync()
    {
        var aujourd = DateTime.Today;

        return new DashboardStats
        {
            TotalProduits = await _context.Produits.CountAsync(),
            TotalCategories = await _context.Categories.CountAsync(),
            ProduitsEnAlerte = await _context.Produits
                .CountAsync(p => p.QuantiteEnStock <= p.SeuilAlerte),
            MouvementsAujourdhui = await _context.MouvementsStock
                .CountAsync(m => m.DateMouvement >= aujourd)
        };
    }
}