namespace Loteria.Infrastructure.Entities;

public sealed class ApostaDezenaEntity
{
    public Guid ApostaId { get; set; }

    public int Dezena { get; set; }

    public ApostaEntity? Aposta { get; set; }
}
