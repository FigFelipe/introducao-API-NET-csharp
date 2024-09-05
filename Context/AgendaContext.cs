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
        public DbSet<Contato> Contatos { get; set; }
    }
}
