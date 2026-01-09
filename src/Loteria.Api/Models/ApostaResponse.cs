namespace Loteria.Api.Models;

public sealed class ApostaResponse
{
    public Guid Id { get; set; }

    public bool Surpresinha { get; set; }

    public int? TeimosinhaQuantidade { get; set; }

    public int QuantidadeDezenas { get; set; }

    public string Status { get; set; } = string.Empty;

    public DateTime DataCriacao { get; set; }

    public IReadOnlyList<int> Dezenas { get; set; } = Array.Empty<int>();
}
