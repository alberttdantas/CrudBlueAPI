# CrudBlue API

Este projeto � uma API RESTful para gerenciamento de contatos, utilizando ASP.NET Core, Entity Framework, MediatR com CQRS, AutoMapper e SQL Server.

## Pr�-requisitos

Antes de come�ar, certifique-se de ter instalado:
- .NET 8.0 SDK
- Docker

## Configura��o Inicial

Clone o reposit�rio:
	`git clone https://seu-repositorio/crudblue-api.git`


Restaure os pacotes NuGet:
	`dotnet restore`


Construa a aplica��o:
	`dotnet build`

## Executando a Aplica��o

Execute a aplica��o localmente:
	`dotnet run`

## Docker
Para construir e executar a aplica��o usando Docker, siga estes passos:

Construa a imagem Docker:
	`docker build -t seu-usuario/crudblue-api:v1 .`

Execute o container: 
	`docker run -d -p 7070:8000 seu-usuario/crudblue-api:v1`

gora voc� pode acessar a API atrav�s de `http://localhost:7070`.

## Documenta��o da API

Ap�s iniciar a aplica��o, voc� pode acessar a documenta��o da API e testar os endpoints atrav�s do Swagger em `http://localhost:7070/swagger/index.html`.
