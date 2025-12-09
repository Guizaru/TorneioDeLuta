using MVC_Torneio.DTO;
using MVC_Torneio.Models;

namespace MVC_Torneio.Services.Interfaces
{
    public interface ILutadorService
    {
        Task<List<DadosLutador>> ListarLutadores();
        Task<DadosLutador?> GetLutadorById(int id);
        Task<DadosLutador> CriarLutador(CriarLutadorDto dto);
        Task<bool> EditarLutador(EditarLutadorDto dto);
        Task<bool> DeletarLutador(int id);
    }
}
