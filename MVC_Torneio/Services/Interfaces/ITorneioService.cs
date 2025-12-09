using MVC_Torneio.DTO;
using MVC_Torneio.Models;
using System.Threading.Tasks;

namespace MVC_Torneio.Services.Interfaces
{
    public interface ITorneioService
    {
        Task <DadosTorneio> CriarTorneio(CriarTorneioDto dto);
        Task <bool> EditarTorneio(EditarTorneioDto dto);
        Task <List<DadosTorneio>> ListarTorneios();
        Task <DadosTorneio> ExecutarTorneio(int torneioId, List<int> lutadoresSelecionados);
        Task<List<DadosLutador>> GetLutadoresByTorneioId(int torneioId);
        Task<DadosTorneio?> ObterTorneio(int torneioId);
        Task<List<DadosLutador>> ListarLutadores();
        Task<bool> DeletarTorneio(int torneioId);

    }
}
