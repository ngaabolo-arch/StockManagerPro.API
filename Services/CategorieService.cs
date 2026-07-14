using Microsoft.EntityFrameworkCore;
using StockManagerPro.API.Data;
using StockManagerPro.API.DTOs;
using StockManagerPro.API.Models;

namespace StockManagerPro.API.Services;

public class CategorieService : ICategorieService
{
    private readonly AppDbContext _context;

    public CategorieService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CategorieDto>> GetAllAsync()
    {
        return await _context.Categories
            .Select(c => new CategorieDto
            {
                Id = c.Id,
                Nom = c.Nom,
                Description = c.Description
            })
            .ToListAsync();
    }

    public async Task<CategorieDto?> GetByIdAsync(int id)
    {
        var categorie = await _context.Categories.FindAsync(id);
        if (categorie == null) return null;

        return new CategorieDto
        {
            Id = categorie.Id,
            Nom = categorie.Nom,
            Description = categorie.Description
        };
    }

    public async Task<CategorieDto> CreateAsync(CategorieCreateDto dto)
    {
        var categorie = new Categorie
        {
            Nom = dto.Nom,
            Description = dto.Description
        };

        _context.Categories.Add(categorie);
        await _context.SaveChangesAsync();

        return new CategorieDto
        {
            Id = categorie.Id,
            Nom = categorie.Nom,
            Description = categorie.Description
        };
    }

    public async Task<bool> UpdateAsync(int id, CategorieCreateDto dto)
    {
        var categorie = await _context.Categories.FindAsync(id);
        if (categorie == null) return false;

        categorie.Nom = dto.Nom;
        categorie.Description = dto.Description;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var categorie = await _context.Categories.FindAsync(id);
        if (categorie == null) return false;

        _context.Categories.Remove(categorie);
        await _context.SaveChangesAsync();
        return true;
    }
}