using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.Articles.PageTests
{
    [Parallelizable(ParallelScope.Self)]
    public class ArticleIndexTests : PageTest
    {
        [Test]
        public async Task Show_ArticleList_OnLoad()
        {
            await Page.GotoAsync($"{HelperClass.BaseUri}/Nieuws");
            await Page.WaitForSelectorAsync("data-test-id=articles");
            var articleList = await Page.Locator("data-test-id=article").CountAsync();
            Assert.IsTrue(articleList > 0);
        }
    }
}
