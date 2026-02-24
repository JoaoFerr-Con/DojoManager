using DojoManager.Api.Data;
using DojoManager.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DojoManager.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AulasController : ControllerBase
{
    private readonly AppDbContext _context;

    public AulasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<Aula>> PostAula(Aula aula)
    {
        _context.Aulas.Add(aula);
        await _context.SaveChangesAsync();
        return Ok(aula);
    }
}