using Domain.EyeConditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.Symptoms.DomainTests
{
	[TestFixture]
	public class SymptomTests
	{
		[Test]
		public void Symptom_WhenInitializedWithValidParameters_ShouldSetProperties()
		{
			//Arrange
			string symptomName = "Symptoom";
			
			//Act
			Symptom newSymptom = new Symptom(symptomName);

			//Assert
			Assert.AreEqual(symptomName, newSymptom.Name);
		}
	}
}
