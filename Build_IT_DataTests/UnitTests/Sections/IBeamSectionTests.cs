using Build_IT_Data.Sections;
using NUnit.Framework;
using System;

namespace Build_IT_DataTests.UnitTests.Sections
{
    [TestFixture()]
    public class IBeamSectionTests
    {
        private Section _iBeamSection;

        private void SetUpIBeamSection()
        {
            _iBeamSection = new IBeamSection(
                width: 91, height: 180,
                flangeWidth: 8, webWidth: 5.3, radius: 9);
        }

        [TestCase(-91, 180, 8, 5.3, 9)]
        [TestCase(91, -180, 8, 5.3, 9)]
        [TestCase(91, 0, 8, 5.3, 9)]
        [TestCase(91, 180, -8, 5.3, 9)]
        [TestCase(91, 180, 8, -5.3, 9)]
        [TestCase(91, 180, 8, -5.3, -9)]
        public void IBeamSection_WrongArguments_ThrowsArgumentOutOfRangeException(
            double width, double height, double flangeWidth, double webWidth, double radius)
        {
            Assert.Throws<ArgumentOutOfRangeException>(()
                => new IBeamSection(
                width, height, flangeWidth, webWidth, radius));
        }

        [Test()]
        public void IBeamSection_ZeroRadiusTest_Success()
        {
            var beamSection = new IBeamSection(
                 width: 10, height: 10,
                 flangeWidth: 1, webWidth: 1, radius: 0);
            Assert.That(beamSection.Circumference, Is.EqualTo(5.8).Within(0.001));
        }

        [Test()]
        public void IBeamSection_CalculatedCircumferenceTest_Success()
        {
            SetUpIBeamSection();
            Assert.That(_iBeamSection.Circumference, Is.EqualTo(69.795).Within(0.001));
        }

        [Test()]
        public void IBeamSection_CalculatedAreaTest_Success()
        {
            SetUpIBeamSection();
            Assert.That(_iBeamSection.Area, Is.EqualTo(23.947).Within(0.001));
        }

        [Test()]
        public void IBeamSection_CalculatedCentroidTest_Success()
        {
            SetUpIBeamSection();
            Assert.That(_iBeamSection.Centroid.X, Is.EqualTo(45.5));
            Assert.That(_iBeamSection.Centroid.Y, Is.EqualTo(90));
        }

