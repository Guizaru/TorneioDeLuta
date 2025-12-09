namespace API_Torneio.Models.Entities
{
    public class Torneio
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; }
        public int? VencedorId { get; set; }
        public virtual Lutador? Vencedor { get; set; }
        public virtual ICollection<Lutador> Participantes { get; set; } = new List<Lutador>();
    }
}
