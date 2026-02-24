namespace DojoManager.Api.Models;

public class Aula
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid TurmaId { get; set; }
    public DateTime Data { get; set; }
    public Turma? Turma { get; set; }
    public List<Presenca> Presencas { get; set; } = new();
}