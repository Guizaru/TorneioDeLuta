using API_Torneio.DTO.TorneioDTO;
using API_Torneio.Models.Entities;
using API_Torneio.Repositories.Interfaces;
using API_Torneio.Services.TorneioService;
using AutoMapper;
using Moq;

namespace ServicesAPI.Test
{
    public class TorneioTests
    {
        private Mock<IMapper> _mapperMock = new Mock<IMapper>();
        private Mock<ITorneioRepository> _repoMock = new Mock<ITorneioRepository>();
        private Mock<ILutadorRepository> _lutadorRepoMock = new Mock<ILutadorRepository>();
        private Mock<IFasesTorneioService> _fasesMock = new Mock<IFasesTorneioService>();
        private TorneioService _service;

        public TorneioTests()
        {
            _service = new TorneioService(_repoMock.Object,_lutadorRepoMock.Object, _fasesMock.Object, _mapperMock.Object);
        }


        [Fact]
        public async Task ListarTorneios_DeveRetornarLista()
        {
            var lista = new List<Torneio>
            {
                new Torneio { Id = 1, Nome = "Torneio A" }
            };

            var listaDto = new List<DtoTorneio>
            {
                new DtoTorneio { Id = 1, Nome = "Torneio A" }
            };

            _repoMock.Setup(r => r.GetAllCompleto()).ReturnsAsync(lista);
            _mapperMock.Setup(m => m.Map<List<DtoTorneio>>(lista)).Returns(listaDto);

            var result = await _service.ListarTorneios();

            Assert.True(result.Status);
            Assert.Equal("Torneio A", result.Data![0].Nome);
        }

        [Fact]
        public async Task CriarTorneio_DeveCriar()
        {
            var dto = new DtoCreateTorneio { Nome = "Novo Torneio" };
            var entidade = new Torneio { Id = 1, Nome = "Novo Torneio" };

            _mapperMock.Setup(m => m.Map<Torneio>(dto)).Returns(entidade);
            _repoMock.Setup(r => r.Add(It.IsAny<Torneio>())).Returns(Task.CompletedTask);

            var result = await _service.CriarTorneio(dto);

            Assert.True(result.Status);
            Assert.Equal("Torneio criado com sucesso!", result.Message);
            Assert.Equal("Novo Torneio", result.Data!.Nome);
        }

        [Fact]
        public async Task BuscarPorId_DeveRetornar()
        {
            var torneio = new Torneio { Id = 10, Nome = "Torneio X" };
            var dto = new DtoTorneio { Id = 10, Nome = "Torneio X" };

            _repoMock.Setup(r => r.GetTorneioCompletoById(10)).ReturnsAsync(torneio);
            _mapperMock.Setup(m => m.Map<DtoTorneio>(torneio)).Returns(dto);

            var result = await _service.GetTorneioById(10);

            Assert.True(result.Status);
            Assert.Equal("Torneio X", result.Data!.Nome);
        }

        [Fact]
        public async Task EditarTorneio_DeveAtualizar()
        {
            var dto = new DtoEditTorneio { Nome = "Atualizado" };
            var torneio = new Torneio { Id = 1, Nome = "Antigo" };

            _repoMock.Setup(r => r.Get(1)).ReturnsAsync(torneio);

            _mapperMock.Setup(m => m.Map(dto, torneio))
                .Callback<DtoEditTorneio, Torneio>((src, dest) =>
                {
                    dest.Nome = src.Nome;
                });

            var result = await _service.EditarTorneio(1, dto);

            Assert.True(result.Status);
            Assert.Equal("Atualizado", torneio.Nome);
            Assert.Equal("Torneio atualizado com sucesso!", result.Message);
        }

        [Fact]
        public async Task DeletarTorneio_DeveRemover()
        {
            var torneio = new Torneio { Id = 3, Nome = "Torneio X" };

            _repoMock.Setup(r => r.Get(3)).ReturnsAsync(torneio);
            _repoMock.Setup(r => r.Delete(torneio)).Returns(Task.CompletedTask);

            var result = await _service.DeletarTorneio(3);

            Assert.True(result.Status);
            Assert.Equal("Torneio removido com sucesso.", result.Message);
            Assert.Equal(torneio, result.Data);
        }

        [Fact]
        public async Task ExecutarTorneio_DeveRetornarVencedor()
        {
            var torneio = new Torneio { Id = 1, Nome = "Teste Torneio" };

            var lutadores = Enumerable.Range(1, 16)
                .Select(i => new Lutador { Id = i, Nome = $"Lutador {i}", Idade = 20 + i })
                .ToList();

            _repoMock.Setup(r => r.Get(1)).ReturnsAsync(torneio);
            _repoMock.Setup(r => r.GetLutadoresPorId(It.IsAny<List<int>>()))
                     .ReturnsAsync(lutadores);

            _fasesMock.Setup(f => f.ExecutarFase(It.IsAny<List<Lutador>>()))
                      .Returns<List<Lutador>>(lista => new List<Lutador> { lista.First() });

            _repoMock.Setup(r => r.UpdateTorneio(It.IsAny<Torneio>()))
                     .Returns(Task.CompletedTask);

            _mapperMock.Setup(m => m.Map<DtoTorneio>(It.IsAny<Torneio>()))
                .Returns(new DtoTorneio { Id = 1, Nome = "Teste Torneio" });

            var result = await _service.ExecutarTorneio(1, lutadores.Select(l => l.Id).ToList());

            Assert.True(result.Status);
            Assert.Equal("Teste Torneio", result.Data.Nome);
            Assert.Equal(1, torneio.VencedorId); 
        }
    }
}
