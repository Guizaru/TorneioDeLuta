using API_Torneio.Context;
using API_Torneio.Models.Entities;
using API_Torneio.Pagination;
using API_Torneio.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API_Torneio.Repositories
{
    public class LutadorRepository
       : Repository<Lutador>, ILutadorRepository
    {
        public LutadorRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Torneio?> GetLutadorByTorneioId(int torneioId)
        {
            return await _context.Torneios.Include(torneio => torneio.Participantes)
                .FirstOrDefaultAsync(torneio => torneio.Id == torneioId);
        }

        public async Task<List<Lutador>> GetLutadoresPaginacao(ItemsParameters lutadoresParams)
        {
            return await GetQueryable()
                .OrderBy(l => l.Nome)
                .Skip((lutadoresParams.PageNumber - 1) *  lutadoresParams.PageSize)
                .Take(lutadoresParams.PageSize).ToListAsync();
        }
    }
}
