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
