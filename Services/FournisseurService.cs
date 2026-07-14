using Microsoft.EntityFrameworkCore;
using StockManagerPro.API.Data;
using StockManagerPro.API.DTOs;
using StockManagerPro.API.Models;

namespace StockManagerPro.API.Services;

public class FournisseurService : IFournisseurService
{
    private readonly AppDbContext _context;

    public FournisseurService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<FournisseurDto>> GetAllAsync()
    {
        return await _context.Fournisseurs
            .Select(f => new FournisseurDto
            {
                Id = f.Id,
                Nom = f.Nom,
                Email = f.Email,
                Telephone = f.Telephone,
                Adresse = f.Adresse
            })
            .ToListAsync();
    }

    public async Task<FournisseurDto?> GetByIdAsync(int id)
    {
        var fournisseur = await _context.Fournisseurs.FindAsync(id);
        if (fournisseur == null) return null;

        return new FournisseurDto
        {
            Id = fournisseur.Id,
            Nom = fournisseur.Nom,
            Email = fournisseur.Email,
            Telephone = fournisseur.Telephone,
            Adresse = fournisseur.Adresse
        };
    }

    public async Task<IEnumerable<ProduitDto>> GetProduitsAsync(int id)
    {
        return await _context.Produits
            .Where(p => p.FournisseurId == id)
            .Include(p => p.Categorie)
            .Include(p => p.Fournisseur)
            .Select(p => new ProduitDto
            {
                Id = p.Id,
                Nom = p.Nom,
                Description = p.Description,
                Prix = p.Prix,
                QuantiteEnStock = p.QuantiteEnStock,
                SeuilAlerte = p.SeuilAlerte,
                CategorieNom = p.Categorie.Nom,
                FournisseurNom = p.Fournisseur.Nom
            })
            .ToListAsync();
    }

    public async Task<FournisseurDto> CreateAsync(FournisseurCreateDto dto)
    {
        var fournisseur = new Fournisseur
        {
            Nom = dto.Nom,
            Email = dto.Email,
            Telephone = dto.Telephone,
            Adresse = dto.Adresse
        };

        _context.Fournisseurs.Add(fournisseur);
        await _context.SaveChangesAsync();

        return new FournisseurDto
        {
            Id = fournisseur.Id,
            Nom = fournisseur.Nom,
            Email = fournisseur.Email,
            Telephone = fournisseur.Telephone,
            Adresse = fournisseur.Adresse
        };
    }

    public async Task<bool> UpdateAsync(int id, FournisseurCreateDto dto)
    {
        var fournisseur = await _context.Fournisseurs.FindAsync(id);
        if (fournisseur == null) return false;

        fournisseur.Nom = dto.Nom;
        fournisseur.Email = dto.Email;
        fournisseur.Telephone = dto.Telephone;
        fournisseur.Adresse = dto.Adresse;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var fournisseur = await _context.Fournisseurs.FindAsync(id);
        if (fournisseur == null) return false;

        _context.Fournisseurs.Remove(fournisseur);
        await _context.SaveChangesAsync();
        return true;
    }
}