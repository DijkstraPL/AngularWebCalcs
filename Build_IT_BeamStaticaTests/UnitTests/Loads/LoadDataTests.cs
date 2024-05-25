using Build_IT_BeamStatica.Loads;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_BeamStaticaTests.UnitTests.Loads
{
    [TestFixture]
    public class LoadDataTests
    {
        [Test]
        public void SetUpLoadData_Success()
        {
            var result = new LoadData(5, 10);

            Assert.That(result.Position, Is.EqualTo(5));
            Assert.That(result.Value, Is.EqualTo(10));
        }
    }
}
