using Moq;
using Moq.Protected;

namespace ServicesMVC.Test
{
    public static class HttpClientMockHelper
    {
        public static HttpClient CreateHttpClient (HttpResponseMessage responseMessage, Action<HttpRequestMessage>? inspectRequest = null)
        {
            var handler = new Mock<HttpMessageHandler>();

            handler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", 
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken> ()
                )
                .Callback<HttpRequestMessage, CancellationToken>((request, token) =>
                {
                    inspectRequest?.Invoke(request);
                })
                .ReturnsAsync(responseMessage);

            return new HttpClient(handler.Object)
            {
                BaseAddress = new Uri("http://localhost/")
            };
        }
    }
}
