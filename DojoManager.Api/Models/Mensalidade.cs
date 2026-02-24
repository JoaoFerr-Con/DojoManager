namespace DojoManager.Api.Models;

public class Mensalidade
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid AlunoId { get; set; }
    public decimal Valor { get; set; }
    public DateTime DataVencimento { get; set; }
    public DateTime? DataPagamento { get; set; } // Nullable, pois começa sem pagamento
    public StatusMensalidade Status { get; set; } = StatusMensalidade.Pendente;
    public Aluno? Aluno { get; set; }
    public void RealizarPagamento()
    {
        DataPagamento = DateTime.Now;
        Status = StatusMensalidade.Pago;
    }
}