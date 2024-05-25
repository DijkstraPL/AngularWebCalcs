using Build_IT_CommonTools.Maths;
using NUnit.Framework;

namespace Build_IT_CommonToolsTests
{
    [TestFixture]
    public class InterpolationTests
    {
        [Test]
        public void InterpolateLinearBetweenTest_BetweenTwoPoints_Success()
        {
           var result = Interpolation.InterpolateLinearBetween(
                start:(position:1, value:1), 
                end:(position: 3, value: 3), 
                at:2);

            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void InterpolateLinearBetweenTest_PointToTheLeft_Success()
        {
            var result = Interpolation.InterpolateLinearBetween(
                 start: (position: 1, value: 1),
                 end: (position: 3, value: 3),
                 at: 0);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void InterpolateLinearBetweenTest_PointToTheRight_Success()
        {
            var result = Interpolation.InterpolateLinearBetween(
                 start: (position: 1, value: 1),
                 end: (position: 3, value: 3),
                 at: 4);

            Assert.That(result, Is.EqualTo(4));
        }

        [Test]
        public void InterpolateLinearBetweenTest_RotatedFunction_Success()
        {
            var result = Interpolation.InterpolateLinearBetween(
                 start: (position: 3, value: 3),
                 end: (position: 1, value: 1),
                 at: 2);

            Assert.That(result, Is.EqualTo(2));
        }
    }
}
