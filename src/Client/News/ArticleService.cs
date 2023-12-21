using Client.Extensions;
using Shared.Articles;
using Shared.Articles;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Client.Blogs;

public class ArticleService : IArticleService
{
	private readonly HttpClient client;
	private const string endpoint = "api/Article";
	public ArticleService(HttpClient client)
	{
		this.client = client;
	}
	
	public async Task<ArticleResult.Index> GetIndexAsync(ArticleRequest.Index request)
	{
		var response = await client.GetFromJsonAsync<ArticleResult.Index>($"{endpoint}?{request.AsQueryString()}");
        return response!;
	}

    public async Task<ArticleResult.Create> CreateAsync(ArticleDto.Mutate model)
    {
        var response = await client.PostAsJsonAsync(endpoint, model);
        return await response.Content.ReadFromJsonAsync<ArticleResult.Create>();
    }

    public async Task EditAsync(long id, ArticleDto.Mutate edit)
    {
	   var response = await client.PutAsJsonAsync($"{endpoint}/{id}", edit);
 	   response.EnsureSuccessStatusCode();
    }

    public async Task DeleteAsync(long id)
    {
        var response = await client.DeleteAsync($"{endpoint}/{id}");
        response.EnsureSuccessStatusCode();
    }

    //public async Task DeleteAsync(long id)
    //{
    //    await client.DeleteAsync($"{endpoint}/{id}");
    //}

    //public async Task<ArticleDto.Detail> GetDetailAsync(long articleId)
    //{
    //    var response = await client.GetFromJsonAsync<ArticleDto.Detail>($"{endpoint}/{articleId}");
    //    return response;
    //}
}