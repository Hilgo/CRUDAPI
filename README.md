# CRUDAPI

Ese é um repositório de um simples CRUD em c# utilizando padrão com Service e Swagger

## Conteúdo

- [Overview](#overview)
- [Tecnologias](#tecnologias)
- [Setup](#setup)
- [Testes](#testes)
- [Estrutura do Projeto](#estrutura-do-projeto)
- [Exemplos de Request](#exemplos-de-request)


## Overview

CRUDAPI tem um controller com incluir, alterar, deletar e listar produtos

## Tecnologias

- C#
- .NET 8
- Entity Framework Core
- Moq
- MSTest

## Setup

Para o setup do projeto localmente, siga os passos:

1. Clone o repositório: git clone https://github.com/your-username/CRUDAPI.git
cd CRUDAPI
2. Instale os pacotes necessários:
dotnet restore
3. Compile o projeto:
dotnet build

## Testes

Para rodar os testes utilize o comando:
dotnet test

## Estrutura do Projeto:

- `CRUDAPI/`: Contém a API Principal.
  - `Controllers/`: Contém o Controller da API.
  - `Data/`: Contém a conexão e configuração da base de dados.
  - `Models/`: Contém o modelo de dados (DTO).
  - `Services/`: Contém a camada de serviços da aplicação para implementação da lógica de negócio.
- `CRUDAPITests/`: Contém o projeto de testes da API.
  - `Services/`: Contémos testes do serviço da API.


##Exemplos de Request:

- request de listar todos os Produtos: /Produto - GET
- request de incluir Produto: /Produto - POST -
  {
  "name": "string",
  "price": 0,
  "stockQuantity": 0
}
- request de alterar Produto: Produto/{id} - PATCH -
  {
  "name": "string",
  "price": 0,
  "stockQuantity": 0
}
- request de deletar Produto: Produto/{id} - DELETE
- request de obter Produto por Id: Produto/{id} - GET
