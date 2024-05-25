using Build_IT_CommonTools.Attributes;
using Build_IT_WindLoads.BuildingData.Interfaces;
using System;
using System.Collections.Generic;

namespace Build_IT_WindLoads.BuildingData
{
    public abstract class Structure : IStructure
    {
        #region Properties

        /// <summary>
        /// d
        /// </summary>
        [Abbreviation("d")]
        [Unit("m")]
        public double Length { get; protected set; }

        /// <summary>
        /// b
        /// </summary>
        [Abbreviation("b")]
        [Unit("m")]
        public double Width { get; protected set; }

        /// <summary>
        /// h
        /// </summary>
        [Abbreviation("h")]
        [Unit("m")]
        public double Height { get; protected set; }

        /// <summary>
        /// e
        /// </summary>
        [Abbreviation("e")]
        [Unit("m")]
        public double EdgeDistance 
            => Math.Min(Width, 2 * Height);
        
        public IDictionary<Field, double> Areas { get; }

        #endregion // Properties

        #region Constructors
      
        protected Structure()
        {
            Areas = new Dictionary<Field, double>();
        }

        #endregion // Constructors

        #region Public_Methods

        /// <summary>
        /// PN-EN 1991-1-4 Fig.6.1
        /// </summary>
        /// <returns></returns>
        public abstract double GetReferenceHeight();

        #endregion // Public_Methods
    }
}
