namespace DojoManager.Api.Models;

public class Presenca
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid AulaId { get; set; }
    public Guid AlunoId { get; set; }

    public Aula? Aula { get; set; }
    public Aluno? Aluno { get; set; }
}