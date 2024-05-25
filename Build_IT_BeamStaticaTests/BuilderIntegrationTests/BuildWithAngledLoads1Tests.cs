using Build_IT_BeamStatica;
using Build_IT_BeamStatica.Builders;
using Build_IT_BeamStatica.Loads.Enums;
using Build_IT_BeamStatica.Nodes.Enums;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using static Build_IT_BeamStatica.Builders.BuildersOrchestrator;

namespace Build_IT_BeamStaticaTests.BuilderIntegrationTests
{
    [TestFixture]
    public class BuildWithAngledLoads1Tests
    {
        [Test]
        public void SetUp()
        {
            var material = Material
                            .WithDensity(2400)
                            .WithYoungModulus(30)
                            .WithThermalExpansionCoefficient(0.000010);

            var sectionProperties = SectionProperties
                            .WithPoint(0, 0)
                            .WithPoint(300, 0)
                            .WithPoint(300, 500)
                            .WithPoint(0, 500);

            var span1 = Span
                .WithLength(3)
                .With(material)
                .With(sectionProperties)
                .WithLeft(Node.Telescope)
                .WithRight(Node.Pin
                    .With(PointLoad.VerticalDisplacement
                        .WithValue(-10)))
                .With(PointLoad.OnSpan.AngledLoad
                    .WithValue(-200)
                    .WithPosition(1.5)
                    .WithAngle(-45));

            var span2 = Span
                .WithLength(7)
                .With(material)
                .With(sectionProperties)
                .WithRight(Node.Hinge
                    .With(PointLoad.AngledLoad
                        .WithValue(-100)
                        .WithAngle(30)))
                .With(ContinuousLoad.ShearLoad
                    .WithStartValue(-20)
                    .WithStartPosition(0)
                    .WithEndValue(-20)
                    .WithEndPosition(7));

            var span3 = Span
                .WithLength(5)
                .With(material)
                .With(sectionProperties)
                .WithRight(Node.Support)
                .With(ContinuousLoad.UpDownTemperatureDifferenceLoad
                    .WithLowerTemperature(0)
                    .WithUpperTemperature(5))
                .With(ContinuousLoad.AlongTemperatureDifferenceContinousLoad
                    .WithTemperatureDifference(10));

            var beam = Beam
              .With(span1)
              .With(span2)
              .With(span3)
              .Build();

            var beamCalculator = new BeamCalculator(beam);
            var result = beamCalculator.Calculate();

            Assert.That(result.NormalForces[0], Is.EqualTo(39.387).Within(0.001));
            Assert.That(result.ShearForces[0], Is.EqualTo(0).Within(0.001));
            Assert.That(result.BendingMoments[0], Is.EqualTo(-884.086).Within(0.001));

            Assert.That(result.NormalForces[1.5], Is.EqualTo(39.387).Within(0.001));
            Assert.That(result.ResultsContainer.NormalForce.GetValue(1.5001).Value, Is.EqualTo(180.808).Within(0.001));
            Assert.That(result.NormalForces[3], Is.EqualTo(180.808).Within(0.001));
        }
    }
}
