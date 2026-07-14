using StockManagerPro.API.DTOs;

namespace StockManagerPro.API.Services;

public interface IFournisseurService
{
    Task<IEnumerable<FournisseurDto>> GetAllAsync();
    Task<FournisseurDto?> GetByIdAsync(int id);
    Task<IEnumerable<ProduitDto>> GetProduitsAsync(int id);
    Task<FournisseurDto> CreateAsync(FournisseurCreateDto dto);
    Task<bool> UpdateAsync(int id, FournisseurCreateDto dto);
    Task<bool> DeleteAsync(int id);
}