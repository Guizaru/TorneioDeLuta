using API_Torneio.DTO.LutadorDTO;
using API_Torneio.Models.Entities;
using API_Torneio.Repositories.Interfaces;
using API_Torneio.Services.LutadorService;
using AutoMapper;
using Moq;

namespace ServicesAPI.Test
{
    public class LutadorTests
    {
        private Mock<IMapper> _mapperMock = new Mock<IMapper>();
        private Mock<ILutadorRepository> _repoMock = new Mock<ILutadorRepository>();
        private LutadorService _service;

        public LutadorTests()
        {
            _service = new LutadorService(_repoMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task ListLutador_DeveRetornarTodos()
        {
            var lista = new List<Lutador>
            {
                  new Lutador { Nome = "A" }
            };

            var listaDto = new List<DtoLutador>
            {
                new DtoLutador { Nome = "A" }
            };

            _repoMock.Setup(r => r.GetAll()).ReturnsAsync(lista);
            _mapperMock.Setup(m => m.Map<List<DtoLutador>>(lista)).Returns(listaDto);

            var result = await _service.ListLutador();

            Assert.True(result.Status);
            Assert.Single(result.Data);
        }

        [Fact]
        public async Task FindLutadorById_DeveRetornar()
        {
            var lutador = new Lutador { Id = 1, Nome = "AAA" };

            _repoMock.Setup(r => r.Get(1)).ReturnsAsync(lutador);

            var result = await _service.FindLutadorById(1);

            Assert.Equal("Lutador AAA encontrado", result.Message);
            Assert.Equal(lutador, result.Data);
        }

        [Fact]
        public async Task FindLutadorByTorneioId_DeveRetornarLista()
        {
            var participantes = new List<Lutador>
            {
                new Lutador { Nome = "A" },
                new Lutador { Nome = "B" }
            };

            var torneio = new Torneio
            {
                Id = 5,
                Nome = "Torneio",
                Participantes = participantes
            };

            var participantesDto = new List<DtoLutador>
            {
                new DtoLutador { Nome = "A" },
                new DtoLutador { Nome = "B" }
            };

            _repoMock.Setup(r => r.GetLutadorByTorneioId(5)).ReturnsAsync(torneio);

            _mapperMock
                .Setup(m => m.Map<List<DtoLutador>>(participantes))
                .Returns(participantesDto);

            var result = await _service.FindLutadorByTorneioId(5);

            Assert.True(result.Status);
            Assert.Equal(2, result.Data!.Count);
        }

        [Fact]
        public async Task CreateLutador_DeveCriarLutador()
        {
            var dto = new DtoCreateLutador { Nome = "Teste", Idade = 20 };
            var lutadorMapeado = new Lutador { Id = 1, Nome = "Teste", Idade = 20 };

            _mapperMock
                .Setup(m => m.Map<Lutador>(dto))
                .Returns(lutadorMapeado);

             _repoMock
                .Setup(r => r.Add(It.IsAny<Lutador>()))
                .Returns(Task.CompletedTask);

            var result = await _service.CreateLutador(dto);

            Assert.True(result.Status);
            Assert.Equal("Lutador criado com sucesso.", result.Message);
            Assert.Equal("Teste", result.Data!.Nome);
        }

        [Fact]
        public async Task DeleteLutador_DeveRemover()
        {
            var lutador = new Lutador { Id = 1, Nome = "AAA" };

            _repoMock.Setup(r => r.Get(1)).ReturnsAsync(lutador);
            _repoMock.Setup(r => r.Delete(lutador)).Returns(Task.CompletedTask);

            var result = await _service.DeleteLutador(1);

            Assert.Equal("Lutador removido com sucesso.", result.Message);
            Assert.Equal(lutador, result.Data);
        }

        [Fact]
        public async Task EditLutador_DeveAtualizar()
        {
            var dto = new DtoEditLutador { Nome = "Novo", Idade = 30 };
            var lutador = new Lutador { Id = 1, Nome = "Antigo", Idade = 20 };

            _repoMock.Setup(r => r.Get(1)).ReturnsAsync(lutador);

            _mapperMock
                .Setup(m => m.Map(dto, lutador))
                .Callback<DtoEditLutador, Lutador>((src, dest) =>
                {
                    dest.Nome = src.Nome;
                    dest.Idade = src.Idade;
                });

            var result = await _service.EditLutador(dto, 1);

            Assert.Equal("Novo", lutador.Nome);
            Assert.Equal(30, lutador.Idade);
            Assert.Equal("Lutador editado com sucesso.", result.Message);
        }
    }
}