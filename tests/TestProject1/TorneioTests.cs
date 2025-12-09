using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using MVC_Torneio.Controllers;
using MVC_Torneio.DTO;
using MVC_Torneio.Models;
using MVC_Torneio.Services.Interfaces;
using MVC_Torneio.ViewModels;

namespace Endpoints.Test
{
    public class TorneioTests
    {
        [Fact]
        public async Task CriarTorneio_RedirecionaParaExecutar()
        {
            var mockService = new Mock<ITorneioService>();
            mockService.Setup(s => s.CriarTorneio(It.IsAny<CriarTorneioDto>()))
                .ReturnsAsync(new DadosTorneio { Id = 10 });

            var controller = new TorneioController (mockService.Object);

            var dto = new CriarTorneioDto();

            var result = await controller.CriarTorneio(dto);

            var redirect = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Executar", redirect.ActionName);
            Assert.Equal(10, redirect.RouteValues["torneioId"]);
        }

        [Fact]
        public async Task DeletarTorneio_RedirecionaParaListar()
        {
            var mockService = new Mock<ITorneioService>();

            mockService.Setup(s => s.DeletarTorneio(It.IsAny<int>())).ReturnsAsync(true);

            var controller = new TorneioController(mockService.Object);

            controller.TempData = new TempDataDictionary(
                new DefaultHttpContext(),
                Mock.Of<ITempDataProvider>()
                );

            var result = await controller.Deletar(99);

            var redirect = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Listar", redirect.ActionName);
        }

        [Fact]
        public async Task ExecutarTorneio_DeveRedirecionar_Para_TelaDeResultado()
        {
            var mockService = new Mock<ITorneioService>();
            mockService.Setup(s => s.ExecutarTorneio(It.IsAny<int>(), It.IsAny<List<int>>()))
                .ReturnsAsync(new DadosTorneio
                {
                    Id = 10
                });

            var controller = new TorneioController(mockService.Object);

            var model = new ExecutarTorneioViewModel
            {
                TorneioId = 10,
                LutadoresSelecionados = Enumerable.Range(1, 16).ToList()

            };

            var result = await controller.Executar(model);

            var redirect = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Resultado", redirect.ActionName);
            Assert.Equal(10, redirect.RouteValues["torneioId"]);

        }

        [Fact]
        public async Task EditarTorneio_RedirecionaParaListar()
        {
            var mockService = new Mock<ITorneioService>();
            mockService.Setup(s => s.EditarTorneio(It.IsAny<EditarTorneioDto>()))
                .ReturnsAsync(true);
         

            var controller = new TorneioController(mockService.Object);

            var dto = new EditarTorneioDto { Nome = "Torneio Teste" };

            var result = await controller.Editar(dto);

            var redirect = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Listar", redirect.ActionName);
        }

        [Fact]
        public async Task Listar_RetornaView_ComTorneios()
        {
            var mockService = new Mock<ITorneioService>();

            var listaMock = new List<DadosTorneio>
            {
                new DadosTorneio{Id = 1, Nome = "Torneio Teste"},
                new DadosTorneio{Id = 2, Nome = "Torneio Teste 2"}
            };

            mockService.Setup(s => s.ListarTorneios()).ReturnsAsync(listaMock);

            var controller = new TorneioController(mockService.Object);

            var result = await controller.Listar();

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<List<DadosTorneio>>(viewResult.Model); 
            
            Assert.Equal(2, model.Count);
            Assert.Equal("Torneio Teste", model[0].Nome);
            Assert.Equal("Torneio Teste 2", model[1].Nome);
        }

        [Fact]
        public async Task Lutadores_RetornaViewComLutadores()
        {
            var mockService = new Mock<ITorneioService>();
            var listaMock = new List<DadosLutador> 
            {
                new DadosLutador{Id = 1, Nome = "Lutador1"},
                new DadosLutador{Id = 2, Nome = "Lutador2"}
            };

            mockService.Setup(s => s.GetLutadoresByTorneioId(1))
                .ReturnsAsync(listaMock);

            var controller = new TorneioController(mockService.Object);

            var result = await controller.Lutadores(1);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Lutadores", viewResult.ViewName);

            var model = Assert.IsType<List<DadosLutador>>(viewResult.Model);
            Assert.Equal(2, model.Count);
            Assert.Equal("Lutador1", model[0].Nome);
            Assert.Equal("Lutador2", model[1].Nome);
        }

        [Fact]
        public async Task Resultado_RetornaView()
        {
            var mockService = new Mock<ITorneioService>();
            mockService.Setup(s => s.ObterTorneio(It.IsAny<int>()))
                .ReturnsAsync(new DadosTorneio
                {
                    Id = 1,
                    Nome = "Teste Torneio",
                    VencedorId = 1,
                });
            
            var controller = new TorneioController(mockService.Object);
            var result = await controller.Resultado(1);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<DadosTorneio>(viewResult.Model);

            Assert.Equal(1, model.Id);
            Assert.Equal("Teste Torneio", model.Nome);
            Assert.Equal(1, model.VencedorId);
        }
    }
}
