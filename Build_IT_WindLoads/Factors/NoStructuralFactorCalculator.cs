using Build_IT_WindLoads.Factors.Interfaces;

namespace Build_IT_WindLoads.Factors
{
    public class NoStructuralFactorCalculator : IStructuralFactorCalculator
    {
        public double GetStructuralFactor(bool calculate = true)
            => 1.0;
    }
}