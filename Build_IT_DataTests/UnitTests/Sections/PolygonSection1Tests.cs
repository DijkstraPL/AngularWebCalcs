using Build_IT_Data.Sections;
using Build_IT_Data.Sections.Additional;
using Build_IT_Data.Sections.Additional.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Build_IT_DataTests.UnitTests.Sections
{
    [TestFixture()]
    public class PolygonSection1Tests
    {
        private Section _polygonSection;

        private void SetUpPolygonSection()
        {
            var points = new List<IPoint>();
            points.Add(new Point(0, 0));
            points.Add(new Point(200, 0));
            points.Add(new Point(200, 300));
            points.Add(new Point(0, 300));
            _polygonSection = new Section(points);
        }

        [Test()]
        public void PolygonSection_LessThan3PointsTest_ThrowsArthmeticException()
        {
            var points = new List<IPoint>();
            points.Add(new Point(0, 0));
            points.Add(new Point(200, 0));
            Assert.Throws<ArithmeticException>(() => new Section(points));
        }

        [Test()]
        public void PolygonSection_CalculatedCircumferenceTest_Success()
        {
            SetUpPolygonSection();
            Assert.That(_polygonSection.Circumference, Is.EqualTo(100));
        }

        [Test()]
        public void PolygonSection_CalculatedAreaTest_Success()
        {
            SetUpPolygonSection();
            Assert.That(_polygonSection.Area, Is.EqualTo(600));
        }

        [Test()]
        public void PolygonSection_CalculatedCentroidTest_Success()
        {
            SetUpPolygonSection();
            Assert.That(_polygonSection.Centroid.X, Is.EqualTo(100));
            Assert.That(_polygonSection.Centroid.Y, Is.EqualTo(150));
        }

        [Test()]
        public void PolygonSection_AdjustedPointsTest_Success()
        {
            SetUpPolygonSection();
            Assert.That(_polygonSection.AdjustedPoints[0].X, Is.EqualTo(-100));
            Assert.That(_polygonSection.AdjustedPoints[0].Y, Is.EqualTo(-150));
            Assert.That(_polygonSection.AdjustedPoints[1].X, Is.EqualTo(100));
            Assert.That(_polygonSection.AdjustedPoints[1].Y, Is.EqualTo(-150));
            Assert.That(_polygonSection.AdjustedPoints[2].X, Is.EqualTo(100));
            Assert.That(_polygonSection.AdjustedPoints[2].Y, Is.EqualTo(150));
            Assert.That(_polygonSection.AdjustedPoints[3].X, Is.EqualTo(-100));
            Assert.That(_polygonSection.AdjustedPoints[3].Y, Is.EqualTo(150));
        }

        [Test()]
        public void PolygonSection_CalculatedMomentOfInteriaTest_Success()
        {
            SetUpPolygonSection();
            Assert.That(_polygonSection.MomentOfInteria, Is.EqualTo(45000));
        }

        [Test()]
        public void PolygonSection_CalculatedSolidHeightTest_Success()
        {
            SetUpPolygonSection();
            Assert.That(_polygonSection.SolidHeight, Is.EqualTo(300));
        }
    }
}