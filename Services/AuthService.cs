using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using StockManagerPro.API.Data;
using StockManagerPro.API.DTOs;
using StockManagerPro.API.Models;

namespace StockManagerPro.API.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthService(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<string> RegisterAsync(RegisterDto dto)
    {
        if (await _context.Utilisateurs.AnyAsync(u => u.Email == dto.Email))
            throw new InvalidOperationException("Un compte avec cet email existe déjà");

        var utilisateur = new Utilisateur
        {
            Nom = dto.Nom,
            Email = dto.Email,
            MotDePasseHash = BCrypt.Net.BCrypt.HashPassword(dto.MotDePasse)
        };

        _context.Utilisateurs.Add(utilisateur);
        await _context.SaveChangesAsync();

        return "Compte créé avec succès";
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
    {
        var utilisateur = await _context.Utilisateurs
            .FirstOrDefaultAsync(u => u.Email == dto.Email);

        if (utilisateur == null)
            throw new UnauthorizedAccessException("Email ou mot de passe incorrect");

        if (!BCrypt.Net.BCrypt.Verify(dto.MotDePasse, utilisateur.MotDePasseHash))
            throw new UnauthorizedAccessException("Email ou mot de passe incorrect");

        var token = GenererToken(utilisateur);

        return new AuthResponseDto
        {
            Token = token,
            Nom = utilisateur.Nom,
            Email = utilisateur.Email
        };
    }

    private string GenererToken(Utilisateur utilisateur)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings["SecretKey"]!;
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, utilisateur.Id.ToString()),
            new Claim(ClaimTypes.Email, utilisateur.Email),
            new Claim(ClaimTypes.Name, utilisateur.Nom)
        };

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(24),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}