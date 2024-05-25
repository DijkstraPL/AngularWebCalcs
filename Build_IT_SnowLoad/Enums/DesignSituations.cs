namespace Build_IT_SnowLoads.Enums
{
    /// <summary>
    /// Current design situation.
    /// </summary>
    /// <remarks>[PN-EN 1991-1-3 Table A.1]</remarks>
    public enum DesignSituations
    {
        /// <summary>
        /// No exceptional falls.
        /// No exceptional drift.
        /// </summary>
        A,
        /// <summary>
        /// Exceptional falls.
        /// No exceptional drift.
        /// </summary>
        B1,
        /// <summary>
        /// No exceptional falls.
        /// Exceptional drift.
        /// </summary>
        B2,
        /// <summary>
        /// Exceptional falls.
        /// Exceptional drift.
        /// </summary>
        B3,
    }
}
