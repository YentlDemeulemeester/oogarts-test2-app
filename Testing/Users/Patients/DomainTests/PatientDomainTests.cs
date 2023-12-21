using Domain.Users.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.Users.Patients.DomainTests
{
	[TestFixture]
	public class PatientDomainTests
	{
		[Test]
		public void Patient_WhenInitializedWithValidParameters_ShouldSetProperties()
		{
			//Arrange
			string firstName = "Lode";
			string lastName = "Van Beneden";
			DateOnly birthDate = new DateOnly(2001, 08, 10);
			string phone = "+324681808820";
			string email = "lode.lode65@gmail.com";

			//Act
			Patient patient = new Patient(firstName, lastName, birthDate, phone, email);

			//Assert
			Assert.AreEqual(firstName, patient.FirstName);
			Assert.AreEqual(lastName, patient.LastName);
			Assert.AreEqual(birthDate, patient.BirthDate);
			Assert.AreEqual(phone, patient.PhoneNumber);
			Assert.AreEqual(email, patient.Email);
		}
	}
}
