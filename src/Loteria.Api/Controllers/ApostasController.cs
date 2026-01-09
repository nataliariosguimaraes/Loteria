using System.ComponentModel.DataAnnotations;
using Loteria.Api.Models;
using Loteria.Application.Interfaces;
using Loteria.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Loteria.Api.Controllers;

[ApiController]
[Route("api/v1/apostas")]
public sealed class ApostasController : ControllerBase
{
    private readonly IApostaService _apostaService;

    public ApostasController(IApostaService apostaService)
    {
        _apostaService = apostaService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApostaResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApostaResponse>> Criar([FromBody] CreateApostaRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var dto = ToDto(request);
            var criada = await _apostaService.CriarAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(Obter), new { id = criada.Id }, ToResponse(criada));
        }
        catch (ValidationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApostaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApostaResponse>> Obter(Guid id, CancellationToken cancellationToken)
    {
        var aposta = await _apostaService.ObterAsync(id, cancellationToken);
        if (aposta is null)
        {
            return NotFound();
        }

        return Ok(ToResponse(aposta));
    }

    private static CreateApostaRequestDto ToDto(CreateApostaRequest request)
    {
        return new CreateApostaRequestDto
        {
            Surpresinha = request.Surpresinha,
            TeimosinhaQuantidade = request.TeimosinhaQuantidade,
            QuantidadeDezenas = request.QuantidadeDezenas,
            Dezenas = request.Dezenas
        };
    }

    private static ApostaResponse ToResponse(ApostaDto aposta)
    {
        return new ApostaResponse
        {
            Id = aposta.Id,
            Surpresinha = aposta.Surpresinha,
            TeimosinhaQuantidade = aposta.TeimosinhaQuantidade,
            QuantidadeDezenas = aposta.QuantidadeDezenas,
            Status = aposta.Status.ToString(),
            DataCriacao = aposta.DataCriacao,
            Dezenas = aposta.Dezenas
        };
    }
}
