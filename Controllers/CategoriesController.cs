using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockManagerPro.API.DTOs;
using StockManagerPro.API.Services;

namespace StockManagerPro.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategorieService _categorieService;

    public CategoriesController(ICategorieService categorieService)
    {
        _categorieService = categorieService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _categorieService.GetAllAsync();
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var categorie = await _categorieService.GetByIdAsync(id);
        if (categorie == null) return NotFound();
        return Ok(categorie);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CategorieCreateDto dto)
    {
        var categorie = await _categorieService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = categorie.Id }, categorie);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CategorieCreateDto dto)
    {
        var result = await _categorieService.UpdateAsync(id, dto);
        if (!result) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _categorieService.DeleteAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }
}