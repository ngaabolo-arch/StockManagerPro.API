using StockManagerPro.API.DTOs;

namespace StockManagerPro.API.Services;

public interface IAuthService
{
    Task<string> RegisterAsync(RegisterDto dto);
    Task<AuthResponseDto> LoginAsync(LoginDto dto);
}