using API_Torneio.Models.Entities;
using API_Torneio.Pagination;

namespace API_Torneio.Repositories.Interfaces
{
    public interface ILutadorRepository :IRepository<Lutador>
    {
        Task<Torneio?> GetLutadorByTorneioId(int torneioId);
        Task<List<Lutador>> GetLutadoresPaginacao(ItemsParameters lutadoresParams);
    }
}
