using Build_IT_Data.Sections;
using NUnit.Framework;
using System;

namespace Build_IT_DataTests.UnitTests.Sections
{
    [TestFixture()]
    public class RectangleSectionTests
    {
        private Section _rectangleSection;

        private void SetUpRectangleBeamSection()
        {
            _rectangleSection = new RectangleSection(
                width: 100, height: 250);
        }

        [Test()]
        public void RectangleSection_MinusHeightTest_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(()
                => new RectangleSection(width: 100, height: -250));
        }

        [Test()]
        public void RectangleSection_ZeroWidthTest_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(()
                => new RectangleSection(width: 0, height: 250));
        }

        [Test()]
        public void RectangleSection_CalculatedCircumferenceTest_Success()
        {
            SetUpRectangleBeamSection();
            Assert.That(_rectangleSection.Circumference, Is.EqualTo(70).Within(0.001));
        }

        [Test()]
        public void RectangleSection_CalculatedAreaTest_Success()
        {
            SetUpRectangleBeamSection();
            Assert.That(_rectangleSection.Area, Is.EqualTo(250).Within(0.001));
        }

        [Test()]
        public void RectangleSection_CalculatedCentroidTest_Success()
        {
            SetUpRectangleBeamSection();
            Assert.That(_rectangleSection.Centroid.X, Is.EqualTo(50));
            Assert.That(_rectangleSection.Centroid.Y, Is.EqualTo(125));
        }

        [Test()]
        public void RectangleSection_AdjustedPointsTest_Success()
        {
            SetUpRectangleBeamSection();
            Assert.That(_rectangleSection.AdjustedPoints[0].X, Is.EqualTo(-50), message: "0X");
            Assert.That(_rectangleSection.AdjustedPoints[0].Y, Is.EqualTo(-125), message: "0Y");
            Assert.That(_rectangleSection.AdjustedPoints[1].X, Is.EqualTo(50), message: "1X");
            Assert.That(_rectangleSection.AdjustedPoints[1].Y, Is.EqualTo(-125), message: "1Y");
            Assert.That(_rectangleSection.AdjustedPoints[2].X, Is.EqualTo(50), message: "2X");
            Assert.That(_rectangleSection.AdjustedPoints[2].Y, Is.EqualTo(125), message: "2Y");
            Assert.That(_rectangleSection.AdjustedPoints[3].X, Is.EqualTo(-50).Within(0.0001), message: "3X");
            Assert.That(_rectangleSection.AdjustedPoints[3].Y, Is.EqualTo(125), message: "3Y");
        }

        [Test()]
        public void RectangleSection_CalculatedMomentOfInteriaTest_Success()
        {
            SetUpRectangleBeamSection();
            Assert.That(_rectangleSection.MomentOfInteria, Is.EqualTo(13020.833).Within(0.001));
        }

        [Test()]
        public void RectangleSection_CalculatedSolidHeightTest_Success()
        {
            SetUpRectangleBeamSection();
            Assert.That(_rectangleSection.SolidHeight, Is.EqualTo(250));
        }
    }
}
