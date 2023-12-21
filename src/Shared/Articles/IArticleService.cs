

namespace Shared.Articles
{
    public interface IArticleService
    {
        Task<ArticleResult.Index> GetIndexAsync(ArticleRequest.Index request);
        Task<ArticleResult.Create> CreateAsync(ArticleDto.Mutate model);
        Task<ArticleDto.Detail> GetDetailAsync(long Id);
        Task DeleteAsync(long id);
    }
}
