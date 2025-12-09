using MVC_Torneio.DTO;
using MVC_Torneio.Models;
using MVC_Torneio.Services.Interfaces;
using System.Text.Json;

namespace MVC_Torneio.Services
{
    public class TorneioService : ITorneioService
    {
        private readonly HttpClient _http;

        public TorneioService(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("TorneioApi");
        }

        public async Task<DadosTorneio?> CriarTorneio(CriarTorneioDto dto)
        {
            var response = await _http.PostAsJsonAsync("torneio/criartorneio", dto);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ApiResponseModel<DadosTorneio>>(json);

                return result?.Data;
            }

            return null;
        }
        public async Task<bool> EditarTorneio(EditarTorneioDto dto)
        {
            var resposta = await _http.PutAsJsonAsync($"torneio/editartorneio/{dto.Id}", dto);

            var json = await resposta.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiResponseModel<DadosTorneio>>(json);

            return resposta.IsSuccessStatusCode && result != null && result.Status;
        }

        public async Task<bool> DeletarTorneio(int torneioId)
        {
            var response = await _http.DeleteAsync($"torneio/deletetorneio/{torneioId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<DadosTorneio?> ExecutarTorneio(int torneioId, List<int> lutadoresSelecionados)
        {
            var response = await _http.PostAsJsonAsync($"torneio/executartorneio/{torneioId}",
                lutadoresSelecionados);

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiResponseModel<DadosTorneio>>(json);

            return result?.Data;
        }

        public async Task<List<DadosLutador>> ListarLutadores()
        {
            var response = await _http.GetStringAsync("lutador/listarlutadores");
            var result = JsonSerializer.Deserialize<ApiResponseModel<List<DadosLutador>>>(response);

            return result?.Data ?? new List<DadosLutador>();

        }

        public async Task<List<DadosTorneio>> ListarTorneios()
        {
            var response = await _http.GetStringAsync("torneio/listartorneios");

            var result = JsonSerializer.Deserialize<ApiResponseModel<List<DadosTorneio>>>(response);

            return result?.Data ?? new List<DadosTorneio>();
        }

        public async Task<DadosTorneio?> ObterTorneio(int torneioId)
        {
            var response = await _http.GetStringAsync($"torneio/{torneioId}");

            var result = JsonSerializer.Deserialize<ApiResponseModel<DadosTorneio>>(response);

            return result?.Data;
        }

        public async Task<List<DadosLutador>> GetLutadoresByTorneioId(int torneioId)
        {
            var json = await _http.GetStringAsync($"lutador/lutadorestorneio/{torneioId}");
            var result = JsonSerializer.Deserialize<ApiResponseModel<List<DadosLutador>>>(json);

            return result?.Data ?? new List<DadosLutador>();
        }
    }
}

        