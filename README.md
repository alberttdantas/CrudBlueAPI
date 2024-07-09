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
