using API_Torneio.DTO.LutadorDTO;
using API_Torneio.Models;
using API_Torneio.Models.Entities;
using API_Torneio.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Torneio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LutadorController : ControllerBase
    {
        private readonly ILutadorServiceInterface _lutadorInterface;

        public LutadorController(ILutadorServiceInterface lutadorInterface)
        {
            _lutadorInterface = lutadorInterface;
        }

        [HttpGet("pagination")]
        public async Task<ActionResult<List<Lutador>>> ListarLutadores([FromQuery] ItemsParameters lutadoresParameters)
        {
            var lutadores = await _lutadorInterface.GetLutadoresPaginacao(lutadoresParameters);

            return Ok(lutadores);
        }

        [HttpGet("ListarLutadores")]
        public async Task<ActionResult<ResponseModel<List<Lutador>>>> ListarLutadores()
        {
            var lutadores = await _lutadorInterface.ListLutador();
            return Ok(lutadores);
        }

        [HttpGet("{lutadorId}", Name = "FindLutadorById")]
        public async Task <ActionResult<ResponseModel<Lutador>>> FindLutadorById(int lutadorId)
        {
            var lutador = await _lutadorInterface.FindLutadorById(lutadorId);
            return Ok(lutador);
        }
        [HttpGet("lutadoresTorneio/{torneioId}")]
        public async Task<ActionResult<ResponseModel<List<Lutador>>>> FindLutadorByTorneioId(int torneioId)
        {
                var lutadores = await _lutadorInterface.FindLutadorByTorneioId(torneioId);
                return Ok(lutadores);
        }
        [HttpPost("criarLutador")]
        public async Task<ActionResult<ResponseModel<Lutador>>> CreateLutador(DtoCreateLutador dtoCreateLutador)
        {
            var lutador = await _lutadorInterface.CreateLutador(dtoCreateLutador);
            return new CreatedAtRouteResult("FindLutadorById", new { lutadorId = lutador.Data!.Id }, lutador);
        }
        [HttpPut("editarLutador/{lutadorId}")]
        public async Task<ActionResult<ResponseModel<Lutador>>> EditLutador(DtoEditLutador dtoEditLutador, int lutadorId)
        {
            var lutador = await _lutadorInterface.EditLutador(dtoEditLutador, lutadorId);
            return Ok(lutador);
        }
        [HttpDelete("DeleteLutador/{lutadorId}")]
        public async Task<ActionResult<ResponseModel<Lutador>>> DeleteLutador(int lutadorId)
        {
            var lutador = await _lutadorInterface.DeleteLutador(lutadorId);
            return Ok(lutador);
        }
    }
}
