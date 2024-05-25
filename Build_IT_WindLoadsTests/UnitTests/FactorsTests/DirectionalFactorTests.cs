using Build_IT_WindLoads;
using Build_IT_WindLoads.Factors;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_WindLoadsTests.UnitTests.FactorsTests
{
    [TestFixture]
    public class DirectionalFactorTests
    {
        [Test]
        [TestCase(45, 45)]
        [TestCase(-45, 315)]
        [TestCase(730, 10)]
        public void ConstructorTest_NormalAngle_Success(double angle, double expectedAngle)
        {
            var directionalFactor = new DirectionalFactor(WindZone.II, angle);
            
            Assert.That(directionalFactor.WindDirection, Is.EqualTo(expectedAngle));
        }

        [Test]
        [TestCase(WindZone.I, 13, 0.8)]
        [TestCase(WindZone.I, 177, 0.7)]
        [TestCase(WindZone.I, 205, 0.8)]
        [TestCase(WindZone.I, 230, 0.9)]
        [TestCase(WindZone.I, 314, 1)]
        [TestCase(WindZone.I, 344, 0.9)]
        [TestCase(WindZone.I, 358, 0.8)]
        [TestCase(WindZone.II, 13, 1)]
        [TestCase(WindZone.II, 30, 0.9)]
        [TestCase(WindZone.II, 60, 0.8)]
        [TestCase(WindZone.II, 90, 0.7)]
        [TestCase(WindZone.II, 210, 0.8)]
        [TestCase(WindZone.II, 240, 0.9)]
        [TestCase(WindZone.II, 300, 1)]
        [TestCase(WindZone.II, 360, 1)]
        [TestCase(WindZone.III, -15, 0.8)]
        [TestCase(WindZone.III, 14.99, 0.8)]
        [TestCase(WindZone.III, 15, 0.7)]
        [TestCase(WindZone.III, 134.99, 0.7)]
        [TestCase(WindZone.III, 135, 0.9)]
        [TestCase(WindZone.III, 164.999, 0.9)]
        [TestCase(WindZone.III, 165, 1)]
        [TestCase(WindZone.III, 344.99, 1)]
        [TestCase(WindZone.III, 345, 0.8)]
        public void GetFactorTest_Success(WindZone windZone, double windDirection, double factor)
        {
            var directionalFactor = new DirectionalFactor(windZone, windDirection);

            Assert.That(directionalFactor.GetFactor(), Is.EqualTo(factor));
        }
        
    }
}
