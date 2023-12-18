using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Articles
{
    public interface IArticleService
    {
        Task<ArticleResult.Index> GetIndexAsync(ArticleRequest.Index request);
        Task<ArticleResult.Create> CreateAsync(ArticleDto.Mutate model);
        Task DeleteAsync(long id);
    }
}
