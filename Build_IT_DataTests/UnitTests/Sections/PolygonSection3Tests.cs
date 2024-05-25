using Build_IT_Data.Sections;
using Build_IT_Data.Sections.Additional;
using Build_IT_Data.Sections.Additional.Interfaces;
using NUnit.Framework;
using System.Collections.Generic;

namespace Build_IT_DataTests.UnitTests.Sections
{
    [TestFixture()]
    public class PolygonSection3Tests
    {
        private Section _polygonSection;

        [SetUp()]
        public void SetUpPolygonSection()
        {
            var points = new List<IPoint>();
            points.Add(new Point(0, 0));
            points.Add(new Point(300, -200));
            points.Add(new Point(800, -50));
            points.Add(new Point(700, 50));
            points.Add(new Point(900, 200));
            points.Add(new Point(400, 200));
            points.Add(new Point(300, 250));
            _polygonSection = new Section(points);
        }

        [Test()]
        public void PolygonSection_CalculatedCircumferenceTest_Success()
        {
            Assert.That(_polygonSection.Circumference, Is.EqualTo(227.6308).Within(0.0001));
        }

        [Test()]
        public void PolygonSection_CalculatedAreaTest_Success()
        {
            Assert.That(_polygonSection.Area, Is.EqualTo(2275));
        }

        [Test()]
        public void PolygonSection_CalculatedCentroidTest_Success()
        {
            Assert.That(_polygonSection.Centroid.X, Is.EqualTo(426.3736).Within(0.0001));
            Assert.That(_polygonSection.Centroid.Y, Is.EqualTo(33.3333).Within(0.0001));
        }

        [Test()]
        public void PolygonSection_AdjustedPointsTest_Success()
        {
            Assert.That(_polygonSection.AdjustedPoints[0].X, Is.EqualTo(-426.3736).Within(0.0001), message: "Point 0 - X");
            Assert.That(_polygonSection.AdjustedPoints[0].Y, Is.EqualTo(-33.3333).Within(0.0001), message: "Point 0 - Y");
            Assert.That(_polygonSection.AdjustedPoints[1].X, Is.EqualTo(-126.3736).Within(0.0001), message: "Point 1 - X");
            Assert.That(_polygonSection.AdjustedPoints[1].Y, Is.EqualTo(-233.3333).Within(0.0001), message: "Point 1 - Y");
            Assert.That(_polygonSection.AdjustedPoints[2].X, Is.EqualTo(373.6264).Within(0.0001), message: "Point 2 - X");
            Assert.That(_polygonSection.AdjustedPoints[2].Y, Is.EqualTo(-83.3333).Within(0.0001), message: "Point 2 - Y");
            Assert.That(_polygonSection.AdjustedPoints[3].X, Is.EqualTo(273.6264).Within(0.0001), message: "Point 3 - X");
            Assert.That(_polygonSection.AdjustedPoints[3].Y, Is.EqualTo(16.6667).Within(0.0001), message: "Point 3 - Y");
            Assert.That(_polygonSection.AdjustedPoints[4].X, Is.EqualTo(473.6264).Within(0.0001), message: "Point 4 - X");
            Assert.That(_polygonSection.AdjustedPoints[4].Y, Is.EqualTo(166.6667).Within(0.0001), message: "Point 4 - Y");
            Assert.That(_polygonSection.AdjustedPoints[5].X, Is.EqualTo(-26.3736).Within(0.0001), message: "Point 5 - X");
            Assert.That(_polygonSection.AdjustedPoints[5].Y, Is.EqualTo(166.6667).Within(0.0001), message: "Point 5 - Y");
            Assert.That(_polygonSection.AdjustedPoints[6].X, Is.EqualTo(-126.3736).Within(0.0001), message: "Point 6 - X");
            Assert.That(_polygonSection.AdjustedPoints[6].Y, Is.EqualTo(216.6667).Within(0.0001), message: "Point 6 - Y");
        }

        [Test()]
        public void PolygonSection_CalculatedMomentOfInteriaTest_Success()
        {
            Assert.That(_polygonSection.MomentOfInteria, Is.EqualTo(232118.0556).Within(0.0001));
        }

        [Test()]
        public void PolygonSection_CalculatedSolidHeightTest_Success()
        {
            Assert.That(_polygonSection.SolidHeight, Is.EqualTo(450));
        }
    }
}