using FinTrack.Core.Entities;
using FluentValidation;

namespace FinTrack.Core.Validators;

public class ContaValidator : AbstractValidator<Conta>
{
    public ContaValidator()
    {
        RuleFor(c => c.Nome)
            .NotEmpty().WithMessage("O nome da conta é obrigatório.")
            .MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres.");

        RuleFor(c => c.SaldoInicial)
            .GreaterThanOrEqualTo(0).WithMessage("O saldo inicial não pode ser negativo.");
    }
}