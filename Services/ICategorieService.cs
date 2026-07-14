using StockManagerPro.API.DTOs;

namespace StockManagerPro.API.Services;

public interface ICategorieService
{
    Task<IEnumerable<CategorieDto>> GetAllAsync();
    Task<CategorieDto?> GetByIdAsync(int id);
    Task<CategorieDto> CreateAsync(CategorieCreateDto dto);
    Task<bool> UpdateAsync(int id, CategorieCreateDto dto);
    Task<bool> DeleteAsync(int id);
}