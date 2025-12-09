using API_Torneio.Controllers;
using API_Torneio.DTO.TorneioDTO;
using API_Torneio.Models;
using API_Torneio.Models.Entities;
using API_Torneio.Services.TorneioService;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ControllersAPI.Test
{
    public class TorneioTests
    {
        [Fact]
        public async Task CriarTorneio_Deve_Retornar201Created()
        {
            var mockInterface = new Mock<ITorneioServiceInterface>();

            var dto = new DtoCreateTorneio
            {
                Nome = "Torneio Teste",
            };

            mockInterface
                .Setup(setup => setup.CriarTorneio(It.IsAny<DtoCreateTorneio>()))
                .ReturnsAsync((DtoCreateTorneio dto) =>
                    new ResponseModel<Torneio>
                    {
                        Data = new Torneio
                        {
                            Nome = dto.Nome,
                        },
                        Message = "Criado com sucesso",
                        Status = true
                    }
                );
            var controller = new TorneioController(mockInterface.Object);

            var result = await controller.CriarTorneio(dto);

            var created = Assert.IsType<CreatedAtRouteResult>(result.Result);
            Assert.Equal(201, created.StatusCode);

            var model = Assert.IsType<ResponseModel<Torneio>>(created.Value);

            Assert.True(model.Status);
            Assert.Equal("Criado com sucesso", model.Message);
        }

        [Fact]
        public async Task EditarTorneio_Deve_Retornar200OK()
        {
            var mockInterface = new Mock<ITorneioServiceInterface>();

            var dto = new DtoEditTorneio
            {
                Nome = "Novo nome Torneio Teste",
            };

            mockInterface
                .Setup(s => s.EditarTorneio(It.IsAny<int>(), It.IsAny<DtoEditTorneio>()))
                .ReturnsAsync((int id, DtoEditTorneio dto) =>
                    new ResponseModel<DtoTorneio>
                    {
                        Data = new DtoTorneio
                        {
                            Id = id,
                            Nome = dto.Nome!
                        },
                        Message = "Editado com sucesso",
                        Status = true
                    }
                );

            var controller = new TorneioController(mockInterface.Object);

            var result = await controller.EditarTorneio(dto, 1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, okResult.StatusCode);

            var model = Assert.IsType<ResponseModel<DtoTorneio>>(okResult.Value);

            Assert.True(model.Status);
            Assert.Equal("Editado com sucesso", model.Message);
        }

        [Fact]
        public async Task DeletarTorneio_Deve_Retornar200OK()
        {
            var mockInterface = new  Mock<ITorneioServiceInterface>();

            mockInterface
                .Setup(s => s.DeletarTorneio(It.IsAny<int>()))
                .ReturnsAsync(new ResponseModel<Torneio>
                {
                    Data = new Torneio
                    {
                        Id = 1,
                        Nome = "Teste Torneio"
                    },
                    Message = "Torneio removido com sucesso",
                    Status = true
                });

            var controller = new TorneioController(mockInterface.Object);

            var result = await controller.DeleteTorneio(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, okResult.StatusCode);

            var model = Assert.IsType<ResponseModel<Torneio>>(okResult.Value);

            Assert.True(model.Status);
            Assert.Equal("Torneio removido com sucesso", model.Message);
        }

        [Fact]
        public async Task FindTorneioById_Deve_Retornar200OK()
        {
            var mockInterface = new Mock<ITorneioServiceInterface>();

            mockInterface
                .Setup(s => s.GetTorneioById(It.IsAny<int>()))
                .ReturnsAsync(new ResponseModel<Torneio>
                {
                    Data = new Torneio
                    {
                        Id = 1,
                        Nome = "Lutador Teste"
                    },
                    Message = "Torneio localizado",
                    Status = true
                });

            var controller = new TorneioController(mockInterface.Object);

            var result = await controller.GetTorneioById(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, okResult.StatusCode);

            var model = Assert.IsType<ResponseModel<Torneio>>(okResult.Value);

            Assert.True(model.Status);
            Assert.Equal("Torneio localizado", model.Message);
        }

        [Fact]
        public async Task ListTorneio_Deve_Retornar200OK()
        {
            var mockInterface = new Mock<ITorneioServiceInterface>();

            mockInterface
                .Setup(s => s.ListarTorneios())
                .ReturnsAsync(new ResponseModel<List<DtoTorneio>>
                {
                    Data = new List<DtoTorneio>
                    {
                        new DtoTorneio { Id = 1, Nome = "Torneio Teste 1" },
                        new DtoTorneio { Id = 2, Nome = "Torneio Teste 2" },
                    },
                    Message = "Torneios encontrados",
                    Status = true
                });

            var controller = new TorneioController(mockInterface.Object);

            var result = await controller.ListarTorneios();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, okResult.StatusCode);

            var model = Assert.IsType<ResponseModel<List<DtoTorneio>>>(okResult.Value);

            Assert.True(model.Status);
            Assert.Equal("Torneios encontrados", model.Message);
        }

        [Fact] 
        public async Task ExecutarTorneio_Deve_Retornar200OK()
        {
            var mockInterface = new Mock<ITorneioServiceInterface>();

            mockInterface
                .Setup(s => s.ExecutarTorneio(It.IsAny<int>(), It.IsAny<List<int>>()))
                .ReturnsAsync(new ResponseModel<DtoTorneio>
                {
                    Data = new DtoTorneio
                    {
                        Id = 1,
                        Nome = "Torneio Teste 1"
                    },
                    Message = "Torneio executado",
                    Status = true
                });
            var controller = new TorneioController(mockInterface.Object);

            var ids = new List<int>
            {
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17
            };

            var result = await controller.ExecutarTorneio(1, ids);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, okResult.StatusCode);

            var model = Assert.IsType<ResponseModel<DtoTorneio>>(okResult.Value);

            Assert.True(model.Status);
            Assert.Equal("Torneio executado", model.Message);
        }
    }
}
