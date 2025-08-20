using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using BibliotecaAPI.Data;
using BibliotecaAPI.DTOs;
using BibliotecaAPI.Models;

namespace BibliotecaAPI.Services
{
    public class AuthService(ApplicationDbContext context, IConfiguration configuration) : IAuthService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IConfiguration _configuration = configuration;

        public async Task<string> Login(UsuarioLoginDto loginDto)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.UltimoNome.ToLower() == loginDto.UltimoNome.ToLower() && u.Ativo);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(loginDto.Senha, usuario.SenhaHash))
            {
                throw new UnauthorizedAccessException("Credenciais inválidas");
            }

            return GenerateJwtToken(usuario.Id, usuario.UltimoNome);
        }

        public async Task<UsuarioResponseDto> Register(UsuarioCreateDto createDto)
        {
            var ultimoNome = ExtrairUltimoNome(createDto.NomeCompleto);
            var existingUser = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.UltimoNome.ToLower() == ultimoNome.ToLower());

            if (existingUser != null)
            {
                throw new InvalidOperationException("Último nome já está em uso");
            }

            var existingRU = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.RU == createDto.RU);

            if (existingRU != null)
            {
                throw new InvalidOperationException("RU já está em uso");
            }

            var usuario = new Usuario
            {
                NomeCompleto = createDto.NomeCompleto,
                UltimoNome = ultimoNome,
                RU = createDto.RU,
                Curso = createDto.Curso,
                SenhaHash = BCrypt.Net.BCrypt.HashPassword(createDto.Senha)
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return new UsuarioResponseDto
            {
                Id = usuario.Id,
                NomeCompleto = usuario.NomeCompleto,
                UltimoNome = usuario.UltimoNome,
                RU = usuario.RU,
                Curso = usuario.Curso,
                DataCriacao = usuario.DataCriacao,
                Ativo = usuario.Ativo
            };
        }

        public string GenerateJwtToken(int userId, string ultimoNome)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key não configurado"));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                    new Claim(ClaimTypes.Name, ultimoNome)
                ]),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private static string ExtrairUltimoNome(string nomeCompleto)
        {
            if (string.IsNullOrWhiteSpace(nomeCompleto))
                return string.Empty;

            var nomes = nomeCompleto.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return nomes.Length > 0 ? nomes[^1] : string.Empty;
        }
    }
}