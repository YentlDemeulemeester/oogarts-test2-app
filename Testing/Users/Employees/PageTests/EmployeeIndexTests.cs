using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.Users.Employees.PageTests
{
    [Parallelizable(ParallelScope.Self)]
    public class EmployeesIndexTests : PageTest
    {
        [Test]
        public async Task Show_Employees_Index_OnLoad()
        {
            await Page.GotoAsync($"{HelperClass.BaseUri}/Team");
            await Page.WaitForSelectorAsync("data-test-id=team");
            var amountOfTeamMembers = await Page.Locator("data-test-id=teammember").CountAsync();
            Assert.IsTrue(amountOfTeamMembers > 0);
        }
    }
}
