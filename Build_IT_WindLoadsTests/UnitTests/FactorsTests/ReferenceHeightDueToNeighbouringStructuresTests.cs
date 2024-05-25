using Build_IT_WindLoads.BuildingData.Interfaces;
using Build_IT_WindLoads.Factors;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_WindLoadsTests.UnitTests.FactorsTests
{
    [TestFixture]
    public class ReferenceHeightDueToNeighbouringStructuresTests
    {
        [Test]
        [TestCase(100,30, 20)]
        [TestCase(40,50,18.75)]
        [TestCase(40,100,15)]
        public void CalculateReferenceHeightDueToNeighbouringStructuresTest_Success(
            double highBuildingHeight, double distanceToBuilding, double expectedResult)
        {
            var referenceHeightDueToNeighbouringStructures =
                 new ReferenceHeightDueToNeighbouringStructures(
                    highBuildingWidth: 10,
                    highBuildingLength: 20,
                    highBuildingHeight,
                    distanceToBuilding
                    );

            var result = referenceHeightDueToNeighbouringStructures.GetFactorAt(height: 15);

            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
