namespace Loteria.Application.Models;

public sealed class ApostaDto
{
    public Guid Id { get; set; }

    public bool Surpresinha { get; set; }

    public int? TeimosinhaQuantidade { get; set; }

    public int QuantidadeDezenas { get; set; }

    public ApostaStatus Status { get; set; }

    public DateTime DataCriacao { get; set; }

    public IReadOnlyList<int> Dezenas { get; set; } = Array.Empty<int>();
}
