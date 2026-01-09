using System.ComponentModel.DataAnnotations;
using Loteria.Application.Interfaces;
using Loteria.Application.Models;
using Loteria.Application.Services;
using Xunit;

namespace Loteria.UnitTests.Services;

public sealed class ApostaServiceTests
{
    [Fact]
    public async Task CriarAsync_DezenasManuais_ValidaECria()
    {
        var repository = new FakeApostaRepository();
        var service = new ApostaService(repository);
        var request = new CreateApostaRequestDto
        {
            Surpresinha = false,
            QuantidadeDezenas = 6,
            TeimosinhaQuantidade = 2,
            Dezenas = new[] { 1, 2, 3, 4, 5, 6 }
        };

        var aposta = await service.CriarAsync(request, CancellationToken.None);

        Assert.Equal(6, aposta.QuantidadeDezenas);
        Assert.False(aposta.Surpresinha);
        Assert.Equal(ApostaStatus.Registrada, aposta.Status);
        Assert.Equal(new[] { 1, 2, 3, 4, 5, 6 }, aposta.Dezenas);
        Assert.Equal(aposta.Id, repository.LastSaved?.Id);
    }

    [Fact]
    public async Task CriarAsync_Surpresinha_GeraDezenasUnicas()
    {
        var repository = new FakeApostaRepository();
        var service = new ApostaService(repository);
        var request = new CreateApostaRequestDto
        {
            Surpresinha = true,
            QuantidadeDezenas = 8,
            Dezenas = null
        };

        var aposta = await service.CriarAsync(request, CancellationToken.None);

        Assert.Equal(8, aposta.Dezenas.Count);
        Assert.Equal(8, aposta.Dezenas.Distinct().Count());
        Assert.All(aposta.Dezenas, dezena => Assert.InRange(dezena, 1, 60));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(5)]
    [InlineData(21)]
    public async Task CriarAsync_QuantidadeDezenasInvalida_LancaErro(int quantidade)
    {
        var repository = new FakeApostaRepository();
        var service = new ApostaService(repository);
        var request = new CreateApostaRequestDto
        {
            Surpresinha = true,
            QuantidadeDezenas = quantidade
        };

        await Assert.ThrowsAsync<ValidationException>(() => service.CriarAsync(request, CancellationToken.None));
    }

    [Fact]
    public async Task CriarAsync_TeimosinhaInvalida_LancaErro()
    {
        var repository = new FakeApostaRepository();
        var service = new ApostaService(repository);
        var request = new CreateApostaRequestDto
        {
            Surpresinha = true,
            QuantidadeDezenas = 6,
            TeimosinhaQuantidade = 5
        };

        await Assert.ThrowsAsync<ValidationException>(() => service.CriarAsync(request, CancellationToken.None));
    }

    [Fact]
    public async Task CriarAsync_SurpresinhaNaoAceitaDezenasInformadas()
    {
        var repository = new FakeApostaRepository();
        var service = new ApostaService(repository);
        var request = new CreateApostaRequestDto
        {
            Surpresinha = true,
            QuantidadeDezenas = 6,
            Dezenas = new[] { 1, 2, 3, 4, 5, 6 }
        };

        await Assert.ThrowsAsync<ValidationException>(() => service.CriarAsync(request, CancellationToken.None));
    }

    [Fact]
    public async Task CriarAsync_DezenasDuplicadas_LancaErro()
    {
        var repository = new FakeApostaRepository();
        var service = new ApostaService(repository);
        var request = new CreateApostaRequestDto
        {
            Surpresinha = false,
            QuantidadeDezenas = 6,
            Dezenas = new[] { 1, 2, 2, 4, 5, 6 }
        };

        await Assert.ThrowsAsync<ValidationException>(() => service.CriarAsync(request, CancellationToken.None));
    }

    [Fact]
    public async Task CriarAsync_DezenasForaDoIntervalo_LancaErro()
    {
        var repository = new FakeApostaRepository();
        var service = new ApostaService(repository);
        var request = new CreateApostaRequestDto
        {
            Surpresinha = false,
            QuantidadeDezenas = 6,
            Dezenas = new[] { 0, 2, 3, 4, 5, 6 }
        };

        await Assert.ThrowsAsync<ValidationException>(() => service.CriarAsync(request, CancellationToken.None));
    }

    [Fact]
    public async Task CriarAsync_QuantidadeDezenasNaoConfere_LancaErro()
    {
        var repository = new FakeApostaRepository();
        var service = new ApostaService(repository);
        var request = new CreateApostaRequestDto
        {
            Surpresinha = false,
            QuantidadeDezenas = 7,
            Dezenas = new[] { 1, 2, 3, 4, 5, 6 }
        };

        await Assert.ThrowsAsync<ValidationException>(() => service.CriarAsync(request, CancellationToken.None));
    }

    private sealed class FakeApostaRepository : IApostaRepository
    {
        public ApostaDto? LastSaved { get; private set; }

        public Task AddAsync(ApostaDto aposta, CancellationToken cancellationToken)
        {
            LastSaved = aposta;
            return Task.CompletedTask;
        }

        public Task<ApostaDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return Task.FromResult<ApostaDto?>(null);
        }
    }
}
