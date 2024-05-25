
namespace Build_IT_Material
{
    public class Material
    {
        public ValueUnit YoungModulus { get;} = new ValueUnit("E_cm", "GPa");
        public ValueUnit Density { get; } = new ValueUnit("γ", "kN/m3");
        public ValueUnit ThermalExpansionCoefficient { get; } = new ValueUnit("l_x", "1/K");
    }
}
