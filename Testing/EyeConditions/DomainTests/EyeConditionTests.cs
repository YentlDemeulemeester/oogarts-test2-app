using Domain.EyeConditions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.EyeConditions.DomainTests
{
    [TestFixture]
    public class EyeConditionTests
    {
        [Test]
        public void EyeCondition_WhenInitializedWithValidParameters_ShouldSetProperties()
        {
            // Arrange
            string name = "Cataract";
            string description = "Cataractische oog";
            string body = "Cataract komt voor wanneer...";
            string imageUrl = "http://afbeelding.com/image";
            string brochureUrl = "http://brochure.com/url";

            // Act
            EyeCondition eyeCondition = new EyeCondition(name, description, body, imageUrl, brochureUrl);

            // Assert
            Assert.AreEqual(name, eyeCondition.Name);
            Assert.AreEqual(description, eyeCondition.Description);
            Assert.AreEqual(body, eyeCondition.Body);
            Assert.AreEqual(imageUrl, eyeCondition.ImageUrl);
            Assert.AreEqual(brochureUrl, eyeCondition.BrochureUrl);
        }

		[Test]
		public void EyeCondition_AddSymptom_ShouldAddSymptomToEyeCondition()
		{
            // Arrange
            EyeCondition eyeCondition = new EyeCondition("Cataract", "Cataractische oog", "Cataract komt voor wanneer...", "http://afbeelding.com/image", "http://brochure.com/url");

			Symptom symptom = new Symptom("Rode ogen");

			// Act
			eyeCondition.Symptom(symptom);

			// Assert
			Assert.IsTrue(eyeCondition.Symptoms.Contains(symptom));
		}

		[Test]
		public void EyeCondition_RemoveSymptom_ShouldRemoveSymptomFromEyeCondition()
		{
			// Arrange
			EyeCondition eyeCondition = new EyeCondition("Cataract", "Cataractische oog", "Cataract komt voor wanneer...", "http://afbeelding.com/image", "http://brochure.com/url");
			Symptom symptom = new Symptom("Rode ogen");
			eyeCondition.Symptom(symptom);

			// Act
			eyeCondition.RemoveSymptom(symptom);

			// Assert
			Assert.IsFalse(eyeCondition.Symptoms.Contains(symptom));
		}

	}
}
