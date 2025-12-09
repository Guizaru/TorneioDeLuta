using API_Torneio.Models.Entities;
using API_Torneio.Pagination;

namespace API_Torneio.Repositories.Interfaces
{
    public interface ITorneioRepository :IRepository<Torneio>
    {
        Task<List<Lutador>> GetLutadoresPorId(List<int> ids);
        Task<Torneio?> GetTorneioCompletoById(int torneioId);
        Task<List<Torneio>> GetTorneioPaginacao(ItemsParameters torneioParams);
        Task UpdateTorneio(Torneio torneio);
        Task<List<Torneio>> GetAllCompleto();
    }
}
