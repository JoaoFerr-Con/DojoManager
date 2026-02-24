using DojoManager.Api.Data;
using DojoManager.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DojoManager.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GraduacaoController : ControllerBase
{
    private readonly AppDbContext _context;

    public GraduacaoController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("progresso/{alunoId}")]
    public async Task<ActionResult> GetProgresso(Guid alunoId)
    {
        var aluno = await _context.Alunos.FindAsync(alunoId);
        if (aluno == null) return NotFound("Aluno não encontrado.");

        var totalPresencas = await _context.Presencas.CountAsync(p => p.AlunoId == alunoId);

        int metaAulas = 20; 
        int aulasRestantes = metaAulas - (totalPresencas % metaAulas);

        return Ok(new {
            Nome = aluno.Nome,
            AulasAssistidas = totalPresencas,
            AulasParaProximoGrau = aulasRestantes,
            ProgressoPercentual = (double)(totalPresencas % metaAulas) / metaAulas * 100
        });
    }

   
    [HttpPost("promover")]
    public async Task<ActionResult> PromoverAluno(Graduacao graduacao)
    {
        var aluno = await _context.Alunos.FindAsync(graduacao.AlunoId);
        if (aluno == null) return BadRequest("Aluno não encontrado.");

        // Atualiza o perfil oficial do aluno
        aluno.FaixaAtual = graduacao.FaixaConquistada;
        aluno.Graus = graduacao.GrauConquistado;

        _context.Graduacoes.Add(graduacao);
        await _context.SaveChangesAsync();

        return Ok($"Parabéns! {aluno.Nome} foi promovido para {graduacao.FaixaConquistada}.");
    }
}