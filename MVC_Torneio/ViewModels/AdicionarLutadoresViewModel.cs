namespace MVC_Torneio.ViewModels
{
    public class AdicionarLutadoresViewModel
    {
        public int TorneioId { get; set; }
        public List<LutadorSelecionavel> Lutadores { get; set; } = new List<LutadorSelecionavel>();
        public List<int> LutadoresSelecionados { get; set; } = new List<int>();
    }

    public class AdicionarLutadorSelecionavel
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
    }
}
