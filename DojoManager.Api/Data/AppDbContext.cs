using DojoManager.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace DojoManager.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Estas propriedades vão virar as tabelas no banco de dados
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Mensalidade> Mensalidades { get; set; }
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<Aula> Aulas { get; set; }
        public DbSet<Presenca> Presencas { get; set; }
        public DbSet<Graduacao> Graduacoes { get; set; }

       protected override void OnModelCreating(ModelBuilder modelBuilder)
{

    modelBuilder.Entity<Mensalidade>()
        .Property(m => m.Valor)
        .HasColumnType("decimal(18,2)");

   modelBuilder.Entity<Graduacao>()
        .HasOne(g => g.Aluno) // Usa a propriedade 'Aluno' no model Graduacao
        .WithMany(a => a.HistoricoGraduacoes) // Usa a lista 'HistoricoGraduacoes' no model Aluno
        .HasForeignKey(g => g.AlunoId) // Define que a chave é o 'AlunoId' que você já tem
        .OnDelete(DeleteBehavior.Cascade); // Mantém o seu Cascade Delete
        
    base.OnModelCreating(modelBuilder);
}
    }
}