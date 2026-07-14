using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockManagerPro.API.DTOs;
using StockManagerPro.API.Services;

namespace StockManagerPro.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class MouvementsController : ControllerBase
{
    private readonly IMouvementService _mouvementService;

    public MouvementsController(IMouvementService mouvementService)
    {
        _mouvementService = mouvementService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var mouvements = await _mouvementService.GetAllAsync();
        return Ok(mouvements);
    }

    [HttpGet("produit/{produitId}")]
    public async Task<IActionResult> GetByProduit(int produitId)
    {
        var mouvements = await _mouvementService.GetByProduitAsync(produitId);
        return Ok(mouvements);
    }

    [HttpPost("entree")]
    public async Task<IActionResult> Entree(MouvementCreateDto dto)
    {
        try
        {
            var mouvement = await _mouvementService.EntreeAsync(dto);
            return Ok(mouvement);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("sortie")]
    public async Task<IActionResult> Sortie(MouvementCreateDto dto)
    {
        try
        {
            var mouvement = await _mouvementService.SortieAsync(dto);
            return Ok(mouvement);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}