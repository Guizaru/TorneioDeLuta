using System.Net.Http.Json;
using System.Text.Json;

namespace TesteApiTorneio
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            var http = new HttpClient(handler);

            int torneioId = 1;

            string url = $"https://localhost:7199/api/lutador/listarlutadores";

            var lutadoresId = new List<int>
            {
                3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18
            };

            Console.WriteLine("Enviando requisição para API...\n");

            try
            {
                var response = await http.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Falha na requisição. Status: " + response.StatusCode);
                    Console.ReadKey();
                    return;
                }

                var json = await response.Content.ReadAsStringAsync();

                var jsonObj = JsonSerializer.Deserialize<object>(json);
                var jsonPretty = JsonSerializer.Serialize(
                    jsonObj,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true,
                        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                    });

                Console.WriteLine("Resposta da API:");
                Console.WriteLine(jsonPretty);

            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao chamar API: " + e.Message);
                Console.ReadKey();
            }
        }
    }
}
