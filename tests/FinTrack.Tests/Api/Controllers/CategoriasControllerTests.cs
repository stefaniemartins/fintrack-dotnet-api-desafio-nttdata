using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using FinTrack.Core.Dtos;

namespace FinTrack.Tests.Api.Controllers
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
            var response = await _client.GetAsync("/api/v1/categorias");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ObterPorId_ComIdInexistente_DeveRetornarNotFound()
        {
            var response = await _client.GetAsync("/api/v1/categorias/9999");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Criar_ComDtoValido_DeveRetornarCreated()
        {
            var novaCategoria = new CategoriaCreateDto
            {
                Nome = "Alimentação Teste",
                Tipo = "Despesa"
            };

            var response = await _client.PostAsJsonAsync("/api/v1/categorias", novaCategoria);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task Deletar_ComIdInexistente_DeveRetornarNotFound()
        {
            var response = await _client.DeleteAsync("/api/v1/categorias/9999");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}