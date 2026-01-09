namespace Loteria.Infrastructure.Entities;

public sealed class ApostaEntity
{
    public Guid Id { get; set; }

    public bool Surpresinha { get; set; }

    public int? TeimosinhaQuantidade { get; set; }

    public int QuantidadeDezenas { get; set; }

    public int Status { get; set; }

    public DateTime DataCriacao { get; set; }

    public List<ApostaDezenaEntity> Dezenas { get; set; } = new();
}
