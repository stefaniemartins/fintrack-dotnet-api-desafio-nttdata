using FinTrack.Core.Entities;
using FinTrack.Core.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace FinTrack.Tests.Validators;

public class CategoriaValidatorTests
{
    private readonly CategoriaValidator _validator;

    public CategoriaValidatorTests()
    {
        _validator = new CategoriaValidator();
    }

    [Fact]
    public void Deve_Passar_Quando_Categoria_For_Valida()
    {
        var categoria = new Categoria
        {
            Nome = "Alimentação",
            Tipo = "Despesa"
        };

        var result = _validator.TestValidate(categoria);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Deve_Retornar_Erro_Quando_Nome_Da_Categoria_For_Vazio(string nomeInvalido)
    {
        var categoria = new Categoria
        {
            Nome = nomeInvalido,
            Tipo = "Despesa"
        };

        var result = _validator.TestValidate(categoria);
        result.ShouldHaveValidationErrorFor(c => c.Nome);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("Outro")]
    public void Deve_Retornar_Erro_Quando_Tipo_Da_Categoria_For_Invalido(string tipoInvalido)
    {
        var categoria = new Categoria
        {
            Nome = "Salário",
            Tipo = tipoInvalido
        };

        var result = _validator.TestValidate(categoria);
        result.ShouldHaveValidationErrorFor(c => c.Tipo);
    }
}