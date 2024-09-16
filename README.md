# DIO - Introdução a criação de uma API Web com Entity Framework e ASP.NET Core

## Autor
- [Felipe Figueiredo Bezerra](https://github.com/FigFelipe)

## Objetivo
Criar um projeto API Web ASP.NET Core, utilizar os recursos disponíveis do EntityFramework para a integração com o banco de dados (Microsoft SQL Express) para realizações de operações CRUD.

## Ambiente de Desenvolvimento

 - **IDE**: Visual Studio 22 (Community Edition)
 - **SDK:** .NET Core
 - **Banco de Dados:** Microsoft SQL Express
 - **Framework:** Entity Framework

## Capítulos

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

## Etapas
1. No Visual Studio IDE, criar um projeto do tipo **API Web do ASP.NETCore**;
2. Instalar (via NuGet) as dependências necessárias do EntityFramework:

  | Nome do Pacote                         |
  |----------------------------------------|
  | EntityFramework                        |
  | Microsoft.EntityFrameworkCore          |
  | Microsoft.EntityFrameworkCore.Design   |
  | Microsoft.EntityFrameworkCore.SqlServer|

3. No arquivo 'appsettings.Development.json' (ambiente de testes), adicionar a seguinte connection string:
```
"ConnectionStrings": {
     "ConexaoPadrao": "Server=localhost\\sqlexpress; Initial Catalog=Agenda; Integrated Security=True"
}
```

4. Através do EntityFramework, criar um tabela 'Contatos' através do comando de 'migrations':
```
dotnet -ef migrations add CriacaoTabelaContato
```

> **Observação:**
Se o comando no terminal do Visual Studio não for reconhecido, então instalar o EntityFramework como ferramenta global. Utilizar o comando abaixo:
```
dotnet tool install --global dotnet-ef
```

5. Aplicar a 'migration' ao banco de dados (Microsoft SQL Express), utilizar o seguinte comando no terminal:
```   
dotnet ef database update
```
> **Observação:**
Caso ocorra erro na tentativa de conexão com o banco de dados, adicionar o parâmetro **'TrustServerCertificate=True'**:
```
"ConnectionStrings": {
    "ConexaoPadrao": "Server = localhost\\sqlexpress; Initial Catalog=Agenda; Integrated Security=True; TrustServerCertificate=True "
  }
```

## Banco de Dados - Propriedades da Conexão

| Propriedades da Conexão             | Valor |
|-------------------------------------|-------|
| Tipo de Servidor                    | Mecanismo de Banco de Dados |
| Nome do Servidor                    | localhost\SQLEXPRESS |
| Autenticação                        | Autenticação do Windows |
| Criptografia                        | Opcional |

## EntityFramework - CRUD

A tabela abaixo descreve os **Endpoints** desenvolvidos:

| Controller             |CRUD    |Verbo Http |Request URL   | Descrição |
|------------------------|--------|-----------|--------------|-----------|
| Contato                | CREATE | POST      |/contato      | Insere um novo contato na tabela 'Contatos' do banco de dados |
| Contato                | READ   | GET       |/contato/{id} | Obtém um contato existente através do parâmetro {id} da tabela 'Contatos' do banco de dados |
| Contato                | UPDATE | PUT       |/contato/{id} | Atualiza um contato existente através do parâmetro {id} da tabela 'Contatos' do banco de dados |
| Contato                | DELETE | DELETE    |/contato/{id} | Deleta um contato existente através do parâmetro {id} da tabela 'Contatos' do banco de dados |




