using System.Text.Json.Serialization;

namespace MVC_Torneio.Models
{
    public class ApiResponseModel<T>
    {

        [JsonPropertyName("data")]
        public T Data { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("status")]
        public bool Status { get; set; } = true;
    }
}
