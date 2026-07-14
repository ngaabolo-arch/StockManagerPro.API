using Microsoft.EntityFrameworkCore;
using StockManagerPro.API.Data;
using StockManagerPro.API.DTOs;
using StockManagerPro.API.Models;

namespace StockManagerPro.API.Services;

public class MouvementService : IMouvementService
{
    private readonly AppDbContext _context;

    public MouvementService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MouvementDto>> GetAllAsync()
    {
        return await _context.MouvementsStock
            .Include(m => m.Produit)
            .Select(m => new MouvementDto
            {
                Id = m.Id,
                Type = m.Type,
                Quantite = m.Quantite,
                Motif = m.Motif,
                DateMouvement = m.DateMouvement,
                ProduitNom = m.Produit.Nom
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<MouvementDto>> GetByProduitAsync(int produitId)
    {
        return await _context.MouvementsStock
            .Include(m => m.Produit)
            .Where(m => m.ProduitId == produitId)
            .Select(m => new MouvementDto
            {
                Id = m.Id,
                Type = m.Type,
                Quantite = m.Quantite,
                Motif = m.Motif,
                DateMouvement = m.DateMouvement,
                ProduitNom = m.Produit.Nom
            })
            .ToListAsync();
    }

    public async Task<MouvementDto> EntreeAsync(MouvementCreateDto dto)
    {
        var produit = await _context.Produits.FindAsync(dto.ProduitId);
        if (produit == null) throw new Exception("Produit introuvable");

        produit.QuantiteEnStock += dto.Quantite;
        produit.DateModification = DateTime.Now;

        var mouvement = new MouvementStock
        {
            Type = "ENTREE",
            Quantite = dto.Quantite,
            Motif = dto.Motif,
            DateMouvement = DateTime.Now,
            ProduitId = dto.ProduitId
        };

        _context.MouvementsStock.Add(mouvement);
        await _context.SaveChangesAsync();

        return new MouvementDto
        {
            Id = mouvement.Id,
            Type = mouvement.Type,
            Quantite = mouvement.Quantite,
            Motif = mouvement.Motif,
            DateMouvement = mouvement.DateMouvement,
            ProduitNom = produit.Nom
        };
    }

    public async Task<MouvementDto> SortieAsync(MouvementCreateDto dto)
    {
        var produit = await _context.Produits.FindAsync(dto.ProduitId);
        if (produit == null) throw new Exception("Produit introuvable");

        if (produit.QuantiteEnStock < dto.Quantite)
            throw new InvalidOperationException($"Stock insuffisant. Stock actuel : {produit.QuantiteEnStock}");

        produit.QuantiteEnStock -= dto.Quantite;
        produit.DateModification = DateTime.Now;

        var mouvement = new MouvementStock
        {
            Type = "SORTIE",
            Quantite = dto.Quantite,
            Motif = dto.Motif,
            DateMouvement = DateTime.Now,
            ProduitId = dto.ProduitId
        };

        _context.MouvementsStock.Add(mouvement);
        await _context.SaveChangesAsync();

        return new MouvementDto
        {
            Id = mouvement.Id,
            Type = mouvement.Type,
            Quantite = mouvement.Quantite,
            Motif = mouvement.Motif,
            DateMouvement = mouvement.DateMouvement,
            ProduitNom = produit.Nom
        };
    }
}