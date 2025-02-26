using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TaskManager.Data;
using TaskManager.DTOs;
using TaskManager.Helpers;
using TaskManager.Models;

namespace TaskManager.Services;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _context;
    private readonly JwtSettings _jwtSettings;

    public AuthService(ApplicationDbContext context, JwtSettings jwtSettings)
    {
        _context = context;
        _jwtSettings = jwtSettings;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
    {
        // Kullanıcı adı veya email zaten kullanılıyor mu kontrol et
        if (await _context.Users.AnyAsync(u => u.Username == registerDto.Username || u.Email == registerDto.Email))
        {
            throw new InvalidOperationException("Kullanıcı adı veya email zaten kullanımda.");
        }

        // Şifreyi hashle
        var passwordHash = HashPassword(registerDto.Password);

        // Yeni kullanıcı oluştur
        var user = new User
        {
            Username = registerDto.Username,
            Email = registerDto.Email,
            PasswordHash = passwordHash
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Token oluştur ve döndür
        var token = GenerateJwtToken(user);
        return new AuthResponseDto
        {
            Token = token,
            Username = user.Username
        };
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
    {
        // Kullanıcıyı bul
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == loginDto.Username);
        if (user == null)
        {
            throw new InvalidOperationException("Kullanıcı adı veya şifre hatalı.");
        }

        // Şifreyi doğrula
        if (!VerifyPassword(loginDto.Password, user.PasswordHash))
        {
            throw new InvalidOperationException("Kullanıcı adı veya şifre hatalı.");
        }

        // Token oluştur ve döndür
        var token = GenerateJwtToken(user);
        return new AuthResponseDto
        {
            Token = token,
            Username = user.Username
        };
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashedBytes);
    }

    private bool VerifyPassword(string password, string hash)
    {
        return HashPassword(password) == hash;
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddMinutes(_jwtSettings.ExpirationInMinutes);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: expires,
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
} 