using API_Torneio.DTO.LutadorDTO;

namespace API_Torneio.DTO.TorneioDTO
{
    public class DtoTorneio
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataCriacao { get; set; }
        public List<DtoLutador>? Participantes { get; set; }
        public DtoLutador? Vencedor { get; set; }
    }
}
