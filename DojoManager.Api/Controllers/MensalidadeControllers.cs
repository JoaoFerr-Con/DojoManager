using DojoManager.Api.Data;
using DojoManager.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DojoManager.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MensalidadesController : ControllerBase
{
    private readonly AppDbContext _context;

    public MensalidadesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("gerar")]
    public async Task<ActionResult<Mensalidade>> GerarMensalidade(Mensalidade mensalidade)
    {
        var alunoExistente = await _context.Alunos.AnyAsync(a => a.Id == mensalidade.AlunoId);
        if (!alunoExistente) return BadRequest("Aluno não encontrado.");

        _context.Mensalidades.Add(mensalidade);
        await _context.SaveChangesAsync();

        return Ok("Mensalidade gerada com sucesso!");
    }

    
    [HttpPut("pagar/{id}")]
    public async Task<IActionResult> Pagar(Guid id)
    {
        var mensalidade = await _context.Mensalidades.FindAsync(id);

        if (mensalidade == null) return NotFound("Mensalidade não encontrada.");

       
        mensalidade.RealizarPagamento();

        await _context.SaveChangesAsync();

        return Ok($"Pagamento de R$ {mensalidade.Valor} confirmado!");
    }

    
    [HttpGet("aluno/{alunoId}")]
    public async Task<ActionResult<IEnumerable<Mensalidade>>> GetMensalidadesPorAluno(Guid alunoId)
    {
        var mensalidades = await _context.Mensalidades
            .Where(m => m.AlunoId == alunoId)
            .OrderByDescending(m => m.DataVencimento)
            .ToListAsync();

        return Ok(mensalidades);
    }

    [HttpGet]
public async Task<ActionResult<IEnumerable<object>>> GetTodasMensalidades()
{
    // Retornamos um objeto anônimo para incluir o Nome do Aluno sem complicação
    var mensalidades = await _context.Mensalidades
        .Include(m => m.Aluno)
        .OrderByDescending(m => m.DataVencimento)
        .Select(m => new {
            m.Id,
            NomeAluno = m.Aluno!.Nome,
            m.Valor,
            m.DataVencimento,
            m.Status,
            m.AlunoId
        })
        .ToListAsync();

    return Ok(mensalidades);
}}