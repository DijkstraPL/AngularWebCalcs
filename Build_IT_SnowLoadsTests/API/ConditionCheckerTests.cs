using Build_IT_SnowLoads;
using Build_IT_SnowLoads.API;
using NUnit.Framework;

namespace Build_IT_SnowLoadsUnitTests.API
{
    [TestFixture]
    public class ConditionCheckerTests
    {
        #region ForDesignSituationTest
        
        [Test]
        public void ForDesignSituationTest_NotExceptional_NotAnnexB_ReturnsTrue()
        {
            bool result = ConditionChecker.ForDesignSituation(exceptionalSituation: false, DesignSituation.A, annexB: false);
            Assert.IsTrue(result);
        }

        [Test]
        public void ForDesignSituationTest_NotExceptional_AnnexB_ReturnsTrue()
        {
            bool result = ConditionChecker.ForDesignSituation(exceptionalSituation: false, DesignSituation.B3, annexB: true);
            Assert.IsFalse(result);
        }

        [Test]
        public void ForDesignSituationTest_SituationA_Exceptional_ReturnsFalse()
        {
            bool result = ConditionChecker.ForDesignSituation(exceptionalSituation: true, DesignSituation.A, annexB: false);
            Assert.IsFalse(result);
        }

        [Test]
        public void ForDesignSituationTest_SituationB1_Exceptional_NotAnnexB_ReturnsTrue()
        {
            bool result = ConditionChecker.ForDesignSituation(exceptionalSituation: true, DesignSituation.B1, annexB: false);
            Assert.IsTrue(result);
        }

        [Test]
        public void ForDesignSituationTest_SituationB1_Exceptional_AnnexB_ReturnsTrue()
        {
            bool result = ConditionChecker.ForDesignSituation(exceptionalSituation: true, DesignSituation.B1, annexB: true);
            Assert.IsFalse(result);
        }

        [Test]
        public void ForDesignSituationTest_SituationB2_Exceptional_NotAnnexB_ReturnsFalse()
        {
            bool result = ConditionChecker.ForDesignSituation(exceptionalSituation: true, DesignSituation.B2, annexB: false);
            Assert.IsFalse(result);
        }

        [Test]
        public void ForDesignSituationTest_SituationB2_Exceptional_AnnexB_ReturnsFalse()
        {
            bool result = ConditionChecker.ForDesignSituation(exceptionalSituation: true, DesignSituation.B2, annexB: true);
            Assert.IsTrue(result);
        }

        [Test]
        public void ForDesignSituationTest_SituationB3_Exceptional_ReturnsFalse()
        {
            bool result = ConditionChecker.ForDesignSituation(false, DesignSituation.B3, true);
            Assert.IsFalse(result);
        }

        #endregion // ForDesignSituationTest
    }
}