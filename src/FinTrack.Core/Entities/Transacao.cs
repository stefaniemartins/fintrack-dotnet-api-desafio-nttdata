namespace FinTrack.Core.Entities;

public class Transacao
{
    public int Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public DateTime Data { get; set; } = DateTime.UtcNow;
    public string Tipo { get; set; } = string.Empty; // "Receita" ou "Despesa"

    public int ContaId { get; set; }
    public Conta Conta { get; set; } = null!;

    public int CategoriaId { get; set; }
    public Categoria Categoria { get; set; } = null!;
}