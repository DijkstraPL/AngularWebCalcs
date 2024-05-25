using Build_IT_Data.Geometry;
using NUnit.Framework;
using System.Collections.Generic;

namespace Build_IT_DataTests.Geometry
{
    [TestFixture]
    public class SectionTests
    {
        [Test]
        public void CreateSectionTest()
        {
            var section = new Section();
            section.AddPoints(
                new List<ContourPoint>
                {
                    new ContourPoint(0,0),
                    new ContourPoint(2,0),
                    new ContourPoint(2,5),
                    new ContourPoint(0,5),
                });

            Assert.Multiple(() =>
            {
                Assert.That(section.Area, Is.EqualTo(10));
                Assert.That(section.Circumference, Is.EqualTo(14));
                Assert.That(section.CenterOfGravity.X, Is.EqualTo(1));
                Assert.That(section.CenterOfGravity.Y, Is.EqualTo(2.5));
                Assert.AreEqual(20.833, section.SecondMomentOfInertiaX, 0.001);
                Assert.AreEqual(3.333, section.SecondMomentOfInertiaY, 0.001);
            });
        }

        [Test]
        public void AddPointsSectionTest()
        {
            var section = new Section();
            section.AddPoints(
                new List<ContourPoint>
                {
                    new ContourPoint(0,0),
                    new ContourPoint(2,0),
                    new ContourPoint(2,5),
                });

            section.AddPoint(new ContourPoint(0, 5));

            Assert.Multiple(() =>
            {
                Assert.That(section.Area, Is.EqualTo(10));
                Assert.That(section.Circumference, Is.EqualTo(14));
                Assert.That(section.CenterOfGravity.X, Is.EqualTo(1));
                Assert.That(section.CenterOfGravity.Y, Is.EqualTo(2.5));
                Assert.AreEqual(20.833, section.SecondMomentOfInertiaX, 0.001);
                Assert.AreEqual(3.333, section.SecondMomentOfInertiaY, 0.001);
            });
        }
    }
}