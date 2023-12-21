using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.EyeConditions.PageTests
{
    [Parallelizable(ParallelScope.Self)]
    public class EyeConditionDetailTests : PageTest
    {
        [Test]
        public async Task Show_Eyecondition_Detail_OnLoad()
        {
            await Page.GotoAsync($"{HelperClass.BaseUri}/Oogaandoening/1");
            await Page.WaitForSelectorAsync("data-test-id=eyeconditionName");
            var eyeconditioName = await Page.TextContentAsync("data-test-id=eyeconditionName");
            Assert.IsNotEmpty(eyeconditioName);
        }
    }
}
