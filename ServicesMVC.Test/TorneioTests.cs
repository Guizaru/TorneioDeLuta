using System.Net;
using System.Text.Json;
using Moq;
using MVC_Torneio.DTO;
using MVC_Torneio.Models;
using MVC_Torneio.Services;

namespace ServicesMVC.Test
{
    public class TorneioTests
    {
        [Fact]
        public async Task CriarTorneio_Deve_RetornarDados()
        {
            var fakeResponse = new ApiResponseModel<DadosTorneio>()
            {
                Status = true,
                Data = new DadosTorneio {Id = 1, Nome = "Torneio Teste" }
            };

            var httpClient = HttpClientMockHelper.CreateHttpClient(
                new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonSerializer.Serialize(fakeResponse))
                },
                inspectRequest: request =>
                {
                    Assert.Equal(HttpMethod.Post, request.Method);
                    Assert.Equal("http://localhost/torneio/criartorneio", request.RequestUri!.ToString());
                }
            );

            var factoryMock = new Mock<IHttpClientFactory>();
            factoryMock.Setup(factory => factory.CreateClient("TorneioApi")).Returns(httpClient);

            var service = new TorneioService(factoryMock.Object);

            var dto = new CriarTorneioDto { Nome = "Torneio Teste" };

            var result = await service.CriarTorneio(dto);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Torneio Teste", result.Nome);
        }

        [Fact]
        public async Task DeletarTorneio_Deve_RetornarStatusDeSucesso()
        {
            var httpClient = HttpClientMockHelper.CreateHttpClient(
                new HttpResponseMessage(HttpStatusCode.OK),
                inspectRequest: request =>
                {
                    Assert.Equal(HttpMethod.Delete, request.Method);
                    Assert.Equal("http://localhost/torneio/deletetorneio/1", request.RequestUri!.ToString());
                }
            );

            var factoryMock = new Mock<IHttpClientFactory>();
            factoryMock.Setup(factory => factory.CreateClient("TorneioApi")).Returns(httpClient);

            var service = new TorneioService(factoryMock.Object);

            var result = await service.DeletarTorneio(1);

            Assert.True(result);
        }

        [Fact]
        public async Task EditarTorneio_Deve_RetornarDadosEStatusDeSucesso()
        {
            var fakeResponse = new ApiResponseModel<DadosTorneio>()
            {
                Status = true,
                Data = new DadosTorneio() { Id = 1, Nome = "Torneio Teste" }
            };

            var httpClient = HttpClientMockHelper.CreateHttpClient(
                new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonSerializer.Serialize(fakeResponse))
                },
                inspectRequest: request =>
                {
                    Assert.Equal(HttpMethod.Put, request.Method);
                    Assert.Equal("http://localhost/torneio/editartorneio/1", request.RequestUri!.ToString());
                }
            );

            var factoryMock = new Mock<IHttpClientFactory>();
            factoryMock.Setup(factory => factory.CreateClient("TorneioApi")).Returns(httpClient);

            var service = new TorneioService(factoryMock.Object);

            var dto = new EditarTorneioDto()
            {
                Id = 1,
                Nome = "Torneio Teste"

            };

            var result = await service.EditarTorneio(dto);

            Assert.True(result);
        }

        [Fact]
        public async Task ObterTorneio_Deve_RetornarDados()
        {
            var fakeResponse = new ApiResponseModel<DadosTorneio>()
            {
                Status = true,
                Data = new DadosTorneio() { Id = 1, Nome = "Torneio Teste" }
            };

            var httpClient = HttpClientMockHelper.CreateHttpClient(
                new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonSerializer.Serialize(fakeResponse))
                },
                inspectRequest: request =>
                {
                    Assert.Equal(HttpMethod.Get, request.Method);
                    Assert.Equal("http://localhost/torneio/1", request.RequestUri!.ToString());
                }
            );

            var factoryMock = new Mock<IHttpClientFactory>();
            factoryMock.Setup(factory => factory.CreateClient("TorneioApi")).Returns(httpClient);

            var service = new TorneioService(factoryMock.Object);

            var result = await service.ObterTorneio(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Torneio Teste", result.Nome);
        }

        [Fact]
        public async Task ListarTorneios_Deve_RetornarDados()
        {
            var fakeResponse = new ApiResponseModel<List<DadosTorneio>>()
            {
                Data = new List<DadosTorneio>()
                {
                    new DadosTorneio { Id = 1, Nome = "Torneio Teste 1" },
                    new DadosTorneio { Id = 2, Nome = "Torneio Teste 2" }
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
                    Assert.Equal("http://localhost/torneio/listartorneios", request.RequestUri!.ToString());
                }
            );

            var factoryMock = new Mock<IHttpClientFactory>();
            factoryMock.Setup(factory => factory.CreateClient("TorneioApi")).Returns(httpClient);

            var service = new TorneioService(factoryMock.Object);

            var result = await service.ListarTorneios();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(1, result[0].Id);
            Assert.Equal(2, result[1].Id);
            Assert.Equal("Torneio Teste 1", result[0].Nome);
            Assert.Equal("Torneio Teste 2", result[1].Nome);
        }

        [Fact]
        public async Task GetLutadoresByTorneioId_Deve_RetornarDados()
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
                    Assert.Equal("http://localhost/lutador/lutadorestorneio/1", request.RequestUri!.ToString());
                }
            );

            var factoryMock = new Mock<IHttpClientFactory>();
            factoryMock.Setup(factory => factory.CreateClient("TorneioApi")).Returns(httpClient);

            var service = new TorneioService(factoryMock.Object);

            var result = await service.GetLutadoresByTorneioId(1);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(1, result[0].Id);
            Assert.Equal(2, result[1].Id);
            Assert.Equal("Lutador Teste 1", result[0].Nome);
            Assert.Equal("Lutador Teste 2", result[1].Nome);
        }

        [Fact]
        public async Task ExecutarTorneio_Deve_RetornarDados()
        {
            var fakeResponse = new ApiResponseModel<DadosTorneio>()
            {
                Status = true,
                Data = new DadosTorneio() { Id = 1, Nome = "Torneio Teste", VencedorId = 1 }
            };
            var httpClient = HttpClientMockHelper.CreateHttpClient(
            new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonSerializer.Serialize(fakeResponse))
            },
                inspectRequest: async request =>
                {
                    Assert.Equal(HttpMethod.Post, request.Method);
                    Assert.Equal("http://localhost/torneio/executartorneio/1", request.RequestUri!.ToString());

                    var body = await request.Content!.ReadAsStringAsync();
                    var enviados = JsonSerializer.Deserialize<List<int>>(body);

                    Assert.NotNull(enviados);
                    Assert.Equal(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 }, enviados);
                }
            );

            var factoryMock = new Mock<IHttpClientFactory>();
            factoryMock.Setup(factory => factory.CreateClient("TorneioApi")).Returns(httpClient);

            var service = new TorneioService(factoryMock.Object);

            var result = await service.ExecutarTorneio(1, new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 });

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Torneio Teste", result.Nome);
            Assert.Equal(1, result.VencedorId);
        }
    }
}
