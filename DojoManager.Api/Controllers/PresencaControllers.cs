using DojoManager.Api.Data;
using DojoManager.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace DojoManager.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PresencasController : ControllerBase
{
    private readonly AppDbContext _context;

    public PresencasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<Presenca>> RegistrarPresenca(Presenca presenca)
    {
       
        var aluno = await _context.Alunos.FindAsync(presenca.AlunoId);
        var aula = await _context.Aulas.FindAsync(presenca.AulaId);

        if (aluno == null || aula == null)
            return BadRequest("Aluno ou Aula não encontrados.");

        _context.Presencas.Add(presenca);
        await _context.SaveChangesAsync();

        return Ok("Presença registrada com sucesso!");
    }
}