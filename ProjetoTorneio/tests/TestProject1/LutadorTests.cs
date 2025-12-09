
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using MVC_Torneio.Controllers;
using MVC_Torneio.DTO;
using MVC_Torneio.Models;
using MVC_Torneio.Services.Interfaces;

namespace Endpoints.Test
{
    public class LutadorTests
    {
        [Fact]
        public async Task CriarLutador_Redireciona_Para_Listar()
        {
            var mockService = new Mock<ILutadorService>();

            mockService.Setup(s => s.CriarLutador(It.IsAny<CriarLutadorDto>()))
                .ReturnsAsync(new DadosLutador
                {
                    Id = 1,
                    Nome = "Nome teste",
                    Idade = 1,
                    ArtesMarciais = 1,
                    TotalLutas = 1,
                    Vitorias = 1,
                    Derrotas = 1
                });

            var controller = new LutadorController(mockService.Object);

            var dto = new CriarLutadorDto
            {
                Nome = "Teste nome",
                Idade = 1,
                ArtesMarciais = 1
            };

            var result = await controller.CriarLutador(dto);

            var redirect = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Listar", redirect.ActionName);
        }

        [Fact]
        public async Task DeletarLutador_Redirecionar_Para_Listar()
        {
            var mockService = new Mock<ILutadorService>();
            mockService.Setup(s => s.DeletarLutador(It.IsAny<int>()))
                .ReturnsAsync(true);

            var controller = new LutadorController(mockService.Object);

            controller.TempData = new TempDataDictionary(
                new DefaultHttpContext(),
                Mock.Of<ITempDataProvider>()
                );

            var result = await controller.Deletar(99);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Listar", redirect.ActionName);
        }

        [Fact]
        public async Task EditarLutador_Redireciona_Para_Listar()
        {
            var mockService = new Mock<ILutadorService>();
            mockService.Setup(s => s.EditarLutador(It.IsAny<EditarLutadorDto>()))
                .ReturnsAsync(true);

            var controller = new LutadorController(mockService.Object);

            var dto = new EditarLutadorDto
            {
                Id = 1,
                Nome = "Teste nome",
                Idade = 1,
                ArtesMarciais = 1
            };

            var result = await controller.Editar(dto);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Listar", redirect.ActionName);
        }

        [Fact]
        public async Task Listar_RetornaView_ComLutadores()
        {
            var mockService = new Mock<ILutadorService>();

            var listaMock = new List<DadosLutador>
            {
                new DadosLutador{Id = 1, Nome = "Lutador 1"},
                new DadosLutador { Id = 2, Nome = "Lutador 2" }
            }; 

            mockService.Setup(s => s.ListarLutadores()).ReturnsAsync(listaMock);

            var controller = new LutadorController(mockService.Object);

            var result = await controller.Listar();

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<List<DadosLutador>>(viewResult.Model);

            Assert.Equal(2, model.Count);
            Assert.Equal("Lutador 1", model[0].Nome);
            Assert.Equal("Lutador 2", model[1].Nome);
        }
        [Fact]
        public async Task Detalhes_RetornaViewComlutador()
        {
            var mockService = new Mock<ILutadorService>();
            mockService.Setup(s => s.GetLutadorById(1))
                .ReturnsAsync(new DadosLutador
                {
                    Id = 1,
                    Nome = "Teste nome"
                });
            var controller = new LutadorController(mockService.Object);

            var result = await controller.Detalhes(1);

            var view = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<DadosLutador>(view.Model);

            Assert.Equal(1, model.Id);
            Assert.Equal("Teste nome", model.Nome);
        }
    }
}
