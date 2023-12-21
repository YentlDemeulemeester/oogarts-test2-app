using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.EyeConditions.PageTests
{
    [Parallelizable(ParallelScope.Self)]
    public class EyeConditionIndexTests : PageTest
    {
/*        [Test]
        public async Task Show_EyeConditions_Index_OnLoad()
        {
            await Page.GotoAsync($"{HelperClass.BaseUri}/Oogaandoeningen");
            await Page.WaitForSelectorAsync("data-test-id=eyeconditions");
            var amountOfEyeConditions = await Page.Locator("data-test-id=eyecondition").CountAsync();
            Assert.IsTrue(amountOfEyeConditions > 0);
        }*/
    }
}
