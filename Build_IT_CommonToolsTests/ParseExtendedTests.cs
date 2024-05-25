using Build_IT_CommonTools.Extensions;
using NUnit.Framework;

namespace Build_IT_CommonToolsTests
{
    [TestFixture]
    public class ParseExtendedTests
    {
        [Test]
        public static void GetDoubleTest_Success()
        {
            var result = "1.1".GetDouble();

            Assert.That(result, Is.TypeOf(typeof(double)));
            Assert.That(result, Is.EqualTo(1.1));
        }

        [Test]
        public static void GetDoubleTest_WrongString_Returns0_Success()
        {
            var result = "a".GetDouble();

            Assert.That(result, Is.TypeOf(typeof(double)));
            Assert.That(result, Is.EqualTo(0));
        }
    }
}
