# DIO - Introdução a criação de uma API Web com Entity Framework e ASP.NET Core

## Autor
- [Felipe Figueiredo Bezerra](https://github.com/FigFelipe)

## Objetivo
Criar um projeto de **Agenda de Contatos** (API Web ASP.NET Core), utilizando os recursos disponíveis do EntityFramework para a integração com o banco de dados (Microsoft SQL Express) para realizações de operações CRUD.

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

### Criando o tipo de Projeto

1. No Visual Studio IDE, criar um projeto do tipo **API Web do ASP.NETCore**.

### Instalando os pacotes necessários
2. Instalar (via NuGet) as dependências necessárias do EntityFramework:

  | Nome do Pacote                         |
  |----------------------------------------|
  | EntityFramework                        |
  | Microsoft.EntityFrameworkCore          |
  | Microsoft.EntityFrameworkCore.Design   |
  | Microsoft.EntityFrameworkCore.SqlServer|

 
### Configurando a 'ConnectionString'
3. No arquivo 'appsettings.Development.json' (ambiente de testes), adicionar a seguinte connection string:
```
"ConnectionStrings": {
     "ConexaoPadrao": "Server=localhost\\sqlexpress; Initial Catalog=Agenda; Integrated Security=True"
}
```

| Propriedades da Conexão             | Valor |
|-------------------------------------|-------|
| Tipo de Servidor                    | Mecanismo de Banco de Dados |
| Nome do Servidor                    | localhost\SQLEXPRESS |
| Autenticação                        | Autenticação do Windows |
| Criptografia                        | Opcional |

### Criando uma Entidade 'Contato'
4. Criar uma pasta 'Entities' e adicionar a classe 'Contato'. A entidade é uma tabela do banco de dados.
```
namespace WebApplicationNETCore.Entities
{
    public class Contato
    {
        // Através do Migration, essa classe irá se transformar em uma tabela no banco de dados
        // É um schema (modelo)
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public bool Ativo { get; set; }
    }
}
```

### Criando um 'Context'
5. Criar uma pasta 'Context' e adicionar a classe 'AgendaContext'. É uma classe que acessa o banco de dados (deve ser herdada de 'DbContext').
```
using Microsoft.EntityFrameworkCore;
using WebApplicationNETCore.Entities;

namespace WebApplicationNETCore.Context
{
    // Herdar os métodos da classe 'DbContext'
    public class AgendaContext : DbContext
    {
        // Recebe a conexão do banco
        // O método deve permanecer vazio
        public AgendaContext(DbContextOptions<AgendaContext> options) : base(options)
        {

        }

        // Propriedade que refere-se á entidade (classe no programa e tabela no db)
        // Os registros da tabela de contato são acessado através do 'Contatos'
        public DbSet<Contato> Contatos { get; set; }
    }
}
```

### Vinculando a 'ConnectionString' á classe 'Program'
6. Na classe 'Program', vincular a 'ConnectionString' para a 'ConexãoPadrao'.
```
// Add services to the container.
builder.Services.AddDbContext<AgendaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoPadrao")));
```

### Realizando as 'Migrations'
7. Via terminal, espelhar as classes do código no banco de dados através da 'Migrations'. Ou seja, o EntityFramework irá criar a tabela automaticamente.
```
dotnet -ef migrations add CriacaoTabelaContato
```

> **Observação:**
Se o comando no terminal do Visual Studio não for reconhecido, então instalar o EntityFramework como ferramenta global. Utilizar o comando abaixo:
```
dotnet tool install --global dotnet-ef
```

8. Ao finalizar as 'migrations', será gerado automaticamente a seguinte classe 'CriacaoTabelaContato'. É o schema de criação da tabela no banco de dados (não deve ser modificado):
```
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationNETCore.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoTabelaContato : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contatos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contatos", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contatos");
        }
    }
}

```
### Aplicando as 'Migrations'
9. Aplicar a 'migration' ao banco de dados (Microsoft SQL Express). É o comando que constroí a tabela no banco de dados. Utilizar o seguinte comando no terminal:
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

### Criando um 'Controller'
10. Adicionar uma pasta chamada de 'Controller' e uma classe com o nome de 'ContatoController'. É o ponto de entrada de acesso aos métodos (Endpoints).
>**Deve obrigatoriamente ser herdada de 'ControllerBase'.**

>**Deve possuir os atributos [APIController] e [Route("[controller]")].**
```
using Microsoft.AspNetCore.Mvc;
using WebApplicationNETCore.Context;
using WebApplicationNETCore.Entities;

namespace WebApplicationNETCore.Controllers
{
    //A classe 'ContatoController' é onde serão desenvolvidos os métodos (Endpoints)
    // que irá popular a Tabela Contato criada anteriormente
    // pelo EntityFramework.

    [ApiController]
    [Route("[controller]")]
    public class ContatoController : ControllerBase // A herança deve ser feita da 'ControllerBase'
    {
        // Atributos
        private readonly AgendaContext _context;

        // Criando um Construtor
        public ContatoController(AgendaContext context)
        {
            _context = context;
        }

        // Adicionando os Endpoints da API
        // CRUD
        // Create
        // HttpPost (Verbo para enviar informação)
        [HttpPost]
        public IActionResult Create(Contato contato) // Objeto do tipo 'Contato' como parâmetro
        {
            _context.Add(contato);
            _context.SaveChanges();

            return Ok(contato);
        }

        // Read
        [HttpGet("{id}")] // Recebe o parametro 'id'
        public IActionResult ObterContatoPorId(int id) // Recebe o parametro do verbo HttpGet 'id'
        {
            // 'Contatos' é o db set (conjunto)
            // Busca o contato no db set
            var contato = _context.Contatos.Find(id);

            // Se o contato for inválido
            if (contato == null)
            {
                return NotFound(); // Retornar 'NotFound()'
            }

            return Ok(contato);
        }

        // Read
        [HttpGet("ObterPorNome")]
        public IActionResult ObterContatoPorNome(string nome)
        {
            // Obtem diretamente do banco, o contato onde o nome seja igual ao do
            // parametro 'nome' informado
            var contatos = _context.Contatos.Where(x => x.Nome.Contains(nome));

            return Ok(contatos);
        }

        // Update
        [HttpPut("{id}")]

        // Parametros
        // id, é o id á ser encontrado no db
        // contato, é o json com as informações do contato
        public IActionResult AtualizarContatoPorId(int id, Contato contato)
        {
            // Busca o contato pelo 'id' diretamente do banco de dados
            var contatoBanco = _context.Contatos.Find(id);

            if(contatoBanco == null)
            {
                return NotFound();
            }
            else
            {
                // Atribuindo um novo nome, telefone e propriedade 'ativo' ao contato
                // Onde:
                // contatoBanco, é o contato existente no db
                // contato.Nome, é o corpo da requisicao (json)
                contatoBanco.Nome = contato.Nome;
                contatoBanco.Telefone = contato.Telefone;
                contato.Ativo = contato.Ativo;

                // Inclui as novas informações no banco de dados
                _context.Contatos.Update(contatoBanco);
                
                // Salva as alterações no db
                _context.SaveChanges();

                return Ok(contatoBanco);
            }
        }

        // Delete
        [HttpDelete("{id}")]
        public IActionResult DeletarPorId(int id)
        {
            // Busca o contato diretamente no db a partir do 'id'
            var contatoBanco = _context.Contatos.Find(id);

            if(contatoBanco == null)
            {
                return NotFound();
            }
            else
            {
                // Deletar somente um contato que exista no db
                _context.Contatos.Remove(contatoBanco);

                // Salva as alterações no db
                _context.SaveChanges();

                // Retorna 'sem-conteudo'
                return NoContent();
            }

        }

    }
}

```
## EntityFramework - CRUD

A tabela abaixo descreve os **Endpoints** desenvolvidos:

| Controller             |CRUD    |Verbo Http |Request URL           | Descrição |
|------------------------|--------|-----------|----------------------|-----------|
| Contato                | CREATE | POST      |/contato              | Insere um novo contato na tabela 'Contatos' do banco de dados |
| Contato                | READ   | GET       |/contato/{id}         | Obtém um contato existente através do parâmetro {id} da tabela 'Contatos' do banco de dados |
| Contato                | READ   | GET       |/contato/ObterPorNome | Obtém um contato existente através do parâmetro 'nome' da tabela 'Contatos' do banco de dados |
| Contato                | UPDATE | PUT       |/contato/{id}         | Atualiza um contato existente através do parâmetro {id} da tabela 'Contatos' do banco de dados |
| Contato                | DELETE | DELETE    |/contato/{id}          | Deleta um contato existente através do parâmetro {id} da tabela 'Contatos' do banco de dados |




