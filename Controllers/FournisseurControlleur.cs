using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockManagerPro.API.DTOs;
using StockManagerPro.API.Services;

namespace StockManagerPro.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class FournisseursController : ControllerBase
{
    private readonly IFournisseurService _fournisseurService;

    public FournisseursController(IFournisseurService fournisseurService)
    {
        _fournisseurService = fournisseurService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var fournisseurs = await _fournisseurService.GetAllAsync();
        return Ok(fournisseurs);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var fournisseur = await _fournisseurService.GetByIdAsync(id);
        if (fournisseur == null) return NotFound();
        return Ok(fournisseur);
    }

    [HttpGet("{id}/produits")]
    public async Task<IActionResult> GetProduits(int id)
    {
        var produits = await _fournisseurService.GetProduitsAsync(id);
        return Ok(produits);
    }

    [HttpPost]
    public async Task<IActionResult> Create(FournisseurCreateDto dto)
    {
        var fournisseur = await _fournisseurService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = fournisseur.Id }, fournisseur);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, FournisseurCreateDto dto)
    {
        var result = await _fournisseurService.UpdateAsync(id, dto);
        if (!result) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _fournisseurService.DeleteAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }
}