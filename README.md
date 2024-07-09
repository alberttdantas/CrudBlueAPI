# CrudBlue API

Este projeto é uma API RESTful para gerenciamento de contatos, utilizando ASP.NET Core, Entity Framework, MediatR com CQRS, AutoMapper e SQL Server.

## Pré-requisitos

Antes de começar, certifique-se de ter instalado:
- .NET 8.0 SDK
- Docker

## Configuração Inicial

Clone o repositório:
	`git clone https://seu-repositorio/crudblue-api.git`


Restaure os pacotes NuGet:
	`dotnet restore`


Construa a aplicação:
	`dotnet build`

## Executando a Aplicação

Execute a aplicação localmente:
	`dotnet run`

## Docker
Para construir e executar a aplicação usando Docker, siga estes passos:

Construa a imagem Docker:
	`docker build -t seu-usuario/crudblue-api:v1 .`

Execute o container: 
	`docker run -d -p 7070:8000 seu-usuario/crudblue-api:v1`

gora você pode acessar a API através de `http://localhost:7070`.

## Documentação da API

Após iniciar a aplicação, você pode acessar a documentação da API e testar os endpoints através do Swagger em `http://localhost:7070/swagger/index.html`.

## Configuração do Banco de Dados

Para conectar-se ao banco de dados local quando estiver executando a aplicação fora do Docker, certifique-se de que a string de conexão no arquivo `appsettings.json` esteja correta para o seu ambiente de banco de dados local. A string de conexão padrão é:

```json
"ConnectionStrings": {
  "AgendaDbConnection": "Data source=DESKTOP-19RHI31\\SQLSERVER2024;database=BlueContactsDB;Trusted_connection=true; Encrypt=false; TrustServerCertificate=true"
}

```

## Acesso ao SQL Server com Docker

Quando estiver executando a aplicação com Docker, certifique-se de que a string de conexão no arquivo `appsettings.json` esteja configurada para se conectar ao SQL Server no Docker. A string de conexão deve ser semelhante a esta:

```json
"ConnectionStrings": {
  "AgendaDbConnection": "Server=sqlserver;Database=BlueContactsDB;User Id=sa;Password=SuaSenhaForte!;"
}

```
Nota para usuários do Docker:

Se você estiver executando a aplicação com Docker e precisar se conectar ao SQL Server também em execução no Docker, use o nome do container ou serviço do SQL Server na string de conexão.
Se você estiver tentando se conectar ao SQL Server na sua máquina host a partir de um container Docker, use `host.docker.internal` para o nome do servidor na string de conexão.
Lembre-se de que, ao alternar entre executar a aplicação localmente e no Docker, você precisará garantir que a string de conexão no `appsettings.json` esteja correta para o ambiente em que a aplicação está sendo executada.
