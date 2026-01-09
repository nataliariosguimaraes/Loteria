using Loteria.Application.Models;

namespace Loteria.Application.Interfaces;

public interface IApostaService
{
    Task<ApostaDto> CriarAsync(CreateApostaRequestDto request, CancellationToken cancellationToken);

    Task<ApostaDto?> ObterAsync(Guid id, CancellationToken cancellationToken);
}
