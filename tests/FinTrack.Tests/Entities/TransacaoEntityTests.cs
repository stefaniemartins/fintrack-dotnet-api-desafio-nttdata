using Xunit;
using FinTrack.Core.Entities;

namespace FinTrack.Tests.Entities
{
    public class TransacaoEntityTests
    {
        [Fact]
        public void DeveCriarTransacaoCorretamente()
        {
            var transacao = new Transacao
            {
                Id = 1,
                Descricao = "Supermercado",
                Valor = 150.00m,
                Data = DateTime.Now,
                Tipo = "Despesa",
                ContaId = 1,
                CategoriaId = 1
            };

            Assert.Equal(1, transacao.Id);
            Assert.Equal("Supermercado", transacao.Descricao);
            Assert.Equal(150.00m, transacao.Valor);
            Assert.Equal("Despesa", transacao.Tipo);
            Assert.Equal(1, transacao.ContaId);
            Assert.Equal(1, transacao.CategoriaId);
        }
    }
}