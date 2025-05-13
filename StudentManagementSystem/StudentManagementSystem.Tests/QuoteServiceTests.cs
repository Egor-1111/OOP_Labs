using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using StudentManagementSystem.Application;
using StudentManagementSystem.Application.DTOs;
using Xunit;

namespace StudentManagementSystem.Tests
{
    public class QuoteServiceTests
    {
        [Fact]
        public async Task GetMotivationalQuote_Success_ReturnsQuote()
        {
            // Arrange
            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{
                    ""content"": ""Test quote text"",
                    ""author"": ""Test Author""
                }"),
            };

            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            var httpClient = new HttpClient(handlerMock.Object);
            var quoteService = new QuoteService(httpClient);

            // Act
            var result = await quoteService.GetMotivationalQuote();

            // Assert
            Assert.Equal("Test quote text", result.Content);
            Assert.Equal("Test Author", result.Author);
        }

        [Fact]
        public async Task GetMotivationalQuote_ApiFails_ReturnsFallbackQuote()
        {
            // Arrange
            var handlerMock = new Mock<HttpMessageHandler>();

            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ThrowsAsync(new HttpRequestException("API failed"));

            var httpClient = new HttpClient(handlerMock.Object);
            var quoteService = new QuoteService(httpClient);

            // Act
            var result = await quoteService.GetMotivationalQuote();

            // Assert
            Assert.NotNull(result);
            Assert.Equal("The expert in anything was once a beginner.", result.Content);
            Assert.Equal("Helen Hayes", result.Author);
        }

        [Fact]
        public async Task GetMotivationalQuote_InvalidJson_ReturnsFallbackQuote()
        {
            // Arrange
            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("invalid json"),
            };

            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            var httpClient = new HttpClient(handlerMock.Object);
            var quoteService = new QuoteService(httpClient);

            // Act
            var result = await quoteService.GetMotivationalQuote();

            // Assert
            Assert.Equal("The expert in anything was once a beginner.", result.Content);
        }
    }
}