using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using FinTrack.Core.Dtos;

namespace FinTrack.Tests
{
    public class ContasControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ContasControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task ObterTodasContas_DeveRetornarStatus200()
        {
            // Act
            var response = await _client.GetAsync("/api/contas");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task CriarConta_ComDtoValido_DeveRetornarCreated()
        {
            // Arrange
            var novaConta = new ContaCreateDto
            {
                Nome = "Conta Corrente Teste",
                SaldoInicial = 1000.00m
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/contas", novaConta);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
    }
}