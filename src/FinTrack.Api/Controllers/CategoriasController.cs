using FinTrack.Core.Entities;
using FinTrack.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinTrack.Api.Controllers;

[ApiController]
[Route("api/v1/categorias")]
public class CategoriasController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoriasController(AppDbContext context)
    {
        _context = context;
    }

    // 1. Listar todas as categorias
    [HttpGet]
    public async Task<IActionResult> ObterTodas()
    {
        var categorias = await _context.Categorias.AsNoTracking().ToListAsync();
        return Ok(categorias);
    }

    // 2. Obter categoria por ID
    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var categoria = await _context.Categorias
            .Include(c => c.Transacoes)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (categoria == null)
            return NotFound(new { mensagem = "Categoria não encontrada." });

        return Ok(categoria);
    }

    // 3. Criar uma nova categoria
    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] Categoria categoria)
    {
        if (string.IsNullOrWhiteSpace(categoria.Nome))
            return BadRequest(new { mensagem = "O nome da categoria é obrigatório." });

        if (string.IsNullOrWhiteSpace(categoria.Tipo) || (categoria.Tipo != "Receita" && categoria.Tipo != "Despesa"))
            return BadRequest(new { mensagem = "O tipo da categoria deve ser 'Receita' ou 'Despesa'." });

        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(ObterPorId), new { id = categoria.Id }, categoria);
    }

    // 4. Atualizar uma categoria
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] Categoria categoriaDto)
    {
        var categoria = await _context.Categorias.FindAsync(id);
        if (categoria == null)
            return NotFound(new { mensagem = "Categoria não encontrada." });

        categoria.Nome = categoriaDto.Nome;
        categoria.Tipo = categoriaDto.Tipo;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // 5. Excluir uma categoria
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Deletar(int id)
    {
        var categoria = await _context.Categorias
            .Include(c => c.Transacoes)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (categoria == null)
            return NotFound(new { mensagem = "Categoria não encontrada." });

        if (categoria.Transacoes != null && categoria.Transacoes.Any())
        {
            return BadRequest(new { mensagem = "Não é possível excluir uma categoria que possui transações vinculadas." });
        }

        _context.Categorias.Remove(categoria);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}