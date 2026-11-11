using System.Net;
using System.Net.Json;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using FinTrack.Core.Dtos;

namespace FinTrack.Tests
{
    public class TransacoesControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public TransacoesControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task ObterTodas_DeveRetornarStatus200()
        {
            // Act
            var response = await _client.GetAsync("/api/transacoes");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Criar_TransacaoComDadosInvalidos_DeveRetornarBadRequest()
        {
            // Arrange (DTO vazio ou inválido para acionar as validações)
            var transacaoInvalida = new TransacaoCreateDto
            {
                Descricao = "",
                Valor = -10,
                Data = DateTime.Now,
                Tipo = "Invalido"
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/transacoes", transacaoInvalida);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}