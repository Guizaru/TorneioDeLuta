using API_Torneio.DTO.LutadorDTO;
using API_Torneio.DTO.TorneioDTO;
using API_Torneio.Models;
using API_Torneio.Models.Entities;
using API_Torneio.Pagination;
using API_Torneio.Repositories.Interfaces;
using AutoMapper;

namespace API_Torneio.Services.TorneioService
{
    public class TorneioService : ITorneioServiceInterface
    {

        private readonly ITorneioRepository _torneioRepository;
        private readonly IFasesTorneioService _fasesTorneio;
        private readonly ILutadorRepository _lutadorRepository;
        private readonly IMapper _mapper;

        public TorneioService(ITorneioRepository torneioRepository, ILutadorRepository lutadorRepository, IFasesTorneioService fasesTorneio, IMapper mapper)
        {
            _torneioRepository = torneioRepository;
            _lutadorRepository = lutadorRepository;
            _fasesTorneio = fasesTorneio;
            _mapper = mapper;

        }

        public async Task<ResponseModel<Torneio>> CriarTorneio(DtoCreateTorneio dtocreateTorneio)
        {
            var response = new ResponseModel<Torneio>();

            try
            {
                if (string.IsNullOrWhiteSpace(dtocreateTorneio.Nome))
                {
                    response.Status = false;
                    response.Message = "O nome do torneio é obrigatório.";
                    return response;
                }

                var torneio = _mapper.Map<Torneio>(dtocreateTorneio);
                torneio.DataCriacao = DateTime.UtcNow;
                await _torneioRepository.Add(torneio);

                response.Data = torneio;
                response.Message = "Torneio criado com sucesso!";
                response.Status = true;

                return response;
            }
            catch (Exception)
            {
                response.Status = false;
                response.Message = $"Erro ao criar o torneio.";
                return response;
            }
        }
        public async Task<ResponseModel<List<DtoTorneio>>> ListarTorneios()
        {
            var response = new ResponseModel<List<DtoTorneio>>();

            try
            {
                var torneios = await _torneioRepository.GetAllCompleto();

                response.Data = _mapper.Map<List<DtoTorneio>>(torneios);
               
                response.Status = true;
                response.Message = "Lista de torneios obtida com sucesso.";
                return response;
            }
            catch (Exception)
            {
                response.Status = false;
                response.Message = "Ocorreu um erro ao tentar obter os torneios";
                return response;
            }
        }

        public async Task<ResponseModel<List<DtoTorneio>>> GetTorneioPaginacao(ItemsParameters lutadoresParams)
        {
            ResponseModel<List<DtoTorneio>> response = new ResponseModel<List<DtoTorneio>>();

            try
            {
                var torneios = await _torneioRepository.GetTorneioPaginacao(lutadoresParams);

                response.Data = _mapper.Map<List<DtoTorneio>>(torneios);

                if (response.Data.Count == 0)
                {
                    response.Message = "Nenhum torneio encontrado na base de dados.";
                    response.Status = true;
                    return response;
                }

                response.Message = "Torneios registrados na base de dados";
                response.Status = true;
                return response;
            }
            catch (Exception)
            {
                response.Message = "Ocorreu um erro ao tentar obter os torneios";
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<DtoTorneio>> ExecutarTorneio(int torneioId, List<int> lutadoresId)
        {
            ResponseModel<DtoTorneio> response = new ResponseModel<DtoTorneio>();

            try
            {
                var torneio = await _torneioRepository.Get(torneioId);

                if (torneio == null)
                {
                    response.Message = "O torneio não existe no banco de dados.";
                    response.Status = false;

                    return response;
                }

                var lutadores = await _torneioRepository.GetLutadoresPorId(lutadoresId);

                if (lutadores.Count != 16)
                {
                    response.Message = "O torneio precisa ter exatamente 16 lutadores.";
                    response.Status = false;
                    return response;
                }

                lutadores = lutadores.OrderBy(lutador => lutador.Idade).ToList();
                torneio.Participantes = lutadores;

                var oitavas = _fasesTorneio.ExecutarFase(lutadores);
                var quartas = _fasesTorneio.ExecutarFase(oitavas);
                var semis = _fasesTorneio.ExecutarFase(quartas);
                var final = _fasesTorneio.ExecutarFase(semis);

                var campeao = final.First();

                torneio.VencedorId = campeao.Id;
                await _torneioRepository.UpdateTorneio(torneio);

                var torneioDto = _mapper.Map<DtoTorneio>(torneio);

                response.Data = torneioDto;
                response.Message = $"Vencedor do torneio = {campeao.Nome}";
                response.Status = true;

                return response;
            }
            catch (Exception)
            {
                response.Message = "Ocorreu um erro ao relizar o torneio";
                response.Status = false;
                return response;
            }
        }
        public async Task<ResponseModel<DtoTorneio>> EditarTorneio(int torneioId, DtoEditTorneio dtoEdit)
        {
            var response = new ResponseModel<DtoTorneio>();

            try
            {
                var torneio = await _torneioRepository.Get(torneioId);

                if (torneio == null)
                {
                    response.Status = false;
                    response.Message = "Torneio não encontrado.";
                    return response;
                }

                if (!string.IsNullOrWhiteSpace(dtoEdit.Nome))
                    torneio.Nome = dtoEdit.Nome;

                if (dtoEdit.VencedorId.HasValue)
                {
                    var vencedor = await _lutadorRepository.Get(dtoEdit.VencedorId.Value);

                    if (vencedor == null)
                    {
                        response.Status = false;
                        response.Message = "Vencedor informado não existe.";
                        return response;
                    }

                    torneio.VencedorId = vencedor.Id;
                }

                await _torneioRepository.UpdateTorneio(torneio);

                var torneioDto = _mapper.Map<DtoTorneio>(torneio);
                response.Status = true;
                response.Message = "Torneio atualizado com sucesso!";
                response.Data = torneioDto;

                return response;
            }
            catch (Exception)
            {
                response.Status = false;
                response.Message = "Ocorreu um erro ao atualizar o torneio.";
                return response;
            }
        }

        public async Task<ResponseModel<Torneio>> GetTorneioById(int torneioId)
        {
            ResponseModel<Torneio> response = new ResponseModel<Torneio>();

            try
            {
                var torneio = await _torneioRepository.GetTorneioCompletoById(torneioId);

                if (torneio == null)
                {
                    response.Message = "Torneio não encontrado no banco de dados.";
                    response.Status = false;
                    return response;
                }

                else
                {
                    response.Data = torneio;
                    response.Status = true;
                    response.Message = $"Torneio {torneio.Nome} encontrado";
                    return response;
                }
            }
            catch (Exception)
            {
                response.Message = "Ocorreu um erro ao tentar localizar o torneio";
                response.Status = false;
                return response;
            }
        }
        public async Task<ResponseModel<Torneio>> DeletarTorneio(int torneioId)
        {
            ResponseModel<Torneio> response = new ResponseModel<Torneio>();

            try
            {
                var torneio = await _torneioRepository.Get(torneioId);

                if (torneio == null)
                {
                    response.Message = "Não foi possível localizar o torneio";
                    response.Status = false;
                    return response;
                }
                else
                {
                   await _torneioRepository.Delete(torneio);

                    response.Data = torneio;
                    response.Message = "Torneio removido com sucesso.";
                    return response;
                }
            }
            catch (Exception)
            {
                response.Message = "Ocorreu um erro ao deletar o torneio.";
                response.Status = false;
                return response;
            }
        }
    }
}
