using Oogarts.Client.Extensions;
using Shared.Articles;
using System.Net.Http.Json;

namespace Client.Articles
{
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
    }
}
