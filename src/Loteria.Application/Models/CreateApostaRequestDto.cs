namespace Loteria.Application.Models;

public sealed class CreateApostaRequestDto
{
    public bool Surpresinha { get; set; }

    public int? TeimosinhaQuantidade { get; set; }

    public int QuantidadeDezenas { get; set; }

    public IReadOnlyList<int>? Dezenas { get; set; }
}
