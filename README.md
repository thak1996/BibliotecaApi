# BibliotecaApi

API REST para gerenciamento de biblioteca, construída com .NET 8, arquitetura em camadas e autenticação JWT.

## Visão Geral

Permite cadastro, consulta e autenticação de usuários e livros. Utiliza Entity Framework Core para persistência e segue boas práticas de separação de responsabilidades.

## Estrutura do Projeto

- `Controllers/`: Endpoints HTTP (ex: `AuthController.cs`, `LivrosController.cs`)
- `Models/`: Entidades do domínio (`Livro.cs`, `Usuario.cs`)
- `DTOs/`: Objetos de transferência de dados
- `Services/`: Lógica de negócio e interfaces
- `Data/`: Contexto EF Core e seed de dados
- `Properties/launchSettings.json`: Perfis de execução/debug
- `appsettings*.json`: Configurações

## Execução

### Requisitos

- .NET 8 SDK
- Docker

### Executando localmente

```pwsh
dotnet build
dotnet run --project BibliotecaApi/BibliotecaApi.csproj
```

A API estará disponível em `http://localhost:5000` (ou porta definida em `launchSettings.json`).

### Executando com Docker

```pwsh
docker-compose up -d
```

O serviço será iniciado em segundo plano. Para parar:

```pwsh
docker-compose down
```

## Exemplos de Uso

Autenticação:

```http
POST /api/auth/login
{
	"email": "usuario@exemplo.com",
	"senha": "123456"
}
```

Cadastro de livro:

```http
POST /api/livros
{
	"titulo": "Dom Casmurro",
	"autor": "Machado de Assis"
}
```

## Dependências Principais

- BCrypt.Net-Next (hash de senha)
- Microsoft.AspNetCore.Authentication.JwtBearer (JWT)
- Entity Framework Core

## Observações

Para dúvidas sobre padrões ou arquitetura, consulte `.github/copilot-instructions.md`.
