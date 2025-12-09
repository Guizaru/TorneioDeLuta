using System.Net;
using System.Text.Json;
using Moq;
using MVC_Torneio.DTO;
using MVC_Torneio.Models;
using MVC_Torneio.Services;

namespace ServicesMVC.Test
{
    
    public class LutadorTests
    {
        [Fact]
        public async Task CriarLutador_Deve_RetornarDados()
        {
            var fakeResponse = new ApiResponseModel<DadosLutador>()
            {
                Status = true,
                Data = new DadosLutador { Id = 1, Nome = "Lutador Teste" }
            };

            var httpClient = HttpClientMockHelper.CreateHttpClient(
                new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonSerializer.Serialize(fakeResponse))
                },
                inspectRequest: request =>
                {
                    Assert.Equal(HttpMethod.Post, request.Method);
                    Assert.Equal("http://localhost/lutador/criarlutador", request.RequestUri!.ToString());
                }
            );

            var factoryMock = new Mock<IHttpClientFactory>();
            factoryMock.Setup(factory => factory.CreateClient("TorneioApi")).Returns(httpClient);

            var service = new LutadorService(factoryMock.Object);

            var dto = new CriarLutadorDto { Nome = "Lutador Teste" };
            
            var result = await service.CriarLutador(dto);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task DeletarLutador_Deve_RetornarStatusCodeDeSucesso()
        {
            var httpClient = HttpClientMockHelper.CreateHttpClient(
                new HttpResponseMessage(HttpStatusCode.OK),
                inspectRequest: request =>
                {
                    Assert.Equal(HttpMethod.Delete, request.Method);
                    Assert.Equal("http://localhost/lutador/deletelutador/1", request.RequestUri!.ToString());
                }
            );

            var factoryMock = new Mock<IHttpClientFactory>();
            factoryMock.Setup(factory => factory.CreateClient("TorneioApi")).Returns(httpClient);

            var service = new LutadorService(factoryMock.Object);

            var result = await service.DeletarLutador(1);

            Assert.True(result);
        }

        [Fact]
        public async Task EditarLutador_Deve_RetornarDadosEStatusDeSucesso()
        {
            var fakeResponse = new ApiResponseModel<DadosLutador>()
            {
                Status = true,
                Data = new DadosLutador() { Id = 1, Nome = "Lutador Teste" }
            };

            var httpClient = HttpClientMockHelper.CreateHttpClient(
                new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonSerializer.Serialize(fakeResponse))
                },
                inspectRequest: request =>
                {
                    Assert.Equal(HttpMethod.Put, request.Method);
                    Assert.Equal("http://localhost/lutador/editarlutador/1", request.RequestUri!.ToString());
                }
            );

            var factoryMock = new Mock<IHttpClientFactory>();
            factoryMock.Setup(factory => factory.CreateClient("TorneioApi")).Returns(httpClient);

            var service = new LutadorService(factoryMock.Object);

            var dto = new EditarLutadorDto()
            {
                Id = 1,
                Nome = "Lutador Teste"

            };

            var result = await service.EditarLutador(dto);

            Assert.True(result);
        }

        [Fact]
        public async Task GetLutadorById_Deve_RetornarDados()
        {
            var fakeResponse = new ApiResponseModel<DadosLutador>()
            {
                Status = true,
                Data = new DadosLutador() { Id = 1, Nome = "Lutador Teste" }
            };

            var httpClient = HttpClientMockHelper.CreateHttpClient(
                new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonSerializer.Serialize(fakeResponse))
                },
                inspectRequest: request =>
                {
                    Assert.Equal(HttpMethod.Get, request.Method);
                    Assert.Equal("http://localhost/lutador/1", request.RequestUri!.ToString());
                }
            );

            var factoryMock = new Mock<IHttpClientFactory>();
            factoryMock.Setup(factory => factory.CreateClient("TorneioApi")).Returns(httpClient);

            var service = new LutadorService(factoryMock.Object);

            var result = await service.GetLutadorById(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Lutador Teste", result.Nome);
        }

        [Fact]
        public async Task ListarLutadores_Deve_RetornarDados()
        {
            var fakeResponse = new ApiResponseModel<List<DadosLutador>>()
            {
                Data = new List<DadosLutador>()
                {
                    new DadosLutador { Id = 1, Nome = "Lutador Teste 1" },
                    new DadosLutador { Id = 2, Nome = "Lutador Teste 2" }
                }
            };

            var httpClient = HttpClientMockHelper.CreateHttpClient(
                new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonSerializer.Serialize(fakeResponse))
                },
                inspectRequest: request =>
                {
                    Assert.Equal(HttpMethod.Get, request.Method);
                    Assert.Equal("http://localhost/lutador/listarlutadores", request.RequestUri!.ToString());
                }
            );

            var factoryMock = new Mock<IHttpClientFactory>();
            factoryMock.Setup(factory => factory.CreateClient("TorneioApi")).Returns(httpClient);

            var service = new LutadorService(factoryMock.Object);

            var result = await service.ListarLutadores();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(1, result[0].Id);
            Assert.Equal(2, result[1].Id);
            Assert.Equal("Lutador Teste 1", result[0].Nome);
            Assert.Equal("Lutador Teste 2", result[1].Nome);
        }
    }
}
