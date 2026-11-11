using FinTrack.Core.Dtos;
using FluentValidation;

namespace FinTrack.Core.Validators;

public class TransacaoCreateValidator : AbstractValidator<TransacaoCreateDto>
{
    public TransacaoCreateValidator()
    {
        RuleFor(x => x.Descricao)
            .NotEmpty().WithMessage("A descrição é obrigatória.")
            .MaximumLength(200).WithMessage("A descrição não pode ter mais de 200 caracteres.");

        RuleFor(x => x.Valor)
            .GreaterThan(0).WithMessage("O valor da transação deve ser maior que zero.");

        RuleFor(x => x.Tipo)
            .NotEmpty().WithMessage("O tipo é obrigatório.")
            .Must(t => t == "Receita" || t == "Despesa")
            .WithMessage("O tipo deve ser 'Receita' ou 'Despesa'.");

        RuleFor(x => x.ContaId)
            .GreaterThan(0).WithMessage("Informe uma conta válida.");

        RuleFor(x => x.CategoriaId)
            .GreaterThan(0).WithMessage("Informe uma categoria válida.");
    }
}