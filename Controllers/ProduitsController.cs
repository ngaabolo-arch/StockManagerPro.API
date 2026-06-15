using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockManagerPro.API.Data;
using StockManagerPro.API.DTOs;
using StockManagerPro.API.Models;

namespace StockManagerPro.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProduitsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProduitsController(AppDbContext context)
        {
            _context = context;
        }

        // GET api/produits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProduitDto>>> GetAll()
        {
            var produits = await _context.Produits
                .Include(p => p.Categorie)
                .Include(p => p.Fournisseur)
                .ToListAsync();

            return Ok(produits.Select(p => new ProduitDto
            {
                Id = p.Id,
                Nom = p.Nom,
                Description = p.Description,
                Prix = p.Prix,
                QuantiteEnStock = p.QuantiteEnStock,
                SeuilAlerte = p.SeuilAlerte,
                CategorieNom = p.Categorie.Nom,
                FournisseurNom = p.Fournisseur.Nom
            }));
        }

        // GET api/produits/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProduitDto>> GetById(int id)
        {
            var produit = await _context.Produits
                .Include(p => p.Categorie)
                .Include(p => p.Fournisseur)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (produit == null) return NotFound();

            return Ok(new ProduitDto
            {
                Id = produit.Id,
                Nom = produit.Nom,
                Description = produit.Description,
                Prix = produit.Prix,
                QuantiteEnStock = produit.QuantiteEnStock,
                SeuilAlerte = produit.SeuilAlerte,
                CategorieNom = produit.Categorie.Nom,
                FournisseurNom = produit.Fournisseur.Nom
            });
        }

        // GET api/produits/alertes
        [HttpGet("alertes")]
        public async Task<ActionResult<IEnumerable<ProduitDto>>> GetAlertes()
        {
            var produits = await _context.Produits
                .Include(p => p.Categorie)
                .Include(p => p.Fournisseur)
                .Where(p => p.QuantiteEnStock <= p.SeuilAlerte)
                .ToListAsync();

            return Ok(produits.Select(p => new ProduitDto
            {
                Id = p.Id,
                Nom = p.Nom,
                Description = p.Description,
                Prix = p.Prix,
                QuantiteEnStock = p.QuantiteEnStock,
                SeuilAlerte = p.SeuilAlerte,
                CategorieNom = p.Categorie.Nom,
                FournisseurNom = p.Fournisseur.Nom
            }));
        }

        // POST api/produits
        [HttpPost]
        public async Task<ActionResult<ProduitDto>> Create(ProduitCreateDto dto)
        {
            var categorie = await _context.Categories.FindAsync(dto.CategorieId);
            if (categorie == null) return BadRequest("Catégorie introuvable");

            var fournisseur = await _context.Fournisseurs.FindAsync(dto.FournisseurId);
            if (fournisseur == null) return BadRequest("Fournisseur introuvable");

            var produit = new Produit
            {
                Nom = dto.Nom,
                Description = dto.Description,
                Prix = dto.Prix,
                QuantiteEnStock = dto.QuantiteEnStock,
                SeuilAlerte = dto.SeuilAlerte,
                CategorieId = dto.CategorieId,
                FournisseurId = dto.FournisseurId
            };

            _context.Produits.Add(produit);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = produit.Id }, new ProduitDto
            {
                Id = produit.Id,
                Nom = produit.Nom,
                Description = produit.Description,
                Prix = produit.Prix,
                QuantiteEnStock = produit.QuantiteEnStock,
                SeuilAlerte = produit.SeuilAlerte,
                CategorieNom = categorie.Nom,
                FournisseurNom = fournisseur.Nom
            });
        }

        // PUT api/produits/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProduitCreateDto dto)
        {
            var produit = await _context.Produits.FindAsync(id);
            if (produit == null) return NotFound();

            produit.Nom = dto.Nom;
            produit.Description = dto.Description;
            produit.Prix = dto.Prix;
            produit.QuantiteEnStock = dto.QuantiteEnStock;
            produit.SeuilAlerte = dto.SeuilAlerte;
            produit.CategorieId = dto.CategorieId;
            produit.FournisseurId = dto.FournisseurId;
            produit.DateModification = DateTime.Now;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/produits/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var produit = await _context.Produits.FindAsync(id);
            if (produit == null) return NotFound();

            _context.Produits.Remove(produit);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}