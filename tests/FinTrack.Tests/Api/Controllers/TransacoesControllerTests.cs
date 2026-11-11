using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using FinTrack.Core.Dtos;

namespace FinTrack.Tests.Api.Controllers
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
            var response = await _client.GetAsync("/api/v1/transacoes");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ObterPorId_ComIdInexistente_DeveRetornarNotFound()
        {
            var response = await _client.GetAsync("/api/v1/transacoes/9999");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Criar_TransacaoComDadosInvalidos_DeveRetornarBadRequest()
        {
            var transacaoInvalida = new TransacaoCreateDto
            {
                Descricao = "",
                Valor = -10,
                Data = DateTime.Now,
                Tipo = "Invalido",
                ContaId = 0,
                CategoriaId = 0
            };

            var response = await _client.PostAsJsonAsync("/api/v1/transacoes", transacaoInvalida);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Deletar_ComIdInexistente_DeveRetornarNotFound()
        {
            var response = await _client.DeleteAsync("/api/v1/transacoes/9999");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}