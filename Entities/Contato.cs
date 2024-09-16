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
