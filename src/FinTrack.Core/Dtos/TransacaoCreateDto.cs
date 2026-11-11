namespace FinTrack.Core.Dtos;

public class TransacaoCreateDto
{
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public DateTime Data { get; set; } = DateTime.UtcNow; // Adicionado aqui
    public string Tipo { get; set; } = string.Empty; // "Receita" ou "Despesa"
    public int ContaId { get; set; }
    public int CategoriaId { get; set; }
}