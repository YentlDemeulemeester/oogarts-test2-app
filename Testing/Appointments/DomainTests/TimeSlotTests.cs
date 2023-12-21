using Domain.Appointments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.Appointments.DomainTests
{
	[TestFixture]
	public class TimeSlotTests
	{
		[Test]
		public void TimeSlot_WhenInitializedWithValidParametersNoCustomTimeSpan_ShouldSetProperties()
		{
			//Arrange
			DateTime dateTime = new DateTime(2023, 12, 20, 10, 15, 0);
			TimeSpan defaultTimeSpan = TimeSpan.FromMinutes(15);

			//Act
			Timeslot newTimeSlot = new Timeslot(dateTime);

			//Assert
			Assert.AreEqual(dateTime, newTimeSlot.Time);
			Assert.AreEqual(defaultTimeSpan, newTimeSlot.Duration);
		}

		[Test]
		public void TimeSlot_WhenInitializedWithValidParametersCustomTimeSpan_ShouldSetProperties()
		{
			//Arrange
			DateTime dateTime = new DateTime(2023, 12, 20, 10, 15, 0);
			TimeSpan timeSpan = TimeSpan.FromMinutes(30);

			//Act
			Timeslot newTimeSlot = new Timeslot(dateTime, timeSpan);

			//Assert
			Assert.AreEqual(dateTime, newTimeSlot.Time);
			Assert.AreEqual(timeSpan, newTimeSlot.Duration);
		}


	}
}
