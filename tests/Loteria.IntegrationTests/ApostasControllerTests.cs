using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Loteria.IntegrationTests;

public sealed class ApostasControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ApostasControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task PostAposta_Manual_DeveCriar()
    {
        var payload = new
        {
            surpresinha = false,
            quantidadeDezenas = 6,
            teimosinhaQuantidade = 2,
            dezenas = new[] { 1, 2, 3, 4, 5, 6 }
        };

        var response = await _client.PostAsJsonAsync("/api/v1/apostas", payload);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task PostAposta_Surpresinha_DeveCriar()
    {
        var payload = new
        {
            surpresinha = true,
            quantidadeDezenas = 8
        };

        var response = await _client.PostAsJsonAsync("/api/v1/apostas", payload);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task PostAposta_DezenasDuplicadas_DeveRetornarBadRequest()
    {
        var payload = new
        {
            surpresinha = false,
            quantidadeDezenas = 6,
            dezenas = new[] { 1, 2, 2, 4, 5, 6 }
        };

        var response = await _client.PostAsJsonAsync("/api/v1/apostas", payload);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
