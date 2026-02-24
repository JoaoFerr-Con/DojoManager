using System.ComponentModel.DataAnnotations;

namespace DojoManager.Api.Models;

public class Aluno
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string Nome { get; set; } = string.Empty;

    [Required]
    public string Telefone { get; set; } = string.Empty;

    [Required]
    public DateTime DataNascimento { get; set; }

    [Required]
    public SexoAtleta Sexo { get; set; }

    [Required]
    public Modalidade Modalidade { get; set; } 
    
    public string FaixaAtual { get; set; } = "Branca";
    public int Graus { get; set; } = 0;
    public bool EstaAtivo { get; set; } = true;

    public int Idade => DateTime.Now.Year - DataNascimento.Year;
    public List<Graduacao> HistoricoGraduacoes { get; set; } = new();
    public List<Mensalidade> Mensalidades { get; set; } = new();
}