using System.ComponentModel.DataAnnotations;

namespace DojoManager.Api.Models;

public class Usuario
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string SenhaHash { get; set; } = string.Empty;

    public string Nome { get; set; } = "Mestre Admin";
}