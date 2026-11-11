using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using FinTrack.Core.Dtos;

namespace FinTrack.Tests
{
    public class CategoriasControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public CategoriasControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task ObterTodas_DeveRetornarStatus200()
        {
            // Act
            var response = await _client.GetAsync("/api/categorias");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Criar_ComDtoValido_DeveRetornarCreated()
        {
            // Arrange
            var novaCategoria = new CategoriaCreateDto
            {
                Nome = "Alimentação Teste",
                Tipo = "Despesa"
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/categorias", novaCategoria);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
    }
}