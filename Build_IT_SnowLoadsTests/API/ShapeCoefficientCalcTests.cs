using Build_IT_SnowLoads.API;
using NUnit.Framework;
using System;

namespace Build_IT_SnowLoadsUnitTests.API
{
    [TestFixture()]
    public class ShapeCoefficientCalcTests
    {
        #region CalculateSnowLoadShapeCoefficient1

        [Test()]
        public void CalculateSnowLoadShapeCoefficient1Test_LessThan30Degree_WithoutFences()
        {
            double slope = 25;
            double result = ShapeCoefficientCalc.CalculateSnowLoadShapeCoefficient1(slope);

            Assert.AreEqual(0.8, result);
        }

        [Test()]
        public void CalculateSnowLoadShapeCoefficient1Test_LessThan60Degree_WithoutFences()
        {
            double slope = 50;
            double result = ShapeCoefficientCalc.CalculateSnowLoadShapeCoefficient1(slope);

            Assert.AreEqual(0.266667, result, 0.001);
        }

        [Test()]
        public void CalculateSnowLoadShapeCoefficient1Test_MoreThan60Degree_WithoutFences()
        {
            double slope = 80;
            double result = ShapeCoefficientCalc.CalculateSnowLoadShapeCoefficient1(slope);

            Assert.AreEqual(0, result);
        }

        [Test()]
        public void CalculateSnowLoadShapeCoefficient1Test_WithFences()
        {
            double slope = 80;
            bool snowFences = true;
            double result = ShapeCoefficientCalc.CalculateSnowLoadShapeCoefficient1(slope, snowFences);

            Assert.AreEqual(0.8, result);
        }

        [Test()]
        public void CalculateSnowLoadShapeCoefficient1Test_LessThan0Degree_WithoutFences_ThrowsArgumentOutOfRangeException()
        {
            double slope = -50;

            Assert.Throws<ArgumentOutOfRangeException>(() => ShapeCoefficientCalc.CalculateSnowLoadShapeCoefficient1(slope));
        }

        [Test()]
        public void CalculateSnowLoadShapeCoefficient1Test_LessThan0Degree_WithFences_ThrowsArgumentOutOfRangeException()
        {
            double slope = -50;
            bool snowFences = true;

            Assert.Throws<ArgumentOutOfRangeException>(() => ShapeCoefficientCalc.CalculateSnowLoadShapeCoefficient1(slope, snowFences));
        }

        #endregion // CalculateSnowLoadShapeCoefficient1

        #region CalculateSnowLoadShapeCoefficient2

        [Test]
        public void CalculateSnowLoadShapeCoefficient2Test_LessThan30Degree()
        {
            double slope = 20;

            double result = ShapeCoefficientCalc.CalculateSnowLoadShapeCoefficient2(slope);

            Assert.AreEqual(1.33334, result, 0.001);
        }

        [Test]
        public void CalculateSnowLoadShapeCoefficient2Test_LessThan60Degree()
        {
            double slope = 50;

            double result = ShapeCoefficientCalc.CalculateSnowLoadShapeCoefficient2(slope);

            Assert.AreEqual(1.6, result);
        }

        [Test]
        public void CalculateSnowLoadShapeCoefficient2Test_MoreThan60Degree()
        {
            double slope = 70;

            double result = ShapeCoefficientCalc.CalculateSnowLoadShapeCoefficient2(slope);

            Assert.AreEqual(1.6, result);
        }

        [Test]
        public void CalculateSnowLoadShapeCoefficient2Test_LessThan0Degree()
        {
            double slope = -20;

            Assert.Throws<ArgumentOutOfRangeException>(() => ShapeCoefficientCalc.CalculateSnowLoadShapeCoefficient2(slope));
        }

        #endregion // CalculateSnowLoadShapeCoefficient2
    }
}