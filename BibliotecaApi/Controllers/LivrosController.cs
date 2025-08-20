using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BibliotecaAPI.Data;
using BibliotecaAPI.DTOs;
using BibliotecaAPI.Models;

namespace BibliotecaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LivrosController(ApplicationDbContext context) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;

        /// <summary>
        /// Listar todos os livros
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LivroResponseDto>>> GetLivros()
        {
            var livros = await _context.Livros
                .Where(l => l.Ativo)
                .Select(l => new LivroResponseDto
                {
                    Id = l.Id,
                    Titulo = l.Titulo,
                    Autor = l.Autor,
                    ISBN = l.ISBN,
                    AnoPublicacao = l.AnoPublicacao,
                    Editora = l.Editora,
                    QuantidadeDisponivel = l.QuantidadeDisponivel,
                    DataCadastro = l.DataCadastro,
                    Ativo = l.Ativo
                })
                .ToListAsync();

            return Ok(livros);
        }

        /// <summary>
        /// Buscar livro por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<LivroResponseDto>> GetLivro(int id)
        {
            var livro = await _context.Livros
                .Where(l => l.Id == id && l.Ativo)
                .Select(l => new LivroResponseDto
                {
                    Id = l.Id,
                    Titulo = l.Titulo,
                    Autor = l.Autor,
                    ISBN = l.ISBN,
                    AnoPublicacao = l.AnoPublicacao,
                    Editora = l.Editora,
                    QuantidadeDisponivel = l.QuantidadeDisponivel,
                    DataCadastro = l.DataCadastro,
                    Ativo = l.Ativo
                })
                .FirstOrDefaultAsync();

            if (livro == null)
            {
                return NotFound(new { Message = "Livro não encontrado" });
            }

            return Ok(livro);
        }

        /// <summary>
        /// Criar novo livro
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<LivroResponseDto>> PostLivro(LivroCreateDto livroDto)
        {
            if (!string.IsNullOrWhiteSpace(livroDto.ISBN))
            {
                var isbnLower = livroDto.ISBN.ToLower();
                var livroExistente = await _context.Livros.FirstOrDefaultAsync(l => l.ISBN.ToLower() == isbnLower);
                if (livroExistente != null)
                {
                    return Conflict(new { Message = "Já existe um livro cadastrado com este ISBN." });
                }
            }

            var livro = new Livro
            {
                Titulo = livroDto.Titulo,
                Autor = livroDto.Autor,
                ISBN = livroDto.ISBN,
                AnoPublicacao = livroDto.AnoPublicacao,
                Editora = livroDto.Editora,
                QuantidadeDisponivel = livroDto.QuantidadeDisponivel
            };

            _context.Livros.Add(livro);
            await _context.SaveChangesAsync();

            var response = new LivroResponseDto
            {
                Id = livro.Id,
                Titulo = livro.Titulo,
                Autor = livro.Autor,
                ISBN = livro.ISBN,
                AnoPublicacao = livro.AnoPublicacao,
                Editora = livro.Editora,
                QuantidadeDisponivel = livro.QuantidadeDisponivel,
                DataCadastro = livro.DataCadastro,
                Ativo = livro.Ativo
            };

            return CreatedAtAction(nameof(GetLivro), new { id = livro.Id }, response);
        }

        /// <summary>
        /// Atualizar livro
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLivro(int id, LivroUpdateDto livroDto)
        {
            var livro = await _context.Livros.FindAsync(id);

            if (livro == null || !livro.Ativo)
            {
                return NotFound(new { Message = "Livro não encontrado" });
            }

            AtualizarPropriedadesLivro(livro, livroDto);

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { Message = "Livro atualizado com sucesso" });
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, new { Message = "Erro ao atualizar livro" });
            }
        }

        private static void AtualizarPropriedadesLivro(Livro livro, LivroUpdateDto livroDto)
        {
            if (!string.IsNullOrEmpty(livroDto.Titulo)) livro.Titulo = livroDto.Titulo;
            if (!string.IsNullOrEmpty(livroDto.Autor)) livro.Autor = livroDto.Autor;
            if (!string.IsNullOrEmpty(livroDto.ISBN)) livro.ISBN = livroDto.ISBN;
            if (livroDto.AnoPublicacao.HasValue) livro.AnoPublicacao = livroDto.AnoPublicacao.Value;
            if (!string.IsNullOrEmpty(livroDto.Editora)) livro.Editora = livroDto.Editora;
            if (livroDto.QuantidadeDisponivel.HasValue) livro.QuantidadeDisponivel = livroDto.QuantidadeDisponivel.Value;
        }

        /// <summary>
        /// Excluir livro (soft delete)
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLivro(int id)
        {
            var livro = await _context.Livros.FindAsync(id);

            if (livro == null)
            {
                return NotFound(new { Message = "Livro não encontrado" });
            }

            livro.Ativo = false;
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Livro excluído com sucesso" });
        }
    }
}