using API_Torneio.Controllers;
using API_Torneio.DTO.LutadorDTO;
using API_Torneio.DTO.TorneioDTO;
using API_Torneio.Models;
using API_Torneio.Models.Entities;
using API_Torneio.Repositories;
using API_Torneio.Services.TorneioService;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ControllersAPI.Test
{
    public class LutadorTests
    {
        [Fact]
        public async Task CriarLutador_Deve_Retornar201Created()
        {
            var mockInterface = new Mock<ILutadorServiceInterface>();

            var dto = new DtoCreateLutador
            {
                Nome = "Lutador Teste",
                Idade = 10,
                ArtesMarciais = 1
            };

            mockInterface
                .Setup(s => s.CreateLutador(It.IsAny<DtoCreateLutador>()))
                .ReturnsAsync((DtoCreateLutador dto) =>
                    new ResponseModel<Lutador>
                    {
                        Data = new Lutador
                        {
                            Nome = dto.Nome,
                            Idade = dto.Idade,
                            ArtesMarciais = dto.ArtesMarciais
                        },
                        Message = "Criado com sucesso",
                        Status = true
                    }
                );
            var controller = new LutadorController(mockInterface.Object);

            var result = await controller.CreateLutador(dto);

            var created = Assert.IsType <CreatedAtRouteResult>(result.Result);
            Assert.Equal(201, created.StatusCode);

            var model = Assert.IsType<ResponseModel<Lutador>>(created.Value);

            Assert.True(model.Status);
            Assert.Equal("Criado com sucesso", model.Message);
        }

        [Fact]
        public async Task EditarLutador_Deve_Retornar200OK()
        {
            var mockInterface = new Mock<ILutadorServiceInterface>();

            var dto = new DtoEditLutador
            {
                Nome = "Novo nome Lutador Teste",
                Idade = 10,
                ArtesMarciais = 1
            };

            mockInterface
                .Setup(s => s.EditLutador(It.IsAny<DtoEditLutador>(), It.IsAny<int>()))
                .ReturnsAsync((DtoEditLutador dto, int id) =>
                    new ResponseModel<Lutador>
                    {
                        Data = new Lutador
                        {
                            Id = id,
                            Nome = dto.Nome,
                            Idade = dto.Idade,
                            ArtesMarciais = dto.ArtesMarciais
                        },
                        Message = "Editado com sucesso",
                        Status = true
                    }
                );

            var controller = new LutadorController(mockInterface.Object);

            var result = await controller.EditLutador(dto, 1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, okResult.StatusCode);

            var model = Assert.IsType<ResponseModel<Lutador>>(okResult.Value);

            Assert.True(model.Status);
            Assert.Equal("Editado com sucesso", model.Message);
        }

        [Fact]
        public async Task DeletarLutador_Deve_Retornar200OK()
        {
            var mockInterface = new Mock<ILutadorServiceInterface>();

            mockInterface
                .Setup(s => s.DeleteLutador(It.IsAny<int>()))
                .ReturnsAsync(new ResponseModel<Lutador>
                {
                    Data = new Lutador
                    {
                        Id = 1,
                        Nome = "Lutador Teste"
                    },
                    Message = "Lutador removido com sucesso",
                    Status = true
                });

            var controller = new LutadorController(mockInterface.Object);

            var result = await controller.DeleteLutador(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, okResult.StatusCode);

            var model = Assert.IsType<ResponseModel<Lutador>>(okResult.Value);

            Assert.True(model.Status);
            Assert.Equal("Lutador removido com sucesso", model.Message);
        }

        [Fact]
        public async Task FindLutadorById_Deve_Retornar200OK()
        {
            var mockInterface = new Mock<ILutadorServiceInterface>();

            mockInterface
                .Setup(s => s.FindLutadorById(It.IsAny<int>()))
                .ReturnsAsync(new ResponseModel<Lutador>
                {
                    Data = new Lutador
                    {
                        Id = 1,
                        Nome = "Lutador Teste"
                    },
                    Message = "Lutador localizado",
                    Status = true
                });

            var controller = new LutadorController(mockInterface.Object);

            var result = await controller.FindLutadorById(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, okResult.StatusCode);

            var model = Assert.IsType<ResponseModel<Lutador>>(okResult.Value);

            Assert.True(model.Status);
            Assert.Equal("Lutador localizado", model.Message);
        }

        [Fact]
        public async Task FindLutadorByTorneioId_Deve_Retornar200OK()
        {
            var mockInterface = new Mock<ILutadorServiceInterface>();

            mockInterface
                .Setup(s => s.FindLutadorByTorneioId(It.IsAny<int>()))
                .ReturnsAsync(new ResponseModel<List<DtoLutador>>
                {
                    Data = new List<DtoLutador>
                    {
                        new DtoLutador{Id = 1, Nome = "Lutador Teste 1"},
                        new DtoLutador{Id = 1, Nome = "Lutador Teste 2"}
                    },
                    Message = "Lutadores do torneio localizados",
                    Status = true
                });

            var controller = new LutadorController(mockInterface.Object);

            var result = await controller.FindLutadorByTorneioId(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, okResult.StatusCode);

            var model = Assert.IsType<ResponseModel<List<DtoLutador>>>(okResult.Value);

            Assert.True(model.Status);
            Assert.Equal("Lutadores do torneio localizados", model.Message);
        }

        [Fact]
        public async Task ListLutador_Deve_Retornar200OK()
        {
            var mockInterface = new Mock<ILutadorServiceInterface>();

            mockInterface
                .Setup(s => s.ListLutador())
                .ReturnsAsync(new ResponseModel<List<DtoLutador>>
                {
                    Data = new List<DtoLutador>
                    {
                        new DtoLutador{Id = 1, Nome = "Lutador Teste 1"},
                        new DtoLutador{Id = 1, Nome = "Lutador Teste 2"}
                    },
                    Message = "Lutadores encontrados",
                    Status = true
                });

            var controller = new LutadorController(mockInterface.Object);

            var result = await controller.ListarLutadores();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, okResult.StatusCode);

            var model = Assert.IsType<ResponseModel<List<DtoLutador>>>(okResult.Value);

            Assert.True(model.Status);
            Assert.Equal("Lutadores encontrados", model.Message);
        }
    }
}
