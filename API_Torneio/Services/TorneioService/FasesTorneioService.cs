using API_Torneio.Models.Entities;
using API_Torneio.Services.TorneioService;

public class FasesTorneioService : IFasesTorneioService
{
    public Lutador RealizarLuta(Lutador lutadorA, Lutador lutadorB)
    {
        double porcentagemLutadorA = lutadorA.TotalLutas == 0 ? 0 : (lutadorA.Vitorias / (double)lutadorA.TotalLutas) * 100;
        double porcentagemLutadorB = lutadorB.TotalLutas == 0 ? 0 : (lutadorB.Vitorias / (double)lutadorB.TotalLutas) * 100;

        Lutador vencedor;

        if (porcentagemLutadorA > porcentagemLutadorB) vencedor = lutadorA;
        else if (porcentagemLutadorB > porcentagemLutadorA) vencedor = lutadorB;
        else if (lutadorA.ArtesMarciais > lutadorB.ArtesMarciais) vencedor = lutadorA;
        else if (lutadorB.ArtesMarciais > lutadorA.ArtesMarciais) vencedor = lutadorB;
        else if (lutadorA.TotalLutas > lutadorB.TotalLutas) vencedor = lutadorA;
        else vencedor = lutadorB;

        lutadorA.TotalLutas++;
        lutadorB.TotalLutas++;

        vencedor.Vitorias++;
        if (vencedor == lutadorA) lutadorB.Derrotas++;
        else lutadorA.Derrotas++;

        return vencedor;
    }

    public List<Lutador> ExecutarFase(List<Lutador> lutadores)
    {
        var vencedores = new List<Lutador>();

        for (int i = 0; i < lutadores.Count; i += 2)
        {
            var vencedor = RealizarLuta(lutadores[i], lutadores[i + 1]);
            vencedores.Add(vencedor);
        }

        return vencedores;
    }
}
