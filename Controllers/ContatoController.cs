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
        // Update
        // Delete

    }
}
