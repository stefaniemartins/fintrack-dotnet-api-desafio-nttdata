using FinTrack.Core.Entities;
using FinTrack.Core.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace FinTrack.Tests.Validators;

public class ContaValidatorTests
{
    private readonly ContaValidator _validator;

    public ContaValidatorTests()
    {
        _validator = new ContaValidator();
    }

    [Fact]
    public void Deve_Passar_Quando_Conta_For_Valida()
    {
        var conta = new Conta
        {
            Nome = "Nubank Principal",
            SaldoInicial = 1000.00m
        };

        var result = _validator.TestValidate(conta);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Deve_Retornar_Erro_Quando_Nome_Da_Conta_For_Vazio(string nomeInvalido)
    {
        var conta = new Conta
        {
            Nome = nomeInvalido,
            SaldoInicial = 500.00m
        };

        var result = _validator.TestValidate(conta);
        result.ShouldHaveValidationErrorFor(c => c.Nome);
    }

    [Fact]
    public void Deve_Retornar_Erro_Quando_Saldo_Inicial_For_Negativo()
    {
        var conta = new Conta
        {
            Nome = "Carteira",
            SaldoInicial = -50.00m
        };

        var result = _validator.TestValidate(conta);
        result.ShouldHaveValidationErrorFor(c => c.SaldoInicial);
    }
}