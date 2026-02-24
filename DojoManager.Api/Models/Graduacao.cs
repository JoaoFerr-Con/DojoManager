using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DojoManager.Api.Models;

public class Graduacao
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid AlunoId { get; set; }
    [ForeignKey("AlunoId")]
    public Aluno? Aluno { get; set; }

    [Required]
    [MaxLength(30)] 
    public string FaixaConquistada { get; set; } = string.Empty;

    [Required]
    [Range(0, 4, ErrorMessage = "No Jiu-jitsu, os graus vão de 0 a 4.")]
    public int GrauConquistado { get; set; }

    [Required]
    public DateTime DataConquista { get; set; } = DateTime.Now;
    public string? Observacao { get; set; }
}