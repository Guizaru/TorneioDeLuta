using MVC_Torneio.DTO;
using MVC_Torneio.Models;
using MVC_Torneio.Services.Interfaces;
using System.Text.Json;

namespace MVC_Torneio.Services
{
    public class LutadorService : ILutadorService
    {
        private readonly HttpClient _http;

        public LutadorService(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("TorneioApi");
        }

        public async Task<DadosLutador?> CriarLutador(CriarLutadorDto dto)
        {
            var response = await _http.PostAsJsonAsync("lutador/criarlutador", dto);
            
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ApiResponseModel<DadosLutador>>(json);

                return result?.Data;
            }

            return null;
        }

        public async Task<bool> DeletarLutador(int lutadorId)
        {
            var response = await _http.DeleteAsync($"lutador/deletelutador/{lutadorId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> EditarLutador(EditarLutadorDto dto)
        {
            var resposta = await _http.PutAsJsonAsync($"lutador/editarlutador/{dto.Id}", dto);

            var json = await resposta.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiResponseModel<DadosLutador>>(json);

            return resposta.IsSuccessStatusCode && result != null && result.Status;
        }

        public async Task<DadosLutador?> GetLutadorById(int id)
        {
            var response = await _http.GetStringAsync($"lutador/{id}");
            var result = JsonSerializer.Deserialize<ApiResponseModel<DadosLutador>>(response);

            return result?.Data;
        }

        public async Task<List<DadosLutador>> ListarLutadores()
        {
            var json = await _http.GetStringAsync("lutador/listarlutadores");
            var result = JsonSerializer.Deserialize<ApiResponseModel<List<DadosLutador>>>(json);

            return result?.Data ?? new List<DadosLutador>();
        }
    }
}
