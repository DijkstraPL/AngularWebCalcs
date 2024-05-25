//using Microsoft.VisualStudio.TestTools.UnitTesting;
using Build_IT_SnowLoads;
using Build_IT_SnowLoads.Enums;
using NUnit.Framework;
using System;

namespace Build_IT_SnowLoadsUnitTests
{
    [TestFixture()]
    public class SnowLoadTests
    {
        #region Constructors

        [Test()]
        [Description("Check the creation of SnowLoad object.")]
        public void SnowLoadTest_ConstructorWithBuildingSite_Success()
        {
            var snowLoad = new SnowLoad(new BuildingSite());

            Assert.IsTrue(snowLoad.BuildingSite != null, "Building Site shouldn't be null.");
        }

        [Test()]
        [Description("Check the creation of SnowLoad object.")]
        public void SnowLoadTest_ConstructorWithBuildingSiteAndDesignSituation_Success()
        {
            var snowLoad = new SnowLoad(new BuildingSite(), DesignSituation.B2);

            Assert.IsTrue(snowLoad.BuildingSite != null, "Building Site shouldn't be null.");
            Assert.IsTrue(snowLoad.CurrentDesignSituation == DesignSituation.B2, "Design situation is not set.");
        }

        [Test()]
        [Description("Check the creation of SnowLoad object.")]
        public void SnowLoadTest_ConstructorWithBuildingSiteDesignSituationAndReturnPeriod_Success()
        {
            var snowLoad = new SnowLoad(new BuildingSite(), 45, DesignSituation.B2);

            Assert.IsTrue(snowLoad.BuildingSite != null, "Building Site shouldn't be null.");
            Assert.IsTrue(snowLoad.ReturnPeriod == 45, "Return period is not set.");
            Assert.IsTrue(snowLoad.CurrentDesignSituation == DesignSituation.B2, "Design situation is not set.");
        }

        [Test()]
        [Description("Check the creation of SnowLoad object.")]
        public void SnowLoadTest_ConstructorWithAllPossibleParameters_Success()
        {
            var snowLoad = new SnowLoad(new BuildingSite(), 3, 45, DesignSituation.B2, true);

            Assert.IsTrue(snowLoad.BuildingSite != null, "Building Site shouldn't be null.");
            Assert.IsTrue(snowLoad.SnowDensity == 3, "Snow density is not set.");
            Assert.IsTrue(snowLoad.ReturnPeriod == 45, "Return period is not set.");
            Assert.IsTrue(snowLoad.CurrentDesignSituation == DesignSituation.B2, "Design situation is not set.");
            Assert.IsTrue(snowLoad.ExcepctionalSituation, "Exceptional situation is not set.");
        }

        #endregion // Constructors

        #region SnowDensity 

        [Test()]
        [Description("Ensure that user can only use proper values of the snwow load.")]
        public void SnowDensityTest_Value1_Success()
        {
            var snowLoad = new SnowLoad(new BuildingSite() , snowDensity:1 );

            Assert.AreEqual(1, snowLoad.SnowDensity, "Value is not equal to the entered one.");
        }

        [Test()]
        [Description("Ensure that user can only use proper values of the snwow load.")]
        public void SnowDensityTest_Value2_Success()
        {
            var snowLoad = new SnowLoad(new BuildingSite(), snowDensity: 2);

            Assert.AreEqual(2, snowLoad.SnowDensity, "Value is not equal to the entered one.");
        }

        [Test()]
        [Description("Ensure that user can only use proper values of the snwow load.")]
        public void SnowDensityTest_Value3_Success()
        {
            var snowLoad = new SnowLoad(new BuildingSite(), snowDensity: 3);

            Assert.AreEqual(3, snowLoad.SnowDensity, "Value is not equal to the entered one.");
        }

