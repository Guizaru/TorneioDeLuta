using System.Text.Json.Serialization;

namespace MVC_Torneio.Models
{
    public class DadosTorneio
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("nome")]
        public string Nome { get; set; } = string.Empty;
        [JsonPropertyName("dataCriacao")]
        public DateTime DataCriacao { get; set; }
        [JsonPropertyName("vencedorId")]
        public int? VencedorId { get; set; }
        [JsonPropertyName("vencedor")]
        public DadosLutador? Vencedor { get; set; }
        [JsonPropertyName("participantes")]
        public List<DadosLutador> Participantes { get; set; } = new List<DadosLutador>();
        public string? VencedorNome => Vencedor?.Nome;
    }
}
