using Domain.Files;
using Microsoft.EntityFrameworkCore;
using Domain.Articles;
using Domain.EyeConditions;
using Persistence;
using Shared.EyeConditions;
using Services.Files;
using Shared.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Services.Articles
{
    public class ArticleService : IArticleService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IStorageService storageService;


        public ArticleService(ApplicationDbContext context, IStorageService storageService)
        {
            dbContext = context;
            this.storageService = storageService;
        }

        public async Task<ArticleResult.Create> CreateAsync(ArticleDto.Mutate model)
        {
            if (await dbContext.Articles.AnyAsync(x => x.Title == model.Title))
                throw new EntityAlreadyExistsException(nameof(Article), nameof(Article.Title), model.Title);

            /*Image image = new Image(storageService.BasePath, model.ImageContentType!);*/

            Article article = new(model.Title, model.Description, model.Content,  /*image.FileUri.ToString()*/ "papapa");

            dbContext.Articles.Add(article);
            await dbContext.SaveChangesAsync();

            /*Uri uploadSas = storageService.GenerateImageUploadSas(image);*/

            ArticleResult.Create result = new()
            {
                Id = article.Id,
                Title = article.Title,
                Description = article.Description,
                Content = article.Content,
                UploadUri = article.ImageUrl
            };

            return result;
        }

        public async Task DeleteAsync(long id)
        {
            Article? article = await dbContext.Articles.SingleOrDefaultAsync(x => x.Id == id);

            if (article is null)
                throw new EntityNotFoundException(nameof(EyeCondition), id);

            dbContext.Articles.Remove(article);

            await dbContext.SaveChangesAsync();
        }

        public async Task<ArticleResult.Index> GetIndexAsync(ArticleRequest.Index request)
        {
            var searchTerm = request.Searchterm != null ? request.Searchterm.ToLowerInvariant() : null;

            var query = dbContext.Articles.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(x => x.Title.ToLower().Contains(searchTerm));
            }

            int totalAmount = await query.CountAsync();

            var items = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .OrderBy(x => x.Id)
                .Select(x => new ArticleDto.Index
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl
                })
                .ToListAsync();

            var result = new ArticleResult.Index
            {
                Articles = items,
                TotalAmount = totalAmount
            };

            return result;
        }
    }
}