        [Test()]
        [Description("Ensure that user can only use proper values of the snwow load.")]
        public void SnowDensityTest_Value5_Exception()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() 
                => new SnowLoad(new BuildingSite(), snowDensity: 5), 
                "Snow load shouldn't be that much.");
        }
        #endregion // SnowDensity

        #region ExceptionalSituation

        [Test()]
        [Description("Check the ExceptionalSituation property.")]
        public void SnowLoadTest_ExceptionalSituationForDesignSituationA_Success()
        {
            var snowLoad = new SnowLoad(new BuildingSite(), DesignSituation.A, true);

            Assert.IsFalse(snowLoad.ExcepctionalSituation, "For design situation A exceptional situation should be false.");
        }

        [Test()]
        [Description("Check the ExceptionalSituation property.")]
        public void SnowLoadTest_ExceptionalSituationForDesignSituationB2_Success()
        {
            var snowLoad = new SnowLoad(new BuildingSite(), DesignSituation.B2, true);

            Assert.IsTrue(snowLoad.ExcepctionalSituation, "For design situation other than A exceptional situation should be proper.");
        }

        #endregion // ExceptionalSituation

        #region ReturnPeriod

        [Test()]
        [Description("Check the ReturnPeriod property.")]
        public void SnowLoadTest_ReturnPeriodLessThan5_Exception()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() 
                => new SnowLoad(new BuildingSite(), returnPeriod: 3), 
                "Return period which is less than 5 throws ArgumentOutOfRangeException.");
        }

        [Test()]
        [Description("Check the ReturnPeriod property.")]
        public void SnowLoadTest_ReturnPeriodMoreThan5_Success()
        {
            var snowLoad = new SnowLoad(new BuildingSite(), returnPeriod: 55);

            Assert.AreEqual(55, snowLoad.ReturnPeriod, "Return period should be proper.");
        }

        #endregion // ReturnPeriod

        #region CalculateSnowLoads

        [Test()]
        [Description("Check the snow load calculations.")]
        public void SnowLoadTest_FirstZone_Success()
        {
            var buildingSite = new BuildingSiteImplementation(10);

            buildingSite.AltitudeAboveSea = 350;
            buildingSite.CurrentZone = Zones.FirstZone;
            buildingSite.CurrentTopography = Topographies.Normal;
            buildingSite.CalculateExposureCoefficient();

            var snowLoad = new SnowLoad(buildingSite);
            snowLoad.CalculateSnowLoad();

            Assert.AreEqual(1.05, Math.Round(snowLoad.DefaultCharacteristicSnowLoad, 3), "DefaultCharacteristicSnowLoad has wrong calculations.");
            Assert.AreEqual(0.648, Math.Round(snowLoad.VariationCoefficient, 3), "VariationCoefficient has wrong calculations.");
            Assert.AreEqual(1.05, Math.Round(snowLoad.SnowLoadForSpecificReturnPeriod, 3), "SnowLoadForSpecificReturnPeriod has wrong calculations.");
            Assert.AreEqual(2, Math.Round(snowLoad.ExceptionalSnowLoadCoefficient, 3), "ExceptionalSnowLoadCoefficient has wrong calculations.");
            Assert.AreEqual(2.10, Math.Round(snowLoad.DesignExceptionalSnowLoadForSpecificReturnPeriod, 3), "DesignExceptionalSnowLoadForSpecificReturnPeriod has wrong calculations.");
        }

        [Test()]
        [Description("Check the snow load calculations.")]
        public void SnowLoadTest_SecondZone_Windswept_Success()
        {
            var buildingSite = new BuildingSiteImplementation(10);

            buildingSite.CurrentZone = Zones.SecondZone;
            buildingSite.CurrentTopography = Topographies.Windswept;
            buildingSite.CalculateExposureCoefficient();

            var snowLoad = new SnowLoad(buildingSite, returnPeriod: 80);
            snowLoad.CalculateSnowLoad();

            Assert.AreEqual(0.9, Math.Round(snowLoad.DefaultCharacteristicSnowLoad, 3), "DefaultCharacteristicSnowLoad has wrong calculations.");
            Assert.AreEqual(0.7, Math.Round(snowLoad.VariationCoefficient, 3), "VariationCoefficient has wrong calculations.");
            Assert.AreEqual(0.983, Math.Round(snowLoad.SnowLoadForSpecificReturnPeriod, 3), "SnowLoadForSpecificReturnPeriod has wrong calculations.");
            Assert.AreEqual(2, Math.Round(snowLoad.ExceptionalSnowLoadCoefficient, 3), "ExceptionalSnowLoadCoefficient has wrong calculations.");
            Assert.AreEqual(1.965, Math.Round(snowLoad.DesignExceptionalSnowLoadForSpecificReturnPeriod, 3), "DesignExceptionalSnowLoadForSpecificReturnPeriod has wrong calculations.");
        }

        #endregion // CalculateSnowLoads
    }
}