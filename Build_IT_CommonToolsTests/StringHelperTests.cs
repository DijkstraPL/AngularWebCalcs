using Build_IT_CommonTools.Extensions;
using NUnit.Framework;

namespace Build_IT_CommonToolsTests
{
    [TestFixture]
    public class StringHelperTests
    {
        [Test]
        public void EverythingBetweenTest_Success()
        {
            var result = "a[bcde]fgh[ijk]l".EverythingBetween("[", "]");

            CollectionAssert.IsNotEmpty(result);
            CollectionAssert.Contains(result, "bcde");
            CollectionAssert.Contains(result, "ijk");
        }
    }
}
