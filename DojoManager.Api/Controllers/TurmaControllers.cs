using DojoManager.Api.Data;
using DojoManager.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DojoManager.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TurmasController : ControllerBase
{
    private readonly AppDbContext _context;

    public TurmasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Turma>>> GetTurmas()
    {
        return await _context.Turmas.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Turma>> PostTurma(Turma turma)
    {
        _context.Turmas.Add(turma);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTurmas), new { id = turma.Id }, turma);
    }
}