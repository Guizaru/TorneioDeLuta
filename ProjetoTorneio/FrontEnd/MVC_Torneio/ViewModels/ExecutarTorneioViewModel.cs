namespace MVC_Torneio.ViewModels
{
    public class ExecutarTorneioViewModel
    {
        public int TorneioId { get; set; }

        public List<LutadorSelecionavel> Lutadores { get; set; } = new List<LutadorSelecionavel>();

        public List<int> LutadoresSelecionados { get; set; } = new List<int>();
    }

    public class LutadorSelecionavel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public int Lutas { get; set; }
        public int Derrotas { get; set; }
        public int Vitorias { get; set; }
        public int ArtesMarciais { get; set; }
    }
}
