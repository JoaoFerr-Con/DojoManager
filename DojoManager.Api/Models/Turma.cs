namespace DojoManager.Api.Models;

public class Turma
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Nome { get; set; } = string.Empty;
    public Modalidade Modalidade { get; set; }
    public TimeSpan Horario { get; set; }
    public List<Aula> Aulas { get; set; } = new();
}