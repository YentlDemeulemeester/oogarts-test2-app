using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.Users.Employees.PageTests
{
    [Parallelizable(ParallelScope.Self)]
    public class EmployeeDetailTests : PageTest
    {
        [Test]
        public async Task Show_Employee_Detail_OnLoad()
        {
            await Page.GotoAsync($"{HelperClass.BaseUri}/Team/1");
            await Page.WaitForSelectorAsync("data-test-id=employeeName");
            var employeeName = await Page.TextContentAsync("data-test-id=employeeName");
            Assert.IsNotEmpty(employeeName);
        }
    }
}
