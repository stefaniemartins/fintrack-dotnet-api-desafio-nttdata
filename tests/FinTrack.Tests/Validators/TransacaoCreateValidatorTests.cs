using FinTrack.Core.Dtos;
using FinTrack.Core.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace FinTrack.Tests.Validators;

public class TransacaoCreateValidatorTests
{
    private readonly TransacaoCreateValidator _validator;

    public TransacaoCreateValidatorTests()
    {
        _validator = new TransacaoCreateValidator();
    }

    [Fact]
    public void Deve_Passar_Quando_Dados_Forem_Validos()
    {
        var model = new TransacaoCreateDto
        {
            Descricao = "Supermercado Mensal",
            Valor = 250.00m,
            Tipo = "Despesa",
            ContaId = 1,
            CategoriaId = 1
        };

        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-10.50)]
    [InlineData(-0.01)]
    public void Deve_Retornar_Erro_Quando_Valor_For_Zero_Ou_Negativo(decimal valorInvalido)
    {
        var model = new TransacaoCreateDto
        {
            Descricao = "Compra",
            Valor = valorInvalido,
            Tipo = "Despesa",
            ContaId = 1,
            CategoriaId = 1
        };

        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Valor);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Deve_Retornar_Erro_Quando_Descricao_For_Vazia_Ou_Nula(string descricaoInvalida)
    {
        var model = new TransacaoCreateDto
        {
            Descricao = descricaoInvalida,
            Valor = 100.00m,
            Tipo = "Receita",
            ContaId = 1,
            CategoriaId = 1
        };

        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Descricao);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("Transferencia")]
    public void Deve_Retornar_Erro_Quando_Tipo_For_Invalido(string tipoInvalido)
    {
        var model = new TransacaoCreateDto
        {
            Descricao = "Pagamento",
            Valor = 50.00m,
            Tipo = tipoInvalido,
            ContaId = 1,
            CategoriaId = 1
        };

        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Tipo);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Deve_Retornar_Erro_Quando_ContaId_For_Invalido(int contaIdInvalido)
    {
        var model = new TransacaoCreateDto
        {
            Descricao = "Lazer",
            Valor = 80.00m,
            Tipo = "Despesa",
            ContaId = contaIdInvalido,
            CategoriaId = 1
        };

        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.ContaId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Deve_Retornar_Erro_Quando_CategoriaId_For_Invalido(int categoriaIdInvalido)
    {
        var model = new TransacaoCreateDto
        {
            Descricao = "Lazer",
            Valor = 80.00m,
            Tipo = "Despesa",
            ContaId = 1,
            CategoriaId = categoriaIdInvalido
        };

        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.CategoriaId);
    }
}