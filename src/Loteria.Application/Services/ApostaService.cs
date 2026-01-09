using System.ComponentModel.DataAnnotations;
using Loteria.Application.Interfaces;
using Loteria.Application.Models;

namespace Loteria.Application.Services;

public sealed class ApostaService : IApostaService
{
    private static readonly int[] TeimosinhaValores = { 2, 3, 4, 6, 8, 9, 12 };
    private readonly IApostaRepository _apostaRepository;

    public ApostaService(IApostaRepository apostaRepository)
    {
        _apostaRepository = apostaRepository;
    }

    public async Task<ApostaDto> CriarAsync(CreateApostaRequestDto request, CancellationToken cancellationToken)
    {
        ValidateRequest(request);

        var dezenas = request.Surpresinha
            ? GerarDezenas(request.QuantidadeDezenas)
            : [.. request.Dezenas!];

        var aposta = new ApostaDto
        {
            Id = Guid.NewGuid(),
            Surpresinha = request.Surpresinha,
            TeimosinhaQuantidade = request.TeimosinhaQuantidade,
            QuantidadeDezenas = dezenas.Length,
            Status = ApostaStatus.Registrada,
            DataCriacao = DateTime.UtcNow,
            Dezenas = dezenas
        };

        await _apostaRepository.AddAsync(aposta, cancellationToken);
        return aposta;
    }

    public Task<ApostaDto?> ObterAsync(Guid id, CancellationToken cancellationToken)
    {
        return _apostaRepository.GetByIdAsync(id, cancellationToken);
    }

    private static void ValidateRequest(CreateApostaRequestDto request)
    {
        if (request.QuantidadeDezenas is < 6 or > 20)
        {
            throw new ValidationException("QuantidadeDezenas deve estar entre 6 e 20.");
        }

        if (request.TeimosinhaQuantidade is not null &&
            !TeimosinhaValores.Contains(request.TeimosinhaQuantidade.Value))
        {
            throw new ValidationException("TeimosinhaQuantidade deve ser 2, 3, 4, 6, 8, 9 ou 12.");
        }

        if (request.Surpresinha)
        {
            if (request.Dezenas is not null && request.Dezenas.Count > 0)
            {
                throw new ValidationException("Surpresinha não aceita dezenas informadas.");
            }
        }
        else
        {
            if (request.Dezenas is null || request.Dezenas.Count == 0)
            {
                throw new ValidationException("Dezenas são obrigatórias quando não é Surpresinha.");
            }

            if (request.Dezenas.Count != request.QuantidadeDezenas)
            {
                throw new ValidationException("QuantidadeDezenas não corresponde ao total de dezenas.");
            }

            var invalidas = request.Dezenas.Where(d => d is < 1 or > 60).ToArray();
            if (invalidas.Length > 0)
            {
                throw new ValidationException("Dezenas devem estar entre 1 e 60.");
            }

            var distintas = request.Dezenas.Distinct().Count();
            if (distintas != request.Dezenas.Count)
            {
                throw new ValidationException("Não é permitido repetir dezenas.");
            }
        }
    }

    private static int[] GerarDezenas(int quantidade)
    {
        var dezenas = new HashSet<int>();
        while (dezenas.Count < quantidade)
        {
            dezenas.Add(Random.Shared.Next(1, 61));
        }

        return dezenas.OrderBy(d => d).ToArray();
    }
}
