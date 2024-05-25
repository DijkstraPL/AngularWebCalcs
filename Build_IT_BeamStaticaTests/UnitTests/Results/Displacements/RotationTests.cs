using Build_IT_BeamStatica.Results.Displacements;
using NUnit.Framework;

namespace Build_IT_BeamStaticaTests.UnitTests.Results.Displacements
{
    [TestFixture]
    public class RotationTests
    {
        [Test]
        public void RotationTest_Construction_Success()
        {
            var rotation = new Rotation(15);

            Assert.That(rotation.Position, Is.EqualTo(15));
        }

        [Test]
        public void RotationTest_ConstructionWithNullPosition_Success()
        {
            var rotation = new Rotation();

            Assert.That(rotation.Position, Is.Null);
        }


        [Test]
        public void RotationTest_SetValue_Success()
        {
            var rotation = new Rotation();
            rotation.Value = 5;

            Assert.That(rotation.Value, Is.EqualTo(5));
        }

        [Test]
        public void RotationTest_ToString_Success()
        {
            var rotation = new Rotation();
            rotation.Value = 5;

            Assert.That(rotation.Value.ToString(), Is.EqualTo("5"));
        }
    }
}
