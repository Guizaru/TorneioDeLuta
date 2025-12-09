using Microsoft.AspNetCore.Mvc;
using MVC_Torneio.DTO;
using MVC_Torneio.Models;
using MVC_Torneio.Services.Interfaces;
using System.Text.Json;

namespace MVC_Torneio.Controllers
{
    public class LutadorController : Controller
    {
        private readonly ILutadorService _lutadorService;

        public LutadorController(ILutadorService lutadorService)
        {
            _lutadorService = lutadorService;
        }

        public async Task<IActionResult> Listar()
        {
            var lutadores = await _lutadorService.ListarLutadores();
            return View(lutadores);
        }

        [HttpGet, ActionName("Criar")]
        public IActionResult CriarLutador() => View();

        [HttpPost, ActionName("Criar")]
        public async Task<IActionResult> CriarLutador(CriarLutadorDto dto)
        {
            var lutador = await _lutadorService.CriarLutador(dto);

            if (lutador == null)
            {
                ModelState.AddModelError("", "Erro ao criar lutador.");
                return View(dto);
            }

            return RedirectToAction("Listar");
        }
        [HttpGet]
        public async Task<IActionResult> Editar(int lutadorId)
        {
            var dados = await _lutadorService.GetLutadorById(lutadorId);

            if (dados == null) return NotFound();

            var dto = new EditarLutadorDto
            {
                Id = dados.Id,
                Nome = dados.Nome,
                Idade = dados.Idade,
                ArtesMarciais = dados.ArtesMarciais,
            };
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(EditarLutadorDto dto)
        {
            if (await _lutadorService.EditarLutador(dto)) 
                return RedirectToAction("Listar");

            ModelState.AddModelError("", "Erro ao editar lutador");
            return View(dto);

        }

        [HttpPost]
        public async Task<IActionResult> Deletar(int lutadorId)
        {
            if (!await _lutadorService.DeletarLutador(lutadorId))
                TempData["Erro"] = "Não foi possível deletar o lutador.";
            else
                TempData["Sucesso"] = "Lutador deletado com sucesso!";

            return RedirectToAction("Listar");
        }

        [ActionName("Detalhes")]
        public async Task<IActionResult> Detalhes(int lutadorId)
        {
            var lutador = await _lutadorService.GetLutadorById(lutadorId);

            if (lutador == null)
            {
                TempData["Erro"] = "Lutador não encontrado.";
                return View(new DadosLutador()); 
            }

            return View(lutador);
        }

      
    }
}
       
