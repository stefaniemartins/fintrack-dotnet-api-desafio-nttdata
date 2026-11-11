using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using FinTrack.Core.Dtos;

namespace FinTrack.Tests.Api.Controllers
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
            var response = await _client.GetAsync("/api/v1/contas");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ObterPorId_ComIdInexistente_DeveRetornarNotFound()
        {
            var response = await _client.GetAsync("/api/v1/contas/9999");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task CriarConta_ComDtoValido_DeveRetornarCreated()
        {
            var novaConta = new ContaCreateDto
            {
                Nome = "Conta Corrente Teste",
                SaldoInicial = 1000.00m
            };

            var response = await _client.PostAsJsonAsync("/api/v1/contas", novaConta);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task Deletar_ComIdInexistente_DeveRetornarNotFound()
        {
            var response = await _client.DeleteAsync("/api/v1/contas/9999");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}