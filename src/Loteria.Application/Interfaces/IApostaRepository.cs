using Loteria.Application.Models;

namespace Loteria.Application.Interfaces;

public interface IApostaRepository
{
    Task AddAsync(ApostaDto aposta, CancellationToken cancellationToken);

    Task<ApostaDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
