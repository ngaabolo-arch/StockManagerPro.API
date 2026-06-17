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
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET api/categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategorieDto>>> GetAll()
        {
            var categories = await _context.Categories.ToListAsync();
            return Ok(categories.Select(c => new CategorieDto
            {
                Id = c.Id,
                Nom = c.Nom,
                Description = c.Description
            }));
        }

        // GET api/categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategorieDto>> GetById(int id)
        {
            var categorie = await _context.Categories.FindAsync(id);
            if (categorie == null) return NotFound();

            return Ok(new CategorieDto
            {
                Id = categorie.Id,
                Nom = categorie.Nom,
                Description = categorie.Description
            });
        }

        // POST api/categories
        [HttpPost]
        public async Task<ActionResult<CategorieDto>> Create(CategorieCreateDto dto)
        {
            var categorie = new Categorie
            {
                Nom = dto.Nom,
                Description = dto.Description
            };

            _context.Categories.Add(categorie);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = categorie.Id }, new CategorieDto
            {
                Id = categorie.Id,
                Nom = categorie.Nom,
                Description = categorie.Description
            });
        }

        // PUT api/categories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CategorieCreateDto dto)
        {
            var categorie = await _context.Categories.FindAsync(id);
            if (categorie == null) return NotFound();

            categorie.Nom = dto.Nom;
            categorie.Description = dto.Description;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var categorie = await _context.Categories.FindAsync(id);
            if (categorie == null) return NotFound();

            _context.Categories.Remove(categorie);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}