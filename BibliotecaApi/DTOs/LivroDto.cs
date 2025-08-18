using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.DTOs
{
    public class LivroCreateDto
    {
        [Required(ErrorMessage = "Título é obrigatório")]
        [StringLength(200, ErrorMessage = "Título deve ter no máximo 200 caracteres")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Autor é obrigatório")]
        [StringLength(100, ErrorMessage = "Autor deve ter no máximo 100 caracteres")]
        public string Autor { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "ISBN deve ter no máximo 50 caracteres")]
        public string ISBN { get; set; } = string.Empty;

        [Range(1000, 2100, ErrorMessage = "Ano de publicação deve estar entre 1000 e 2100")]
        public int AnoPublicacao { get; set; }

        [StringLength(100, ErrorMessage = "Editora deve ter no máximo 100 caracteres")]
        public string Editora { get; set; } = string.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "Quantidade disponível deve ser maior ou igual a 0")]
        public int QuantidadeDisponivel { get; set; }
    }

    public class LivroUpdateDto
    {
        [StringLength(200, ErrorMessage = "Título deve ter no máximo 200 caracteres")]
        public string? Titulo { get; set; }

        [StringLength(100, ErrorMessage = "Autor deve ter no máximo 100 caracteres")]
        public string? Autor { get; set; }

        [StringLength(50, ErrorMessage = "ISBN deve ter no máximo 50 caracteres")]
        public string? ISBN { get; set; }

        [Range(1000, 2100, ErrorMessage = "Ano de publicação deve estar entre 1000 e 2100")]
        public int? AnoPublicacao { get; set; }

        [StringLength(100, ErrorMessage = "Editora deve ter no máximo 100 caracteres")]
        public string? Editora { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantidade disponível deve ser maior ou igual a 0")]
        public int? QuantidadeDisponivel { get; set; }
    }

    public class LivroResponseDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Autor { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public int AnoPublicacao { get; set; }
        public string Editora { get; set; } = string.Empty;
        public int QuantidadeDisponivel { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }
    }
}