using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Testing.Appointments.PageTests
{
    [Parallelizable(ParallelScope.Self)]
    public class AppointmentIndexTests : PageTest
    {
        [Test]
        public async Task Show_Appointment_Index_OnLoad()
        {
            await Page.GotoAsync($"{HelperClass.BaseUri}/Afspraak");
            await Page.WaitForSelectorAsync("data-test-id=doctors");
            var doctorList = await Page.QuerySelectorAsync("[data-test-id=doctors]");
            Assert.NotNull(doctorList);

            var doctorItems = await doctorList.QuerySelectorAllAsync("ul > li");
            Assert.IsTrue(doctorItems.Count > 0);
        }
    }
}
