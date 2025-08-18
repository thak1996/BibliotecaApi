using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.DTOs
{
    public class UsuarioCreateDto
    {
        [Required(ErrorMessage = "Nome completo é obrigatório")]
        [StringLength(100, ErrorMessage = "Nome completo deve ter no máximo 100 caracteres")]
        public string NomeCompleto { get; set; } = string.Empty;

        [Required(ErrorMessage = "RU é obrigatório")]
        [StringLength(20, ErrorMessage = "RU deve ter no máximo 20 caracteres")]
        public string RU { get; set; } = string.Empty;

        [Required(ErrorMessage = "Curso é obrigatório")]
        [StringLength(100, ErrorMessage = "Curso deve ter no máximo 100 caracteres")]
        public string Curso { get; set; } = string.Empty;

        [Required(ErrorMessage = "Senha é obrigatória")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Senha deve ter entre 6 e 50 caracteres")]
        public string Senha { get; set; } = string.Empty;
    }

    public class UsuarioLoginDto
    {
        [Required(ErrorMessage = "Último nome é obrigatório")]
        public string UltimoNome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Senha é obrigatória")]
        public string Senha { get; set; } = string.Empty;
    }

    public class UsuarioResponseDto
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; } = string.Empty;
        public string UltimoNome { get; set; } = string.Empty;
        public string RU { get; set; } = string.Empty;
        public string Curso { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; }
        public bool Ativo { get; set; }
    }
}
