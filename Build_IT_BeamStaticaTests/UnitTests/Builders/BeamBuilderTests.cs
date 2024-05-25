using Build_IT_BeamStatica.Builders;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_BeamStaticaTests.UnitTests.Builders
{
    [TestFixture]
    public class BeamBuilderTests
    {
        [Test]
        public void BuildTest()
        {
            var beamBuilder = new BeamBuilder();
            
            var result = beamBuilder.Build();

            Assert.IsNotNull(result);
        }
    }
}
