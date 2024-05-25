using Build_IT_CommonTools.Attributes;
using Build_IT_SnowLoads.Enums;
using Build_IT_SnowLoads.Interfaces;
using System;

namespace Build_IT_SnowLoads
{
    /// <summary>
    /// Class for calculation of snow load on the ground.
    /// </summary>
    /// <example>
    /// <code>
    /// class TestClass 
    /// {
    ///     static void Main() 
    ///     {
    ///         BuildingSite buildingSite = new BuildingSite();
    ///          SnowLoad snowLoad = new SnowLoad(buildingSite, DesignSituation.A, false);
    ///          snowLoad.CalculateSnowLoad();
    ///     }
    /// }
    /// </code>
    /// </example>
    public class SnowLoad : ICalculatable, ISnowLoad
    {
        /// <summary>
        /// Current design situation enumerator - <see cref="DesignSituation"/>.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Table A.1]</remarks>
        public DesignSituation CurrentDesignSituation { get; set; }

        private bool excepctionalSituation;
        /// <summary>
        /// Is situation for calculations exceptional.
        /// </summary>
        /// <seealso cref="CurrentDesignSituation"/>
        public bool ExcepctionalSituation
        {
            get { return excepctionalSituation; }
            private set
            {
                if (CurrentDesignSituation != DesignSituation.A)
                    excepctionalSituation = value;
                else
                    excepctionalSituation = false;
            }
        }

        private double snowDensity = 2;
        /// <summary>
        /// Density of the snow.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Table E.1]</remarks>
        /// <exception cref="ArgumentOutOfRangeException">Snow density can't differ those values {1,2,3.5-3.5,4)</exception>
        [Abbreviation("gamma")]
        [Unit("kN/m3")]
        public double SnowDensity
        {
            get { return snowDensity; }
            private set
            {
                switch (value)
                {
                    case 1:
                    case 2:
                    case 4:
                        snowDensity = value;
                        break;
                    default:
                        if (2.5 <= value && value <= 3.5)
                            snowDensity = value;
                        else
                            throw new ArgumentOutOfRangeException(nameof(SnowDensity), "Snow density can't be this value.");
                        break;
                }
            }
        }

        /// <summary>
        /// Characteristic value of snow on the ground at the relevant site.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Table NB.1]</remarks>
        [Abbreviation("s_k")]
        [Unit("kN/m2")]
        public double DefaultCharacteristicSnowLoad { get; private set; }

        /// <summary>
        /// Characteristic ground snow load with a return period of n years.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 (D.1)]</remarks>
        [Abbreviation("s_n")]
        [Unit("kN/m2")]
        public double SnowLoadForSpecificReturnPeriod { get; private set; }

        /// <summary>
        /// Coefficient of variation of annual maximum snow load.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 (NB.3)]</remarks>
        [Abbreviation("V")]
        [Unit("")]
        public double VariationCoefficient { get; private set; }

        private int returnPeriod = 50;
        /// <summary>
        /// Years of return period.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 D.(2)]</remarks>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when value is less than 5.</exception>
        [Abbreviation("n")]
        [Unit("year")]
        public int ReturnPeriod
        {
            get { return returnPeriod; }
            private set
            {
                if (value < 5)
                    throw new ArgumentOutOfRangeException("Return period shouldn't be less than 5 years.");
                returnPeriod = value;
            }
        }

        /// <summary>
        /// Coefficient for exceptional snow loads.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 4.3.(1)]</remarks>
        [Abbreviation("C_esl")]
        [Unit("")]
        public double ExceptionalSnowLoadCoefficient { get; private set; }

        /// <summary>
        /// The design value of exceptional snow load on the ground for the given location. 
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 (4.1)]</remarks>
        [Abbreviation("s_Ad")]
        [Unit("kN/m2")]
        public double DesignExceptionalSnowLoadForSpecificReturnPeriod { get; private set; }

        /// <summary>
        /// Instance of class implementing <see cref="IBuildingSite"/>.
        /// </summary>
        public IBuildingSite BuildingSite { get; private set; }

