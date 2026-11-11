namespace FinTrack.Core.Dtos;

public class CategoriaCreateDto
{
    public string Nome { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty; // "Receita" ou "Despesa"
}