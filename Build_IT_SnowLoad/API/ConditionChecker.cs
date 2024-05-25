using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_SnowLoads.API
{
    /// <summary>
    /// Class for checking if conditions are proper.
    /// </summary>
    /// <remarks>[PN-EN 1991-1-3]</remarks>
    public static class ConditionChecker
    {
        /// <summary>
        /// Checks the conditions for current design situation.
        /// </summary>
        /// <param name="exceptionalSituation"><c>true</c> if situation is exceptional.</param>
        /// <param name="designSituation">Design situation to be checked.</param>
        /// <param name="annexB">If annex B is used.</param>
        /// <returns><c>true</c> if current design situation can be used in calculations.</returns>
        /// <remarks>[PN-EN 1991-1-3 Table A.1]</remarks>
        /// <example>
        /// <code>
        /// bool canBeUsedInCalculations = ConditionChecker.ForDesignSituation(true, DesignSituation.A, false);
        /// </code>
        /// </example>
        public static bool ForDesignSituation(bool exceptionalSituation, DesignSituation designSituation, bool annexB)
        {
            if (exceptionalSituation)
                switch (designSituation)
                {
                    case DesignSituation.A:
                        return false;
                    case DesignSituation.B1:
                        return !annexB;
                    case DesignSituation.B2:
                        return annexB;
                    case DesignSituation.B3:
                        return true;
                    default:
                        throw new NotSupportedException();
                }

            if (!annexB)
                return true;
            return false;
        }
    }
}
