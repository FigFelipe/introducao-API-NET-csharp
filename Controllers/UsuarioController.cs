using Microsoft.AspNetCore.Mvc;

namespace WebApplicationNETCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        [HttpGet("ObterDataHoraAtual")]
        public IActionResult ObterDataHora()
        {
            var obj = new
            {
                Data = DateTime.Now.ToLongDateString(),
                Hora = DateTime.Now.ToShortTimeString()
            };

            return Ok(obj);
        }

        // Metodo que recebe um parâmetro
        [HttpGet("Apresentar/{nome}")]
        public IActionResult Apresentar(string nome)
        {
            var mensagem = $"Olá {nome}, seja bem-vindo";

            // Retorna um objeto anônimo com a mensagem
            return Ok(new {mensagem});
        }
    }
    
}
