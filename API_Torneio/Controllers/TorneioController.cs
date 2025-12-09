using API_Torneio.DTO.TorneioDTO;
using API_Torneio.Models;
using API_Torneio.Models.Entities;
using API_Torneio.Pagination;
using API_Torneio.Services.TorneioService;
using Microsoft.AspNetCore.Mvc;

namespace API_Torneio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TorneioController : Controller
    {
        private readonly ITorneioServiceInterface _torneioInterface;

        public TorneioController(ITorneioServiceInterface torneioInterface)
        {
            _torneioInterface = torneioInterface;
        }

        [HttpPost("criarTorneio")]
        public async Task<ActionResult<ResponseModel<Torneio>>> CriarTorneio(DtoCreateTorneio dtoCreateTorneio)
        {
            var torneio = await _torneioInterface.CriarTorneio(dtoCreateTorneio);
            return new CreatedAtRouteResult("GetTorneioById", new { torneioId = torneio.Data!.Id }, torneio);
        }

        [HttpGet("pagination")]
        public async Task<ActionResult<List<Lutador>>> ListarTorneios([FromQuery] ItemsParameters torneiosParameters)
        {
            var lutadores = await _torneioInterface.GetTorneioPaginacao(torneiosParameters);

            return Ok(lutadores);
        }

        [HttpGet("listarTorneios")]
        public async Task<ActionResult<ResponseModel<List<Torneio>>>> ListarTorneios()
        {
            var torneio = await _torneioInterface.ListarTorneios();
            return Ok(torneio);
        }


        [HttpGet("{torneioId}", Name = "GetTorneioById")]
        public async Task<ActionResult<ResponseModel<Torneio>>> GetTorneioById(int torneioId)
        {
            var torneio = await _torneioInterface.GetTorneioById(torneioId);
            return Ok(torneio);
        }

        [HttpPut("editarTorneio/{torneioId}")]
        public async Task<ActionResult<ResponseModel<Torneio>>> EditarTorneio(DtoEditTorneio dtoEditTorneio, int torneioId)
        {
            var torneio = await _torneioInterface.EditarTorneio(torneioId, dtoEditTorneio);
            return Ok(torneio);
        }
        [HttpPost("executarTorneio/{torneioId}")]
        public async Task<ActionResult<ResponseModel<Torneio>>> ExecutarTorneio(int torneioId, [FromBody] List<int> lutadoresId)
        {
            var resultado = await _torneioInterface.ExecutarTorneio(torneioId, lutadoresId);
            return Ok(resultado);
        }

        [HttpDelete("deleteTorneio/{torneioId}")]
        public async Task<ActionResult<ResponseModel<Lutador>>> DeleteTorneio(int torneioId)
        {
            var torneio = await _torneioInterface.DeletarTorneio(torneioId);
            return Ok(torneio);
        }
    }
}
