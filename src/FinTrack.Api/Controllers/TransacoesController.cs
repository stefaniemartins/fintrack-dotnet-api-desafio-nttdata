using FinTrack.Core.Dtos;
using FinTrack.Core.Entities;
using FinTrack.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinTrack.Api.Controllers;

[ApiController]
[Route("api/v1/transacoes")]
public class TransacoesController : ControllerBase
{
    private readonly AppDbContext _context;

    public TransacoesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodas()
    {
        var transacoes = await _context.Transacoes
            .AsNoTracking()
            .Include(t => t.Conta)
            .Include(t => t.Categoria)
            .ToListAsync();

        return Ok(transacoes);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var transacao = await _context.Transacoes
            .Include(t => t.Conta)
            .Include(t => t.Categoria)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (transacao == null)
            return NotFound(new { mensagem = "Transação não encontrada." });

        return Ok(transacao);
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] TransacaoCreateDto dto)
    {
        var contaExists = await _context.Contas.AnyAsync(c => c.Id == dto.ContaId);
        if (!contaExists)
            return BadRequest(new { mensagem = "A conta informada não existe." });

        var categoriaExists = await _context.Categorias.AnyAsync(c => c.Id == dto.CategoriaId);
        if (!categoriaExists)
            return BadRequest(new { mensagem = "A categoria informada não existe." });

        var transacao = new Transacao
        {
            Descricao = dto.Descricao,
            Valor = dto.Valor,
            Data = dto.Data == default ? DateTime.UtcNow : dto.Data,
            Tipo = dto.Tipo,
            ContaId = dto.ContaId,
            CategoriaId = dto.CategoriaId
        };

        _context.Transacoes.Add(transacao);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(ObterPorId), new { id = transacao.Id }, transacao);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Deletar(int id)
    {
        var transacao = await _context.Transacoes.FindAsync(id);
        if (transacao == null)
            return NotFound(new { mensagem = "Transação não encontrada." });

        _context.Transacoes.Remove(transacao);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}