        [Test()]
        public void IBeamSection_AdjustedPointsTest_Success()
        {
            SetUpIBeamSection();
            Assert.That(_iBeamSection.AdjustedPoints[0].X, Is.EqualTo(-45.5), message: "0X");
            Assert.That(_iBeamSection.AdjustedPoints[0].Y, Is.EqualTo(-90), message: "0Y");
            Assert.That(_iBeamSection.AdjustedPoints[1].X, Is.EqualTo(45.5), message: "1X");
            Assert.That(_iBeamSection.AdjustedPoints[1].Y, Is.EqualTo(-90), message: "1Y");
            Assert.That(_iBeamSection.AdjustedPoints[2].X, Is.EqualTo(45.5), message: "2X");
            Assert.That(_iBeamSection.AdjustedPoints[2].Y, Is.EqualTo(-82), message: "2Y");
            Assert.That(_iBeamSection.AdjustedPoints[3].X, Is.EqualTo(11.65).Within(0.0001), message: "3X");
            Assert.That(_iBeamSection.AdjustedPoints[3].Y, Is.EqualTo(-82), message: "3Y");
            Assert.That(_iBeamSection.AdjustedPoints[4].X, Is.EqualTo(9.3206).Within(0.0001), message: "4X");
            Assert.That(_iBeamSection.AdjustedPoints[4].Y, Is.EqualTo(-81.6933).Within(0.0001), message: "4Y");
            Assert.That(_iBeamSection.AdjustedPoints[5].X, Is.EqualTo(7.15).Within(0.0001), message: "5X");
            Assert.That(_iBeamSection.AdjustedPoints[5].Y, Is.EqualTo(-80.7942).Within(0.0001), message: "5Y");
            Assert.That(_iBeamSection.AdjustedPoints[6].X, Is.EqualTo(5.286).Within(0.0001), message: "6X");
            Assert.That(_iBeamSection.AdjustedPoints[6].Y, Is.EqualTo(-79.364).Within(0.0001), message: "6Y");
            Assert.That(_iBeamSection.AdjustedPoints[7].X, Is.EqualTo(3.8558).Within(0.0001), message: "7X");
            Assert.That(_iBeamSection.AdjustedPoints[7].Y, Is.EqualTo(-77.5).Within(0.0001), message: "7Y");
            Assert.That(_iBeamSection.AdjustedPoints[8].X, Is.EqualTo(2.9567).Within(0.0001), message: "8X");
            Assert.That(_iBeamSection.AdjustedPoints[8].Y, Is.EqualTo(-75.3294).Within(0.0001), message: "8Y");
            Assert.That(_iBeamSection.AdjustedPoints[9].X, Is.EqualTo(2.65).Within(0.0001), message: "9X");
            Assert.That(_iBeamSection.AdjustedPoints[9].Y, Is.EqualTo(-73).Within(0.0001), message: "9Y");
            Assert.That(_iBeamSection.AdjustedPoints[10].X, Is.EqualTo(2.65).Within(0.0001), message: "10X");
            Assert.That(_iBeamSection.AdjustedPoints[10].Y, Is.EqualTo(73).Within(0.0001), message: "10Y");
            Assert.That(_iBeamSection.AdjustedPoints[11].X, Is.EqualTo(2.9567).Within(0.0001), message: "11X");
            Assert.That(_iBeamSection.AdjustedPoints[11].Y, Is.EqualTo(75.3294).Within(0.0001), message: "11Y");
            Assert.That(_iBeamSection.AdjustedPoints[12].X, Is.EqualTo(3.8558).Within(0.0001), message: "12X");
            Assert.That(_iBeamSection.AdjustedPoints[12].Y, Is.EqualTo(77.5).Within(0.0001), message: "12Y");
            Assert.That(_iBeamSection.AdjustedPoints[13].X, Is.EqualTo(5.286).Within(0.0001), message: "13X");
            Assert.That(_iBeamSection.AdjustedPoints[13].Y, Is.EqualTo(79.364).Within(0.0001), message: "13Y");
            Assert.That(_iBeamSection.AdjustedPoints[14].X, Is.EqualTo(7.15).Within(0.0001), message: "14X");
            Assert.That(_iBeamSection.AdjustedPoints[14].Y, Is.EqualTo(80.7942).Within(0.0001), message: "14Y");
            Assert.That(_iBeamSection.AdjustedPoints[15].X, Is.EqualTo(9.3206).Within(0.0001), message: "15X");
            Assert.That(_iBeamSection.AdjustedPoints[15].Y, Is.EqualTo(81.6933).Within(0.0001), message: "15Y");
            Assert.That(_iBeamSection.AdjustedPoints[16].X, Is.EqualTo(11.65).Within(0.0001), message: "16X");
            Assert.That(_iBeamSection.AdjustedPoints[16].Y, Is.EqualTo(82).Within(0.0001), message: "16Y");
            Assert.That(_iBeamSection.AdjustedPoints[17].X, Is.EqualTo(45.5).Within(0.0001), message: "17X");
            Assert.That(_iBeamSection.AdjustedPoints[17].Y, Is.EqualTo(82).Within(0.0001), message: "17Y");
            Assert.That(_iBeamSection.AdjustedPoints[18].X, Is.EqualTo(45.5).Within(0.0001), message: "18X");
            Assert.That(_iBeamSection.AdjustedPoints[18].Y, Is.EqualTo(90).Within(0.0001), message: "18Y");
            Assert.That(_iBeamSection.AdjustedPoints[19].X, Is.EqualTo(-45.5).Within(0.0001), message: "19X");
            Assert.That(_iBeamSection.AdjustedPoints[19].Y, Is.EqualTo(90).Within(0.0001), message: "19Y");
            Assert.That(_iBeamSection.AdjustedPoints[20].X, Is.EqualTo(-45.5).Within(0.0001), message: "20X");
            Assert.That(_iBeamSection.AdjustedPoints[20].Y, Is.EqualTo(82).Within(0.0001), message: "20Y");
            Assert.That(_iBeamSection.AdjustedPoints[20].X, Is.EqualTo(-45.5).Within(0.0001), message: "20X");
            Assert.That(_iBeamSection.AdjustedPoints[20].Y, Is.EqualTo(82).Within(0.0001), message: "20Y");
            Assert.That(_iBeamSection.AdjustedPoints[21].X, Is.EqualTo(-11.65).Within(0.0001), message: "21X");
            Assert.That(_iBeamSection.AdjustedPoints[21].Y, Is.EqualTo(82), message: "21Y");
            Assert.That(_iBeamSection.AdjustedPoints[22].X, Is.EqualTo(-9.3206).Within(0.0001), message: "22X");
            Assert.That(_iBeamSection.AdjustedPoints[22].Y, Is.EqualTo(81.6933).Within(0.0001), message: "22Y");
            Assert.That(_iBeamSection.AdjustedPoints[23].X, Is.EqualTo(-7.15).Within(0.0001), message: "23X");
            Assert.That(_iBeamSection.AdjustedPoints[23].Y, Is.EqualTo(80.7942).Within(0.0001), message: "23Y");
            Assert.That(_iBeamSection.AdjustedPoints[24].X, Is.EqualTo(-5.286).Within(0.0001), message: "24X");
            Assert.That(_iBeamSection.AdjustedPoints[24].Y, Is.EqualTo(79.364).Within(0.0001), message: "24Y");
            Assert.That(_iBeamSection.AdjustedPoints[25].X, Is.EqualTo(-3.8558).Within(0.0001), message: "25X");
            Assert.That(_iBeamSection.AdjustedPoints[25].Y, Is.EqualTo(77.5).Within(0.0001), message: "25Y");
            Assert.That(_iBeamSection.AdjustedPoints[26].X, Is.EqualTo(-2.9567).Within(0.0001), message: "26X");
            Assert.That(_iBeamSection.AdjustedPoints[26].Y, Is.EqualTo(75.3294).Within(0.0001), message: "26Y");
            Assert.That(_iBeamSection.AdjustedPoints[27].X, Is.EqualTo(-2.65).Within(0.0001), message: "27X");
            Assert.That(_iBeamSection.AdjustedPoints[27].Y, Is.EqualTo(73).Within(0.0001), message: "27Y");
            Assert.That(_iBeamSection.AdjustedPoints[28].X, Is.EqualTo(-2.65).Within(0.0001), message: "28X");
            Assert.That(_iBeamSection.AdjustedPoints[28].Y, Is.EqualTo(-73).Within(0.0001), message: "28Y");
            Assert.That(_iBeamSection.AdjustedPoints[29].X, Is.EqualTo(-2.9567).Within(0.0001), message: "29X");
            Assert.That(_iBeamSection.AdjustedPoints[29].Y, Is.EqualTo(-75.3294).Within(0.0001), message: "29Y");
            Assert.That(_iBeamSection.AdjustedPoints[30].X, Is.EqualTo(-3.8558).Within(0.0001), message: "30X");
            Assert.That(_iBeamSection.AdjustedPoints[30].Y, Is.EqualTo(-77.5).Within(0.0001), message: "30Y");
            Assert.That(_iBeamSection.AdjustedPoints[31].X, Is.EqualTo(-5.286).Within(0.0001), message: "31X");
            Assert.That(_iBeamSection.AdjustedPoints[31].Y, Is.EqualTo(-79.364).Within(0.0001), message: "31Y");
            Assert.That(_iBeamSection.AdjustedPoints[32].X, Is.EqualTo(-7.15).Within(0.0001), message: "32X");
            Assert.That(_iBeamSection.AdjustedPoints[32].Y, Is.EqualTo(-80.7942).Within(0.0001), message: "32Y");
            Assert.That(_iBeamSection.AdjustedPoints[33].X, Is.EqualTo(-9.3206).Within(0.0001), message: "33X");
            Assert.That(_iBeamSection.AdjustedPoints[33].Y, Is.EqualTo(-81.6933).Within(0.0001), message: "33Y");
            Assert.That(_iBeamSection.AdjustedPoints[34].X, Is.EqualTo(-11.65).Within(0.0001), message: "34X");
            Assert.That(_iBeamSection.AdjustedPoints[34].Y, Is.EqualTo(-82).Within(0.0001), message: "34Y");
            Assert.That(_iBeamSection.AdjustedPoints[35].X, Is.EqualTo(-45.5).Within(0.0001), message: "35X");
            Assert.That(_iBeamSection.AdjustedPoints[35].Y, Is.EqualTo(-82).Within(0.0001), message: "35Y");
        }

        [Test()]
        public void IBeamSection_CalculatedMomentOfInteriaTest_Success()
        {
            SetUpIBeamSection();
            Assert.That(_iBeamSection.MomentOfInteria, Is.EqualTo(1316.959).Within(0.001));
        }

        [Test()]
        public void IBeamSection_CalculatedMomentOfInteriaTest_IPE300_Success()
        {
            var iBeamSection = new IBeamSection(
                width: 150, height: 300,
                flangeWidth: 10.7, webWidth: 7.1, radius: 15);
            Assert.That(iBeamSection.MomentOfInteria, Is.EqualTo(8356.109).Within(0.001));
        }

        [Test()]
        public void IBeamSection_CalculatedSolidHeightTest_Success()
        {
            SetUpIBeamSection();
            Assert.That(_iBeamSection.SolidHeight, Is.EqualTo(180));
        }
    }
}
