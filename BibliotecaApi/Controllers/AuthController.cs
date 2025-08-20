using Microsoft.AspNetCore.Mvc;
using BibliotecaAPI.DTOs;
using BibliotecaAPI.Services;

namespace BibliotecaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        /// <summary>
        /// Realizar login
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDto loginDto)
        {
            try
            {
                var token = await _authService.Login(loginDto);
                return Ok(new { token, message = "Login realizado com sucesso" });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", details = ex.Message });
            }
        }

        /// <summary>
        /// Registrar novo usuário
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UsuarioCreateDto createDto)
        {
            try
            {
                var usuario = await _authService.Register(createDto);
                return CreatedAtAction(nameof(Register), new { id = usuario.Id }, new
                {
                    id = usuario.Id,
                    nomeCompleto = usuario.NomeCompleto,
                    ultimoNome = usuario.UltimoNome,
                    ru = usuario.RU,
                    curso = usuario.Curso,
                    dataCriacao = usuario.DataCriacao,
                    ativo = usuario.Ativo,
                    message = "Usuário registrado com sucesso"
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", details = ex.Message });
            }
        }
    }
}