        public const DesignSituation DefaultDesignSituation = DesignSituation.A;
        public const bool DefaultExcepctionalSituation = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="SnowLoad"/> class.
        /// </summary>
        /// <param name="buildingSite">Set instance of a class implementing <see cref="IBuildingSite"/> for <see cref="BuildingSite"/>.</param>
        /// <param name="currentDesignSituation">Set <see cref="CurrentDesignSituation"/>.</param>
        /// <param name="excepctionalSituation">Set <see cref="ExcepctionalSituation"/>.</param>
        public SnowLoad(IBuildingSite buildingSite,
            DesignSituation currentDesignSituation = DefaultDesignSituation,
            bool excepctionalSituation = DefaultExcepctionalSituation)
        {
            BuildingSite = buildingSite;
            CurrentDesignSituation = currentDesignSituation;
            ExcepctionalSituation = excepctionalSituation;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SnowLoad"/> class.
        /// </summary>
        /// <param name="buildingSite">Set instance of a class implementing <see cref="IBuildingSite"/> for <see cref="BuildingSite"/>.</param>
        /// <param name="returnPeriod">Set <see cref="ReturnPeriod"/> in years</param>
        /// <param name="currentDesignSituation">Set <see cref="CurrentDesignSituation"/>.</param>
        /// <param name="excepctionalSituation">Set <see cref="ExcepctionalSituation"/>.</param>
        public SnowLoad(IBuildingSite buildingSite, int returnPeriod,
            DesignSituation currentDesignSituation = DesignSituation.A,
            bool excepctionalSituation = false) : this(buildingSite, currentDesignSituation, excepctionalSituation)
        {
            ReturnPeriod = returnPeriod ;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SnowLoad"/> class.
        /// </summary>
        /// <param name="buildingSite">Set instance of a class implementing <see cref="IBuildingSite"/> for <see cref="BuildingSite"/>.</param>
        /// <param name="snowDensity">Set <see cref="SnowDensity"/> in kN/m3.</param>
        /// <param name="currentDesignSituation">Set <see cref="CurrentDesignSituation"/>.</param>
        /// <param name="excepctionalSituation">Set <see cref="ExcepctionalSituation"/>.</param>
        public SnowLoad(IBuildingSite buildingSite, double snowDensity,
            DesignSituation currentDesignSituation = DesignSituation.A,
            bool excepctionalSituation = false) : this(buildingSite, currentDesignSituation, excepctionalSituation)
        {
            SnowDensity = snowDensity;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SnowLoad"/> class.
        /// </summary>
        /// <param name="buildingSite">Set instance of a class implementing <see cref="IBuildingSite"/> for <see cref="BuildingSite"/>.</param>
        /// <param name="snowDensity">Set <see cref="SnowDensity"/> in kN/m3.</param>
        /// <param name="returnPeriod">Set <see cref="ReturnPeriod"/> in years</param>
        /// <param name="currentDesignSituation">Set <see cref="CurrentDesignSituation"/>.</param>
        /// <param name="excepctionalSituation">Set <see cref="ExcepctionalSituation"/>.</param>
        public SnowLoad(IBuildingSite buildingSite, double snowDensity, int returnPeriod,
        DesignSituation currentDesignSituation = DesignSituation.A,
        bool excepctionalSituation = false) : this(buildingSite, currentDesignSituation, excepctionalSituation)
        {
            SnowDensity = snowDensity;
            ReturnPeriod = returnPeriod;
        }

        /// <summary>
        /// Calculate characteristic snow load on ground.
        /// </summary>
        /// <seealso cref="DefaultCharacteristicSnowLoad"/>
        /// <seealso cref="VariationCoefficient"/>
        /// <seealso cref="SnowLoadForSpecificReturnPeriod"/>
        /// <seealso cref="ExcepctionalSituation"/>
        /// <seealso cref="DesignExceptionalSnowLoadForSpecificReturnPeriod"/>
        public void CalculateSnowLoad()
        {
            SetCharacteristicSnowLoad();
            SetVariationCoefficient();
            SetCharacteristicSnowLoadForSpecificReturnPeriod();
            SetExceptionalSnowLoadCoefficient();
            SetDesignExceptionalSnowLoadForSpecificReturnPeriod();
        }

        /// <summary>
        /// Set snow load base on terrain zone.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Table NB.1]</remarks>
        /// <param name="ownValue">Value of the snow load set to <see cref="DefaultCharacteristicSnowLoad"/> in kN/m2.</param>
        /// <exception cref="ArgumentException" >Thrown when no zone specified.</exception>
        private void SetCharacteristicSnowLoad(double ownValue = 0)
        {
            switch (BuildingSite.CurrentZone)
            {
                case Zones.None:
                    DefaultCharacteristicSnowLoad = ownValue;
                    break;
                case Zones.FirstZone:
                    DefaultCharacteristicSnowLoad = Math.Max(0.007 * BuildingSite.AltitudeAboveSea - 1.4, 0.7);
                    break;
                case Zones.BetweenFirst_Second:
                    DefaultCharacteristicSnowLoad = (Math.Max(0.007 * BuildingSite.AltitudeAboveSea - 1.4, 0.7) + 0.9) / 2;
                    break;
                case Zones.SecondZone:
                    DefaultCharacteristicSnowLoad = 0.9;
                    break;
                case Zones.BetweenSecond_Third:
                    DefaultCharacteristicSnowLoad = (Math.Max(0.006 * BuildingSite.AltitudeAboveSea - 0.6, 1.2) + 0.9) / 2;
                    break;
                case Zones.ThirdZone:
                    DefaultCharacteristicSnowLoad = Math.Max(0.006 * BuildingSite.AltitudeAboveSea - 0.6, 1.2);
                    break;
                case Zones.BetweenThird_Fourth:
                    DefaultCharacteristicSnowLoad = (Math.Max(0.006 * BuildingSite.AltitudeAboveSea - 0.6, 1.2) + 1.6) / 2;
                    break;
                case Zones.FourthZone:
                    DefaultCharacteristicSnowLoad = 1.6;
                    break;
                case Zones.BetweenThird_Fifth:
                    DefaultCharacteristicSnowLoad = (Math.Max(0.93 * Math.Pow(Math.E, 0.00134 * BuildingSite.AltitudeAboveSea), 2) + 
                        (Math.Max(0.006 * BuildingSite.AltitudeAboveSea - 0.6, 1.2))) / 2;
                    break;
                case Zones.FifthZone:
                    DefaultCharacteristicSnowLoad = Math.Max(0.93 * Math.Pow(Math.E, 0.00134 * BuildingSite.AltitudeAboveSea), 2);
                    break;
                default:
                    throw new ArgumentException("There is no zone specified.");
            }
        }

        /// <summary>
        /// Calculate coefficient of variation of annual maximum snow load.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 (NB.3)]</remarks>
        private void SetVariationCoefficient()
        {
            if (BuildingSite.AltitudeAboveSea < 300)
                VariationCoefficient = 0.7;
            else
                VariationCoefficient = 0.8 * Math.Pow(Math.E, -0.0006 * BuildingSite.AltitudeAboveSea);
        }

        /// <summary>
        /// Calculate characteristic snow load for specific return period.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 (D.1)]</remarks>
        private void SetCharacteristicSnowLoadForSpecificReturnPeriod()
        {
            if (ReturnPeriod == 50)
                SnowLoadForSpecificReturnPeriod = DefaultCharacteristicSnowLoad;
            else
                SnowLoadForSpecificReturnPeriod = DefaultCharacteristicSnowLoad *
                    ((1 - VariationCoefficient * Math.Sqrt(6) / Math.PI * (Math.Log(-Math.Log(1 - 1.0 / ReturnPeriod)) + 0.57722)) /
                    (1 + 2.5923 * VariationCoefficient));
        }

        /// <summary>
        /// Calculate exceptional snow load coefficient.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 4.3.(1)]</remarks>
        private void SetExceptionalSnowLoadCoefficient()
        {
            ExceptionalSnowLoadCoefficient = 2;
        }

        /// <summary>
        /// Calculate exceptional snow load value..
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 (4.1)]</remarks>
        private void SetDesignExceptionalSnowLoadForSpecificReturnPeriod()
        {
            DesignExceptionalSnowLoadForSpecificReturnPeriod =
                ExceptionalSnowLoadCoefficient * SnowLoadForSpecificReturnPeriod;
        }
    }

    /// <summary>
    /// Current design situation.
    /// </summary>
    /// <remarks>[PN-EN 1991-1-3 Table A.1]</remarks>
    public enum DesignSituation
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
