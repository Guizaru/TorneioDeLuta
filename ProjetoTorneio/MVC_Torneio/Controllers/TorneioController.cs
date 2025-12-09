using Microsoft.AspNetCore.Mvc;
using MVC_Torneio.DTO;
using MVC_Torneio.Services.Interfaces;
using MVC_Torneio.ViewModels;

namespace MVC_Torneio.Controllers
{
    public class TorneioController : Controller
    {
        private readonly ITorneioService _torneioService;

        public TorneioController(ITorneioService torneioService)
        {
            _torneioService = torneioService;
        }

        public async Task<IActionResult> Listar()
        {
            var torneios = await _torneioService.ListarTorneios();
            return View(torneios);
        }

        public async Task<IActionResult> Lutadores(int torneioId)
        {
            var dados = await _torneioService.GetLutadoresByTorneioId(torneioId);

            if (dados == null)
                return NotFound();

            return View("Lutadores", dados);
        }

        [HttpGet, ActionName("Criar")]
        public IActionResult CriarTorneio() => View();

        [HttpPost, ActionName("Criar")]
        public async Task<IActionResult> CriarTorneio(CriarTorneioDto dto)
        {
            var torneio = await _torneioService.CriarTorneio(dto);

            if (torneio == null)
            {
                ModelState.AddModelError("", "Erro ao criar torneio.");
                return View(dto);
            }

            return RedirectToAction("Executar", new { torneioId = torneio.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int torneioId)
        {
            var dados = await _torneioService.ObterTorneio(torneioId);

            if (dados == null) return NotFound();

            var dto = new EditarTorneioDto
            {
                Id = dados.Id,
                Nome = dados.Nome,
                VencedorId = dados.VencedorId,
            };
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(EditarTorneioDto dto)
        {
            if (await _torneioService.EditarTorneio(dto))
                return RedirectToAction("Listar");

            ModelState.AddModelError("", "Erro ao editar torneio");
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Executar(int torneioId)
        {
            var lutadores = await _torneioService.ListarLutadores();
            var torneio = await  _torneioService.ObterTorneio(torneioId);

            if (torneio == null) return NotFound();

            var viewModel = new ExecutarTorneioViewModel
            {
                TorneioId = torneioId,
                Lutadores = lutadores.Select(l => new LutadorSelecionavel
                {
                    Id = l.Id,
                    Nome = l.Nome,
                    Idade = l.Idade,
                    Lutas = l.TotalLutas,
                    Derrotas = l.Derrotas,
                    Vitorias = l.Vitorias,
                    ArtesMarciais = l.ArtesMarciais
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Executar(ExecutarTorneioViewModel model)
        {
            if (model.LutadoresSelecionados.Count != 16)
            {
                ModelState.AddModelError("", "Você deve selecionar exatamente 16 lutadores.");

                var lutadores = await _torneioService.ListarLutadores();
                model.Lutadores = lutadores
                    .Select(l => new LutadorSelecionavel { Id = l.Id, Nome = l.Nome })
                    .ToList();

                return View(model);
            }

            var resultado = await _torneioService.ExecutarTorneio(model.TorneioId, model.LutadoresSelecionados);

            return RedirectToAction("Resultado", new { torneioId = model.TorneioId });
        }

        [HttpGet]
        public async Task<IActionResult> Resultado(int torneioId)
        {
            var torneio = await _torneioService.ObterTorneio(torneioId);

            if (torneio == null)
                return NotFound();

            return View(torneio);
        }

        [HttpPost]
        public async Task<IActionResult> Deletar(int torneioId)
        {
            var sucesso = await _torneioService.DeletarTorneio(torneioId);

            TempData[sucesso ? "Sucesso" : "Erro"] = sucesso
           ? "Torneio deletado com sucesso!"
           : "Não foi possível deletar o torneio.";

            return RedirectToAction("Listar");
        }
    }
}
