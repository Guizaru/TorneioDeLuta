namespace MVC_Torneio.DTO
{
    public class ExecutarTorneioDto
    {
        public int TorneioId { get; set; }
        public List<int> LutadoresSelecionados { get; set; } = new();
    }
}
