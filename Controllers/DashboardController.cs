using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockManagerPro.API.Data;
using Microsoft.AspNetCore.Authorization;

namespace StockManagerPro.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly AppDbContext _context;

    public DashboardController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetStats()
    {
        var today = DateTime.Today;

        var stats = new
        {
            TotalProduits = await _context.Produits.CountAsync(),
            TotalCategories = await _context.Categories.CountAsync(),
            ProduitsEnAlerte = await _context.Produits
                .CountAsync(p => p.QuantiteEnStock <= p.SeuilAlerte),
            MouvementsAujourdhui = await _context.MouvementsStock
                .CountAsync(m => m.DateMouvement.Date == today)
        };

        return Ok(stats);
    }
}