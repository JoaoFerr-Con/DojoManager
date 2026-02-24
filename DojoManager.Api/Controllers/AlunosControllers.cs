using DojoManager.Api.Data;
using DojoManager.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace DojoManager.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class AlunosController : ControllerBase
{
    private readonly AppDbContext _context;

    public AlunosController(AppDbContext context)
    {
        _context = context;
    }
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Aluno>>> GetAlunos()
    {
        return await _context.Alunos.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Aluno>> PostAluno(Aluno aluno)
    {
        _context.Alunos.Add(aluno);

        var cobrancaInicial = new Mensalidade
        {
            AlunoId = aluno.Id,
            Valor = 150.00m,
            DataVencimento = DateTime.Now.AddDays(30),
            Status = StatusMensalidade.Pendente
        };

        _context.Mensalidades.Add(cobrancaInicial);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetAlunos", new { id = aluno.Id }, aluno);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAluno(Guid id)
    {
        var aluno = await _context.Alunos.FindAsync(id);
        if (aluno == null) return NotFound();

        _context.Alunos.Remove(aluno);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> ToggleStatus(Guid id)
    {
        var aluno = await _context.Alunos.FindAsync(id);
        if (aluno == null) return NotFound(new { message = "Atleta não encontrado." });

        aluno.EstaAtivo = !aluno.EstaAtivo;
        await _context.SaveChangesAsync();

        return Ok(new { novoStatus = aluno.EstaAtivo });
    }

   
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAluno(Guid id, Aluno alunoEditado)
    {
        if (id != alunoEditado.Id) return BadRequest("IDs divergentes.");

        var existente = await _context.Alunos.FindAsync(id);
        if (existente == null) return NotFound("Atleta não encontrado.");

       
        existente.Nome = alunoEditado.Nome;
        existente.Telefone = alunoEditado.Telefone;
        existente.Sexo = alunoEditado.Sexo;
        existente.Modalidade = alunoEditado.Modalidade;
        existente.DataNascimento = alunoEditado.DataNascimento;

        await _context.SaveChangesAsync();
        return NoContent();
    }
} 