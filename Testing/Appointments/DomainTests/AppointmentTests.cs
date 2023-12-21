using Domain.Appointments;
using Domain.EyeConditions;
using Domain.Users.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Testing.Appointments.DomainTests
{
	[TestFixture]
	public class AppointmentTests
	{
		public void Appointment_WhenInitializedWithValidParameters_ShouldSetProperties()
		{
			//Arrange
			Patient patient = new Patient("Lode", "Van Beneden", new DateOnly(2001, 08, 10), "+32468180882", "lode.lode65@gmail.com");
			Timeslot timeSlot = new Timeslot(new DateTime(2023, 11, 26, 8, 30, 0));
			string reason = "controle";
			string note = "niets";

			//Act
			Appointment newAppointment = new Appointment(patient, timeSlot, reason, note);

			//Assert
			Assert.AreEqual(patient, newAppointment.Patient);
			Assert.AreEqual(timeSlot, newAppointment.Timeslot);
			Assert.AreEqual(reason, newAppointment.Reason);
			Assert.AreEqual(note, newAppointment.Note);
		}
	}
}
