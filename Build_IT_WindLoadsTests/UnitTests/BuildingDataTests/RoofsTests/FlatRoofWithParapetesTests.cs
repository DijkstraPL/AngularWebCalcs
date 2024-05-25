using Build_IT_WindLoads.BuildingData.Roofs;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_WindLoadsTests.UnitTests.BuildingDataTests.RoofsTests
{
    [TestFixture]
    public class FlatRoofWithParapetesTests
    {
        [Test]
        public void ConstructorTest_Roof_Success()
        {
            var building = new FlatRoofWithParapetes(length: 10, width: 5, height: 3, parapetHeight: 2);

            Assert.That(building.Length, Is.EqualTo(10));
            Assert.That(building.Width, Is.EqualTo(5));
            Assert.That(building.Height, Is.EqualTo(5));
            Assert.That(building.Areas.Count, Is.GreaterThan(0));
        }
    }
}
