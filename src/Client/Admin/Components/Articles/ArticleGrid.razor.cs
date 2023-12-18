using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen.Blazor;
using Shared.Articles;
using System.Security.Policy;

namespace Client.Admin.Components.Articles
{
    public partial class ArticleGrid
    {
        [Inject] IJSRuntime JSRuntime { get; set; }
        RadzenDataGrid<ArticleDto.Index> articleGrid;
        List<ArticleDto.Index>? articles;
        [Inject] public IArticleService ArticleService { get; set; } = default!;
        [Inject] public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            ArticleResult.Index res = await ArticleService.GetIndexAsync(new ArticleRequest.Index());
            articles = res.Articles.ToList();
        }

        async Task EditRow(ArticleDto.Index eyeCondition)
        {
            long eyeConditionId = eyeCondition.Id;
            await JSRuntime.InvokeVoidAsync("openInNewTab", $"/Nieuws/edit/{eyeConditionId}");
        }

        async Task DeleteRow(ArticleDto.Index article)
        {
            long articleId = article.Id;
            await ArticleService.DeleteAsync(articleId);
            articles.Remove(article);
            await articleGrid.Reload();
        }
        async Task CreateArticle()
        {
            await JSRuntime.InvokeVoidAsync("openInNewTab", "/Nieuws/nieuw");
        }

    }
}
