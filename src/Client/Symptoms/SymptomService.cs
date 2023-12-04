using Oogarts.Client.Extensions;
using Oogarts.Shared.EyeConditions;
using Oogarts.Shared.EyeConditions;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Client.Symptoms;

public class SymptomService : ISymptomService
{
    private readonly HttpClient client;
    private const string endpoint = "api/symptom";

    public SymptomService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<SymptomResult.Index> GetIndexAsync(SymptomRequest.Index request)
    {
        var response = await client.GetFromJsonAsync<SymptomResult.Index>($"{endpoint}?{request.AsQueryString()}");
        return response!;
    }

    public async Task<SymptomResult.Create> CreateAsync(SymptomDto.Mutate model)
    {
        var response = await client.PostAsJsonAsync(endpoint, model);
        return await response.Content.ReadFromJsonAsync<SymptomResult.Create>();
    }
}