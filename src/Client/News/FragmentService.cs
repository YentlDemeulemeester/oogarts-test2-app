using Client.Extensions;
using Shared.Articles.Fragments;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Client.Blogs;

public class FragmentService : IFragmentService
{
	private readonly HttpClient client;
	private const string endpoint = "api/Fragment";
	public FragmentService(HttpClient client)
	{
		this.client = client;
	}
	
	public async Task<FragmentResult.Index> GetIndexAsync(FragmentRequest.Index request)
	{
		var response = await client.GetFromJsonAsync<FragmentResult.Index>($"{endpoint}?{request.AsQueryString()}");
        return response!;
	}

    public async Task<FragmentResult.Create> CreateAsync(FragmentDto.Mutate model)
    {
        var response = await client.PostAsJsonAsync(endpoint, model);
        return await response.Content.ReadFromJsonAsync<FragmentResult.Create>();
    }

    public async Task EditAsync(long id, FragmentDto.Mutate edit)
    {
	   var response = await client.PutAsJsonAsync($"{endpoint}/{id}", edit);
 	   response.EnsureSuccessStatusCode();
    }

    public async Task DeleteAsync(long id)
    {
        await client.DeleteAsync($"{endpoint}/{id}");
    }

    public async Task<FragmentDto.Detail> GetDetailAsync(long fragmentId)
    {
        var response = await client.GetFromJsonAsync<FragmentDto.Detail>($"{endpoint}/{fragmentId}");
        return response;
    }
}