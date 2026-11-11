using FinTrack.Core.Entities;
using FluentValidation;

namespace FinTrack.Core.Validators;

public class CategoriaValidator : AbstractValidator<Categoria>
{
    public CategoriaValidator()
    {
        RuleFor(c => c.Nome)
            .NotEmpty().WithMessage("O nome da categoria é obrigatório.")
            .MaximumLength(50).WithMessage("O nome deve ter no máximo 50 caracteres.");

        RuleFor(c => c.Tipo)
            .NotEmpty().WithMessage("O tipo é obrigatório.")
            .Must(t => t == "Receita" || t == "Despesa")
            .WithMessage("O tipo deve ser 'Receita' ou 'Despesa'.");
    }
}