using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockManagerPro.API.Data;
using StockManagerPro.API.DTOs;
using StockManagerPro.API.Models;

namespace StockManagerPro.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MouvementsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MouvementsController(AppDbContext context)
        {
            _context = context;
        }

        // GET api/mouvements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MouvementDto>>> GetAll()
        {
            var mouvements = await _context.MouvementsStock
                .Include(m => m.Produit)
                .OrderByDescending(m => m.DateMouvement)
                .ToListAsync();

            return Ok(mouvements.Select(m => new MouvementDto
            {
                Id = m.Id,
                Type = m.Type,
                Quantite = m.Quantite,
                Motif = m.Motif,
                DateMouvement = m.DateMouvement,
                ProduitNom = m.Produit.Nom
            }));
        }

        // GET api/mouvements/produit/5
        [HttpGet("produit/{id}")]
        public async Task<ActionResult<IEnumerable<MouvementDto>>> GetByProduit(int id)
        {
            var produit = await _context.Produits.FindAsync(id);
            if (produit == null) return NotFound();

            var mouvements = await _context.MouvementsStock
                .Include(m => m.Produit)
                .Where(m => m.ProduitId == id)
                .OrderByDescending(m => m.DateMouvement)
                .ToListAsync();

            return Ok(mouvements.Select(m => new MouvementDto
            {
                Id = m.Id,
                Type = m.Type,
                Quantite = m.Quantite,
                Motif = m.Motif,
                DateMouvement = m.DateMouvement,
                ProduitNom = m.Produit.Nom
            }));
        }

        // POST api/mouvements/entree
        [HttpPost("entree")]
        public async Task<ActionResult<MouvementDto>> Entree(MouvementCreateDto dto)
        {
            var produit = await _context.Produits.FindAsync(dto.ProduitId);
            if (produit == null) return NotFound("Produit introuvable");

            // Mise à jour du stock
            produit.QuantiteEnStock += dto.Quantite;
            produit.DateModification = DateTime.Now;

            var mouvement = new MouvementStock
            {
                ProduitId = dto.ProduitId,
                Type = "ENTREE",
                Quantite = dto.Quantite,
                Motif = dto.Motif,
                DateMouvement = DateTime.Now
            };

            _context.MouvementsStock.Add(mouvement);
            await _context.SaveChangesAsync();

            return Ok(new MouvementDto
            {
                Id = mouvement.Id,
                Type = mouvement.Type,
                Quantite = mouvement.Quantite,
                Motif = mouvement.Motif,
                DateMouvement = mouvement.DateMouvement,
                ProduitNom = produit.Nom
            });
        }

        // POST api/mouvements/sortie
        [HttpPost("sortie")]
        public async Task<ActionResult<MouvementDto>> Sortie(MouvementCreateDto dto)
        {
            var produit = await _context.Produits.FindAsync(dto.ProduitId);
            if (produit == null) return NotFound("Produit introuvable");

            // Vérification stock suffisant
            if (produit.QuantiteEnStock < dto.Quantite)
                return BadRequest($"Stock insuffisant. Stock actuel : {produit.QuantiteEnStock}");

            // Mise à jour du stock
            produit.QuantiteEnStock -= dto.Quantite;
            produit.DateModification = DateTime.Now;

            var mouvement = new MouvementStock
            {
                ProduitId = dto.ProduitId,
                Type = "SORTIE",
                Quantite = dto.Quantite,
                Motif = dto.Motif,
                DateMouvement = DateTime.Now
            };

            _context.MouvementsStock.Add(mouvement);
            await _context.SaveChangesAsync();

            return Ok(new MouvementDto
            {
                Id = mouvement.Id,
                Type = mouvement.Type,
                Quantite = mouvement.Quantite,
                Motif = mouvement.Motif,
                DateMouvement = mouvement.DateMouvement,
                ProduitNom = produit.Nom
            });
        }
    }
}