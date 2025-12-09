using API_Torneio.DTO.LutadorDTO;
using API_Torneio.DTO.TorneioDTO;
using API_Torneio.Models;
using API_Torneio.Models.Entities;
using API_Torneio.Pagination;

namespace API_Torneio.Services.TorneioService
{
    public interface ITorneioServiceInterface
    {
        Task<ResponseModel<Torneio>> CriarTorneio(DtoCreateTorneio dtoCreateTorneio);
        Task<ResponseModel<List<DtoTorneio>>> ListarTorneios();
        Task<ResponseModel<List<DtoTorneio>>> GetTorneioPaginacao(ItemsParameters lutadoresParams);
        Task<ResponseModel<Torneio>> GetTorneioById(int torneioid);
        Task<ResponseModel<DtoTorneio>> ExecutarTorneio(int torneioId, List<int> lutadoresId);
        Task<ResponseModel<DtoTorneio>> EditarTorneio(int torneioId, DtoEditTorneio dtoEditTorneio);
        Task<ResponseModel<Torneio>> DeletarTorneio(int torneioId);
    }
}
