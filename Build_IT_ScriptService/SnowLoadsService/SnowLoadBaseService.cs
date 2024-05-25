using Build_IT_SnowLoads;
using Build_IT_SnowLoads.Enums;
using Build_IT_SnowLoads.Interfaces;
using System;

namespace Build_IT_ScriptService.SnowLoadsService
{
    public abstract class SnowLoadBaseService : ServiceBase
    {
        #region Properties
        
        public Property<Zones> Zone { get; } =
            new Property<Zones>("Zone",
            v => Enum.Parse<Zones>(v.ToString()));
        public Property<Topographies> Topography { get; } =
            new Property<Topographies>("Topography",
            v => Enum.Parse<Topographies>(v.ToString()));
        public Property<double> AltitudeAboveSea { get; } =
            new Property<double>("AltitudeAboveSea",
                v => Convert.ToDouble(v));
        public Property<double> ExposureCoefficient { get; } =
            new Property<double>("ExposureCoefficient",
                v => Convert.ToDouble(v));
        public Property<double> SnowDensity { get; } =
            new Property<double>("SnowDensity",
                v => Convert.ToDouble(v), required: false);
        public Property<int> ReturnPeriod { get; } =
            new Property<int>("ReturnPeriod",
                v => Convert.ToInt32(v), required: false);
        public Property<DesignSituation> DesignSituation { get; } =
            new Property<DesignSituation>("DesignSituation",
            v => Enum.Parse<DesignSituation>(v.ToString()), required: false);
        public Property<bool> ExceptionalSituation { get; } =
            new Property<bool>("ExceptionalSituation",
            v => v.ToString() == "true", required: false);
        public Property<double> InternalTemperature { get; } =
            new Property<double>("InternalTemperature",
            v => Convert.ToDouble(v), required: false);
        public Property<double> OverallHeatTransferCoefficient { get; } =
            new Property<double>("OverallHeatTransferCoefficient",
            v => Convert.ToDouble(v), required: false);

        #endregion // Properties

        #region Protected_Methods

        protected BuildingSite GetBuildingSite()
        {
            if (ExposureCoefficient.HasValue)
                return new BuildingSite(
                    Zone.Value, Topography.Value,
                    AltitudeAboveSea.Value, ExposureCoefficient.Value);
            return new BuildingSite(
                Zone.Value, Topography.Value, AltitudeAboveSea.Value);
        }
        protected SnowLoad GetSnowLoad(IBuildingSite buildingSite)
        {
            if (SnowDensity.HasValue &&
                ReturnPeriod.HasValue)
                return new SnowLoad(buildingSite,
                    SnowDensity.Value,
                    ReturnPeriod.Value,
                    DesignSituation.HasValue ? DesignSituation.Value : SnowLoad.DefaultDesignSituation,
                    ExceptionalSituation.HasValue ? ExceptionalSituation.Value : SnowLoad.DefaultExcepctionalSituation);
            else if (SnowDensity.HasValue)
                return new SnowLoad(buildingSite,
                     SnowDensity.Value,
                     DesignSituation.HasValue ? DesignSituation.Value : SnowLoad.DefaultDesignSituation,
                     ExceptionalSituation.HasValue ? ExceptionalSituation.Value : SnowLoad.DefaultExcepctionalSituation);
            else if (ReturnPeriod.HasValue)
                return new SnowLoad(buildingSite,
                     ReturnPeriod.Value,
                     DesignSituation.HasValue ? DesignSituation.Value : SnowLoad.DefaultDesignSituation,
                     ExceptionalSituation.HasValue ? ExceptionalSituation.Value : SnowLoad.DefaultExcepctionalSituation);
            else
                return new SnowLoad(buildingSite,
                     DesignSituation.HasValue ? DesignSituation.Value : SnowLoad.DefaultDesignSituation,
                     ExceptionalSituation.HasValue ? ExceptionalSituation.Value : SnowLoad.DefaultExcepctionalSituation);
        }
        protected Building GetBuilding(ISnowLoad snowLoad)
        {
            if (InternalTemperature.HasValue && OverallHeatTransferCoefficient.HasValue)
                return new Building(snowLoad, InternalTemperature.Value,
                    OverallHeatTransferCoefficient.Value);
            else
                return new Building(snowLoad);
        }

        #endregion // Protected_Methods
    }
}
