using Loteria.Application.Interfaces;
using Loteria.Application.Models;
using Loteria.Infrastructure.Data;
using Loteria.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Loteria.Infrastructure.Repositories;

public sealed class ApostaRepository : IApostaRepository
{
    private readonly LoteriaDbContext _context;

    public ApostaRepository(LoteriaDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(ApostaDto aposta, CancellationToken cancellationToken)
    {
        var entity = new ApostaEntity
        {
            Id = aposta.Id,
            Surpresinha = aposta.Surpresinha,
            TeimosinhaQuantidade = aposta.TeimosinhaQuantidade,
            QuantidadeDezenas = aposta.QuantidadeDezenas,
            Status = (int)aposta.Status,
            DataCriacao = aposta.DataCriacao,
            Dezenas = aposta.Dezenas.Select(d => new ApostaDezenaEntity
            {
                ApostaId = aposta.Id,
                Dezena = d
            }).ToList()
        };

        _context.Apostas.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<ApostaDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.Apostas
            .AsNoTracking()
            .Include(a => a.Dezenas)
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

        if (entity is null)
        {
            return null;
        }

        return new ApostaDto
        {
            Id = entity.Id,
            Surpresinha = entity.Surpresinha,
            TeimosinhaQuantidade = entity.TeimosinhaQuantidade,
            QuantidadeDezenas = entity.QuantidadeDezenas,
            Status = (ApostaStatus)entity.Status,
            DataCriacao = entity.DataCriacao,
            Dezenas = entity.Dezenas.Select(d => d.Dezena).OrderBy(d => d).ToArray()
        };
    }
}
