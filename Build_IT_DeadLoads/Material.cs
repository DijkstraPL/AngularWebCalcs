using Build_IT_DeadLoads.Enums;

namespace Build_IT_DeadLoads
{
    public class Material
    {
        public string Name { get; }
        public double MinimumDensity { get; }
        public double MaximumDensity { get; }

        public LoadUnit LoadUnit { get; }

        public Material(string name ,double minimumDensity, double maximumDensity, LoadUnit loadUnit)
        {
            Name = name;
            MinimumDensity = minimumDensity;
            MaximumDensity = maximumDensity;
            LoadUnit = loadUnit;
        }
    }
}
