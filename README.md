# DIO - Introdução a criação de API com Entity Framework e ASP.NET C#

## Autor
- [Felipe Figueiredo Bezerra](https://github.com/FigFelipe)

## Objetivo
Criar um projeto WebApplicationNETCore do tipo API, utilizar os recursos disponíveis do EntityFramework para a integração com o banco de dados (Microsoft SQL Express) para realizações de operações CRUD.

## Etapas

1. Criar um projeto WebApplicationNETCore
2. Instalar (via NuGet) as dependências necessárias do EntityFramework:

  | Nome do Pacote                         |
  |----------------------------------------|
  | EntityFramework                        |
  | Microsoft.EntityFrameworkCore          |
  | Microsoft.EntityFrameworkCore.Design   |
  | Microsoft.EntityFrameworkCore.SqlServer|

3. No arquivo 'appsettings.json', adicionar a seguinte connection string:

  > **Connection String:**  "ConnectionStrings": {
     "ConexaoPadrao": "Server=localhost\\sqlexpress; Initial Catalog=Agenda; Integrated Security=True"

4. Realizar as 'Migrations'

  > **Connection String:**  "ConnectionStrings": {
     "ConexaoPadrao": "Server=localhost\\sqlexpress; Initial Catalog=Agenda; Integrated Security=True"

## Ambiente de Desenvolvimento

 - **IDE**: Visual Studio 22 (Community Edition)
 - **SDK:** .NET Core
 - **BD:** Microsoft SQL Express

## Capitulos

| Nome                                |
|-------------------------------------|
| Introdução                          |
| Entendendo o CRUD                   |
| Instalando pacotes                  |
| Criando a classe entidade           |
| Criando o Contexto                  |
| Configurando a conexão              |
| Entendendo as migrations            |
| Criando a controller e o endpoint de Create |
| Criando o endpoint obter por ID     |
| Criando o endpoint de update        |
| Criando o endpoint de delete        |
| Criando o endpoint de obter por nome|
| Entendendo os verbos HTTP           |
| Recapitulando a construção da API   |
| Alterando o endpoint create         |
