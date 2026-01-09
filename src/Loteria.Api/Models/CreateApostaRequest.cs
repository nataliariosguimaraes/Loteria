namespace Loteria.Api.Models;

public sealed class CreateApostaRequest
{
    public bool Surpresinha { get; set; }

    public int? TeimosinhaQuantidade { get; set; }

    
    public int QuantidadeDezenas { get; set; }

    public List<int>? Dezenas { get; set; }
}
