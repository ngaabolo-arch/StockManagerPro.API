using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockManagerPro.API.Data;
using StockManagerPro.API.DTOs;
using StockManagerPro.API.Models;
using Microsoft.AspNetCore.Authorization;

namespace StockManagerPro.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FournisseursController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FournisseursController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FournisseurDto>>> GetAll()
        {
            var fournisseurs = await _context.Fournisseurs.ToListAsync();
            return Ok(fournisseurs.Select(f => new FournisseurDto
            {
                Id = f.Id,
                Nom = f.Nom,
                Email = f.Email,
                Telephone = f.Telephone,
                Adresse = f.Adresse
            }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FournisseurDto>> GetById(int id)
        {
            var fournisseur = await _context.Fournisseurs.FindAsync(id);
            if (fournisseur == null) return NotFound();

            return Ok(new FournisseurDto
            {
                Id = fournisseur.Id,
                Nom = fournisseur.Nom,
                Email = fournisseur.Email,
                Telephone = fournisseur.Telephone,
                Adresse = fournisseur.Adresse
            });
        }

        [HttpGet("{id}/produits")]
        public async Task<ActionResult> GetProduits(int id)
        {
            var fournisseur = await _context.Fournisseurs.FindAsync(id);
            if (fournisseur == null) return NotFound();

            var produits = await _context.Produits
                .Where(p => p.FournisseurId == id)
                .Select(p => new { p.Id, p.Nom, p.Prix, p.QuantiteEnStock })
                .ToListAsync();

            return Ok(produits);
        }

        [HttpPost]
        public async Task<ActionResult<FournisseurDto>> Create(FournisseurCreateDto dto)
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

            return CreatedAtAction(nameof(GetById), new { id = fournisseur.Id }, new FournisseurDto
            {
                Id = fournisseur.Id,
                Nom = fournisseur.Nom,
                Email = fournisseur.Email,
                Telephone = fournisseur.Telephone,
                Adresse = fournisseur.Adresse
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, FournisseurCreateDto dto)
        {
            var fournisseur = await _context.Fournisseurs.FindAsync(id);
            if (fournisseur == null) return NotFound();

            fournisseur.Nom = dto.Nom;
            fournisseur.Email = dto.Email;
            fournisseur.Telephone = dto.Telephone;
            fournisseur.Adresse = dto.Adresse;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var fournisseur = await _context.Fournisseurs.FindAsync(id);
            if (fournisseur == null) return NotFound();

            _context.Fournisseurs.Remove(fournisseur);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}