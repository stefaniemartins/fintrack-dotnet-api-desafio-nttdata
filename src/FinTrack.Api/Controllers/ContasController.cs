using FinTrack.Core.Entities;
using FinTrack.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinTrack.Api.Controllers;

[ApiController]
[Route("api/v1/contas")]
public class ContasController : ControllerBase
{
    private readonly AppDbContext _context;

    public ContasController(AppDbContext context)
    {
        _context = context;
    }

    // 1. Listar todas as contas
    [HttpGet]
    public async Task<IActionResult> ObterTodas()
    {
        var contas = await _context.Contas.AsNoTracking().ToListAsync();
        return Ok(contas);
    }

    // 2. Obter conta por ID
    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var conta = await _context.Contas
            .Include(c => c.Transacoes)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (conta == null)
            return NotFound(new { mensagem = "Conta não encontrada." });

        return Ok(conta);
    }

    // 3. Criar uma nova conta
    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] Conta conta)
    {
        if (string.IsNullOrWhiteSpace(conta.Nome))
            return BadRequest(new { mensagem = "O nome da conta é obrigatório." });

        _context.Contas.Add(conta);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(ObterPorId), new { id = conta.Id }, conta);
    }

    // 4. Atualizar uma conta existente
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] Conta contaDto)
    {
        var conta = await _context.Contas.FindAsync(id);
        if (conta == null)
            return NotFound(new { mensagem = "Conta não encontrada." });

        conta.Nome = contaDto.Nome;
        conta.SaldoInicial = contaDto.SaldoInicial;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // 5. Excluir uma conta
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Deletar(int id)
    {
        var conta = await _context.Contas
            .Include(c => c.Transacoes)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (conta == null)
            return NotFound(new { mensagem = "Conta não encontrada." });

        // Regra de segurança: Evitar excluir conta se houver transações vinculadas
        if (conta.Transacoes != null && conta.Transacoes.Any())
        {
            return BadRequest(new { mensagem = "Não é possível excluir uma conta que possui transações vinculadas." });
        }

        _context.Contas.Remove(conta);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}