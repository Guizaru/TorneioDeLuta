using API_Torneio.DTO.LutadorDTO;
using API_Torneio.Models.Entities;

namespace API_Torneio.Services.TorneioService
{
    public interface IFasesTorneioService
    {
        Lutador RealizarLuta(Lutador lutadorA, Lutador lutadorB);
        List<Lutador> ExecutarFase(List<Lutador> lutadores);
    }
}
