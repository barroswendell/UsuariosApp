

🚀 UsuariosApp

API desenvolvida utilizando .NET, seguindo a abordagem DDD (Domain-Driven Design) e implementando o Entity Framework para a persistência de dados.

📝 Funcionalidades
	•	📌 Cadastro e gerenciamento de usuários
	•	📌 Autenticação e autorização via JWT
	•	📌 Gerenciamento de entidades do sistema
	•	📌 Integração com banco de dados SQL Server
	•	📌 Padrão Repository e Unit of Work
	•	📌 Testes automatizados com xUnit

🛠 Tecnologias utilizadas
	•	C# - Linguagem principal
	•	.NET Core / .NET - Framework para desenvolvimento
	•	Entity Framework Core - ORM para manipulação do banco de dados
	•	SQL Server - Banco de dados relacional
	•	JWT - Autenticação e segurança
	•	xUnit - Testes automatizados

📦 Estrutura do Projeto (DDD)

📂 NomeDoProjeto.API        -> Camada de apresentação (Controllers, Middlewares)  
📂 NomeDoProjeto.Application -> Regras de negócio (Services, DTOs, Validações)  
📂 NomeDoProjeto.Domain      -> Modelos de domínio (Entities, Interfaces, Eventos)  
📂 NomeDoProjeto.Infrastructure -> Persistência de dados (Repositories, Contexto EF)  
📂 NomeDoProjeto.Tests       -> Testes automatizados com xUnit  

