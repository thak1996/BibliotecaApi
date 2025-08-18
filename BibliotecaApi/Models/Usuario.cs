using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string NomeCompleto { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string UltimoNome { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string RU { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Curso { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string SenhaHash { get; set; } = string.Empty;

        public DateTime DataCriacao { get; set; } = DateTime.Now;

        public bool Ativo { get; set; } = true;
    }
}