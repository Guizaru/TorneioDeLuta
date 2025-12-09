using System.Text.Json.Serialization;

namespace MVC_Torneio.Models
{
    public class DadosLutador
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        [JsonPropertyName("idade")]
        public int Idade { get; set; }

        [JsonPropertyName("artesMarciais")]
        public int ArtesMarciais { get; set; }

        [JsonPropertyName("totalLutas")]
        public int TotalLutas { get; set; }

        [JsonPropertyName("vitorias")]
        public int Vitorias { get; set; }

        [JsonPropertyName("derrotas")]
        public int Derrotas { get; set; }
    }
}
