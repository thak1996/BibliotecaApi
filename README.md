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

### Executando com Docker

```pwsh
docker-compose up -d
```

### Caso realize alterações no código

```pwsh
docker-compose up -d --build
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
	"email": "santos",
	"senha": "4298019"
}
```

Cadastro de livro:

```http
POST /api/livros
{
	"titulo": "Dom Casmurro",
	"autor": "Machado de Assis",
	"isbn": "978-85-7522-635-1",
	"anoPublicacao": 1899,
	"editora": "Editora Exemplo",
	"quantidadeDisponivel": 10,
	"dataCadastro": "2025-08-20T10:00:00",
	"ativo": true
}
```

## Dependências Principais

- BCrypt.Net-Next (hash de senha)
- Microsoft.AspNetCore.Authentication.JwtBearer (JWT)
- Entity Framework Core