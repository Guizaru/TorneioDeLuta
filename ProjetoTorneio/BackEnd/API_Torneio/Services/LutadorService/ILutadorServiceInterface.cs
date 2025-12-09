using API_Torneio.DTO.LutadorDTO;
using API_Torneio.Models;
using API_Torneio.Models.Entities;
using API_Torneio.Pagination;

public interface ILutadorServiceInterface
{
    Task<ResponseModel<Lutador>> CreateLutador(DtoCreateLutador dtoCreateLutador);
    Task<ResponseModel<Lutador>> EditLutador(DtoEditLutador dtoEditLutador, int lutadorId);
    Task<ResponseModel<Lutador>> DeleteLutador(int lutadorId);
    Task<ResponseModel<Lutador>> FindLutadorById(int lutadorId);
    Task<ResponseModel<List<DtoLutador>>> ListLutador();
    Task<ResponseModel<List<DtoLutador>>> GetLutadoresPaginacao(ItemsParameters lutadoresParams);
    Task<ResponseModel<List<DtoLutador>>> FindLutadorByTorneioId(int torneioId);
}
