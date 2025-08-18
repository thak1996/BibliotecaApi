using BibliotecaAPI.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace BibliotecaAPI.Data
{
    public static class DatabaseSeeder
    {
        public static void SeedData(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
            if (!context.Usuarios.Any())
            {
                var usuario = new Usuario
                {
                    NomeCompleto = "Franklyn Viana dos Santos",
                    UltimoNome = ExtrairUltimoNome("Franklyn Viana dos Santos"),
                    RU = "4298019",
                    Curso = "Desenvolvimento Mobile",
                    SenhaHash = BCrypt.Net.BCrypt.HashPassword("4298019")
                };

                context.Usuarios.Add(usuario);
                context.SaveChanges();
                Console.WriteLine($"Usuário criado - Login: {usuario.UltimoNome}, Senha: {usuario.RU}");
            }

            if (!context.Livros.Any())
            {
                var livros = new List<Livro>
                {
                    new() {
                        Titulo = "Programação Back End II",
                        Autor = "LEDUR, Cleverson Lopes",
                        ISBN = "978-85-430-0001-1",
                        AnoPublicacao = 2019,
                        Editora = "SAGAH",
                        QuantidadeDisponivel = 5
                    },
                    new() {
                        Titulo = "Programação Back End III",
                        Autor = "FREITAS, Pedro H. Chagas [et al.]",
                        ISBN = "978-85-430-0002-2",
                        AnoPublicacao = 2021,
                        Editora = "SAGAH",
                        QuantidadeDisponivel = 3
                    },
                    new() {
                        Titulo = "Ajax, RICH Internet Applications e desenvolvimento Web para programadores",
                        Autor = "DEITEL, Paul J.",
                        ISBN = "978-85-7605-123-4",
                        AnoPublicacao = 2008,
                        Editora = "Pearson Prentice Hall",
                        QuantidadeDisponivel = 2
                    }
                };

                context.Livros.AddRange(livros);
                context.SaveChanges();
                Console.WriteLine($"{livros.Count} livros adicionados ao banco de dados.");
            }
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