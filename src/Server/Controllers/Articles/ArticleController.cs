using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Articles;
using Swashbuckle.AspNetCore.Annotations;

namespace Server.Controllers.Articles
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize(Roles = "Administrator")]
    public class ArticleController : Controller
    {
        private readonly IArticleService articleService;

        public ArticleController(IArticleService articleService)
        {
            this.articleService = articleService;
        }
        [SwaggerOperation("Returns a list of articles.")]
        [HttpGet, AllowAnonymous]
        public async Task<ArticleResult.Index> GetIndex([FromQuery] ArticleRequest.Index request)
        {
            return await articleService.GetIndexAsync(request);
        }

        [SwaggerOperation("Creates an article.")]
        [HttpPost]
        public async Task<IActionResult> Create(ArticleDto.Mutate model)
        {
            var creationId = await articleService.CreateAsync(model);
            return CreatedAtAction(nameof(Create), creationId);
        }

    }
}
