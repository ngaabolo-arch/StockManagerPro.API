using StockManagerPro.API.DTOs;

namespace StockManagerPro.API.Services;

public interface IMouvementService
{
    Task<IEnumerable<MouvementDto>> GetAllAsync();
    Task<IEnumerable<MouvementDto>> GetByProduitAsync(int produitId);
    Task<MouvementDto> EntreeAsync(MouvementCreateDto dto);
    Task<MouvementDto> SortieAsync(MouvementCreateDto dto);
}