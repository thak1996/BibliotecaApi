using BibliotecaAPI.DTOs;

namespace BibliotecaAPI.Services
{
    public interface IAuthService
    {
        Task<string> Login(UsuarioLoginDto loginDto);
        Task<UsuarioResponseDto> Register(UsuarioCreateDto createDto);
        string GenerateJwtToken(int userId, string ru);
    }
}