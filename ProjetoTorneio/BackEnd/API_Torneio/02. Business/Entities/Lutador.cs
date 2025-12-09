namespace API_Torneio.Models.Entities
{
    public class Lutador
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public int? Idade { get; set; }
        public int? ArtesMarciais { get; set; }
        public int TotalLutas { get; set; } = 0;
        public int Derrotas { get; set; } = 0;
        public int Vitorias { get; set; } = 0;

        public virtual ICollection<Torneio> Torneios { get; set; } = new List<Torneio>();
    }
}
