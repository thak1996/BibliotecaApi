using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.Models
{
    public class Livro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Titulo { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Autor { get; set; } = string.Empty;

        [StringLength(50)]
        public string ISBN { get; set; } = string.Empty;

        public int AnoPublicacao { get; set; }

        [StringLength(100)]
        public string Editora { get; set; } = string.Empty;

        public int QuantidadeDisponivel { get; set; }

        public DateTime DataCadastro { get; set; } = DateTime.Now;

        public bool Ativo { get; set; } = true;
    }
}