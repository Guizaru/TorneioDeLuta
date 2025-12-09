using API_Torneio.DTO.LutadorDTO;
using API_Torneio.Models;
using API_Torneio.Models.Entities;
using API_Torneio.Pagination;
using API_Torneio.Repositories.Interfaces;
using AutoMapper;

namespace API_Torneio.Services.LutadorService
{
    public class LutadorService : ILutadorServiceInterface
    {
            private readonly ILutadorRepository _repository;
            private readonly IMapper _mapper;

            public LutadorService(ILutadorRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }
        
        public async Task<ResponseModel<Lutador>> CreateLutador(DtoCreateLutador dtoCreateLutador)
        {
            ResponseModel<Lutador> response = new ResponseModel<Lutador>();

            try
            {
                var lutador = _mapper.Map<Lutador>(dtoCreateLutador);

               await _repository.Add(lutador);
                        
                response.Data = lutador;
                response.Message = "Lutador criado com sucesso.";
                response.Status = true;
                return response;
            }
            catch (Exception)
            {
                response.Message = "Ocorreu um erro ao criar o lutador.";
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<Lutador>> DeleteLutador(int lutadorId)
        {
            ResponseModel<Lutador> response = new ResponseModel<Lutador>();

            try
            {
                var lutador = await _repository.Get(lutadorId);

                if (lutador == null)
                {
                    response.Message = "Não foi possível localizar o lutador";
                    response.Status = false;
                    return response;
                }
                else
                {
                    await _repository.Delete(lutador);

                    response.Data = lutador;
                    response.Message = "Lutador removido com sucesso.";
                    return response;
                }
            }
            catch (Exception)
            {
                response.Message = "Ocorreu um erro ao deletar o lutador.";
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<Lutador>> EditLutador(DtoEditLutador dtoEditLutador, int lutadorId)
        {
            ResponseModel<Lutador> response = new ResponseModel<Lutador>();

            try
            {
                var lutador = await _repository.Get(lutadorId);

                if (lutador == null)
                {
                    response.Message = "Lutador não encontrado.";
                    return response;
                }

                _mapper.Map(dtoEditLutador, lutador);
                await _repository.Update(lutador);
               
                response.Data = lutador;
                response.Message = "Lutador editado com sucesso.";
                return response;
                
            }
            catch (Exception)
            {
                response.Message = "Ocorreu um erro ao editar o lutador.";
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<Lutador>> FindLutadorById(int lutadorId)
        {
            ResponseModel<Lutador> response = new ResponseModel<Lutador>();

            try
            {
                var lutador = await _repository.Get(lutadorId);

                if (lutador == null)
                {
                    response.Message = "Lutador não encontrado no banco de dados.";
                    response.Status = false;
                    return response;
                }

                else
                {
                    response.Data = lutador;
                    response.Message = $"Lutador {lutador.Nome} encontrado";
                    return response;
                }
            }
            catch (Exception)
            {
                response.Message = "Ocorreu um erro ao tentar localizar o lutador";
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<DtoLutador>>> FindLutadorByTorneioId(int torneioId)
        {
            ResponseModel<List<DtoLutador>> response = new ResponseModel<List<DtoLutador>>();

            try
            {
                var torneio = await _repository.GetLutadorByTorneioId(torneioId);

                if (torneio == null)
                {
                    response.Message = "Torneio não encontrado no banco de dados.";
                    response.Status = false;
                    return response;
                }

                response.Data = _mapper.Map<List<DtoLutador>>(torneio.Participantes);
                response.Message = $"Participantes do torneio {torneio.Nome}";
                response.Status = true;
                return response;

            }
            catch (Exception)
            {
                response.Message = "Ocorreu um erro ao tentar encontrar a lista de participantes";
                response.Status = false;
                return response;
            }
        }
        public async Task<ResponseModel<List<DtoLutador>>> ListLutador()
        {
            ResponseModel<List<DtoLutador>> response = new ResponseModel<List<DtoLutador>>();

            try
            {
                var lutadores = await _repository.GetAll();

                response.Data = _mapper.Map<List<DtoLutador>>(lutadores);

                if (response.Data.Count == 0)
                {
                    response.Message = "Nenhum lutador encontrado na base de dados.";
                    response.Status = true;
                    return response;
                }

                response.Message = "Lutadores registrados na base de dados";
                response.Status = true;
                return response;
            }
            catch (Exception)
            {
                response.Message = "Ocorreu um erro ao tentar obter os lutadores";
                response.Status = false;
                return response;
            }
        }

            public async Task<ResponseModel<List<DtoLutador>>> GetLutadoresPaginacao(ItemsParameters lutadoresParams)
            {
                ResponseModel<List<DtoLutador>> response = new ResponseModel<List<DtoLutador>>();

                try
                {
                    var lutadores = await _repository.GetLutadoresPaginacao(lutadoresParams);

                    response.Data = _mapper.Map<List<DtoLutador>>(lutadores);

                    if (response.Data.Count == 0)
                    {
                        response.Message = "Nenhum lutador encontrado na base de dados.";
                        response.Status = true;
                        return response;
                    }

                    response.Message = "Lutadores registrados na base de dados";
                    response.Status = true;
                    return response;
                }
                catch (Exception)
                {
                    response.Message = "Ocorreu um erro ao tentar obter os lutadores";
                    response.Status = false;
                    return response;
                }
            }
        }
    }
