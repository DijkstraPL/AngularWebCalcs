using Build_IT_WindLoads.DynamicCharacteristics.Enums;
using Build_IT_WindLoads.Factors.Interfaces;
using System;

namespace Build_IT_WindLoads.DynamicCharacteristics
{
    /// <summary>
    /// ẟ_s
    /// </summary>
    /// <remarks>[PN-EN 1991-1-4 Tab.F.2]</remarks>
    public class LogarithmicDecrementOfStructuralDamping : IFactor
    {
        #region Fields

        private readonly StructuralType _structuralType;

        #endregion // Fields

        #region Constructorsx

        public LogarithmicDecrementOfStructuralDamping(StructuralType structuralType)
        {
            _structuralType = structuralType;
        }

        #endregion // Constructors

        #region Public_Methods

        public double GetFactor()
        {
            switch (_structuralType)
            {
                case StructuralType.ReinforcementConcreteBuilding:
                    return 0.1;
                case StructuralType.SteelBuilding:
                    return 0.05;
                case StructuralType.MixedStructuresConcrete_Steel:
                    return 0.08;
                case StructuralType.ReinforcementConcreteTowerOrChimney:
                    return 0.03;
                default:
                    throw new ArgumentException(nameof(_structuralType));
            }
        }

        #endregion // Public_Methods
    }
}
