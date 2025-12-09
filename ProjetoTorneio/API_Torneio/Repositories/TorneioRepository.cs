using API_Torneio.Context;
using API_Torneio.Models.Entities;
using API_Torneio.Pagination;
using API_Torneio.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API_Torneio.Repositories
{
    public class TorneioRepository
        : Repository<Torneio>, ITorneioRepository
    {
        public TorneioRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Lutador>> GetLutadoresPorId(List<int> lutadoresId)
        {
            return await _context.Lutadores
                .Where(l => lutadoresId.Contains(l.Id))
                .ToListAsync();
        }

        public async Task<Torneio?> GetTorneioCompletoById(int torneioId)
        {
            return await _context.Torneios
                .Include(t => t.Participantes)
                .Include(t => t.Vencedor)
                .FirstOrDefaultAsync(t => t.Id == torneioId);
        }

        public async Task<List<Torneio>> GetAllCompleto()
        {
            return await _context.Torneios
                .Include(t => t.Participantes)
                .Include(t => t.Vencedor)
                .ToListAsync();
        }
        public async Task<List<Torneio>> GetTorneioPaginacao(ItemsParameters torneioParams)
        {
            return await _context.Torneios
                .Include(t => t.Participantes)
                .Include(t => t.Vencedor)
                .OrderBy(t => t.Nome)
                .Skip((torneioParams.PageNumber - 1) * torneioParams.PageSize)
                .Take(torneioParams.PageSize)
                .ToListAsync();
        }

        public async Task UpdateTorneio(Torneio torneio)
        {
            _context.Torneios.Attach(torneio);

            _context.Entry(torneio).Property(t => t.Nome).IsModified = true;
            _context.Entry(torneio).Property(t => t.VencedorId).IsModified = true;

            _context.Entry(torneio).Property(t => t.DataCriacao).IsModified = false;

            await _context.SaveChangesAsync();
        }

    }
}
