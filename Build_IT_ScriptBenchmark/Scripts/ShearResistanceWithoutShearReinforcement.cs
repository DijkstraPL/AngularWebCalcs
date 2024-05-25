using BenchmarkDotNet.Attributes;
using Build_IT_NCalc.Units;
using Build_IT_ScriptInterpreter.Diagrams.Nodes;
using Build_IT_ScriptInterpreter.Diagrams.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Build_IT_ScriptBenchmark.Scripts
{
    [MemoryDiagnoser]
    public class ShearResistanceWithoutShearReinforcement
    {
        private static Node _node = PrepareNode();
        private static InputParameter<ValueUnit> _parameterV_Ed_;
        private static InputParameter<ValueUnit> _parameterf_ck_;
        private static InputParameter<ValueUnit> _parameterb_w_;
        private static InputParameter<ValueUnit> _parameterd;
        private static InputParameter<ValueUnit> _parameterA_sl_;
        private static InputParameter<ValueUnit> _parameterN_Ed_;
        private static InputParameter<ValueUnit> _parameterA_c_;
        private static InputParameter<ValueUnit> _parameterk_1_;
        private static InputParameter<ValueUnit> _parameterγ_c_;

        private static Node PrepareNode()
        {
            _parameterV_Ed_ = new InputParameter<ValueUnit>("V_Ed_");
            _parameterf_ck_ = new InputParameter<ValueUnit>("f_ck_");
            _parameterb_w_ = new InputParameter<ValueUnit>("b_w_");
            _parameterd = new InputParameter<ValueUnit>("d");
            _parameterA_sl_ = new InputParameter<ValueUnit>("A_sl_");
            _parameterN_Ed_ = new InputParameter<ValueUnit>("N_Ed_");
            _parameterA_c_ = new InputParameter<ValueUnit>("A_c_");
            _parameterk_1_ = new InputParameter<ValueUnit>("k_1_");
            _parameterγ_c_ = new InputParameter<ValueUnit>("γ_c_");

            var parameterC_Rd_c_ = new CalculationParameter("C_Rd,c_", "0.18/[γ_c_]");
            var parameterk = new CalculationParameter("k", "Min(1+Sqrt(Unit(200,'mm')/[d]),2)");
            var parameterρ_l_ = new CalculationParameter("ρ_l_", "Min(0.02,[A_sl_]/([b_w_]*[d]))");
            var parameterf_cd_ = new CalculationParameter("f_cd_", "[f_ck_]/[γ_c_]");
            var parameterσ_cp_ = new CalculationParameter("σ_cp_", "Min([N_Ed_]/[A_c_],0.2*[f_cd_])");
            var parameterv_min_ = new CalculationParameter("v_min_", "Unit(0.035,'MPa')*Pow([k],3/2)*Pow(([f_ck_]/Unit(1,'MPa')),1/2)");
            var parameterV_Rd_c_ = new CalculationParameter("V_Rd,c_", "Max(([v_min_]+[k_1_]*[σ_cp_])*[b_w_]*[d]," +
                "(Unit(1,'MPa')*[C_Rd,c_]*[k]*Pow(100*[ρ_l_]*[f_ck_]/Unit(1,'MPa'),1/3)+[k_1_]*[σ_cp_])*[b_w_]*[d])" +
                "/1000");
            var parameterResistance = new CalculationParameter("Resistance", "[V_Ed_]/[V_Rd,c_]*100");

            var node = Node.Builder(b =>
             b.Data.With(_parameterV_Ed_)
                .With(_parameterf_ck_)
                .With(_parameterb_w_)
                .With(_parameterd)
                .With(_parameterA_sl_)
                .With(_parameterN_Ed_)
                .With(_parameterA_c_)
                .With(_parameterk_1_)
                .With(_parameterγ_c_)
             .Equation.WithParameter(parameterC_Rd_c_)
             .Equation.WithParameter(parameterk)
             .Equation.WithParameter(parameterρ_l_)
             .Equation.WithParameter(parameterf_cd_)
             .Equation.WithParameter(parameterσ_cp_)
             .Equation.WithParameter(parameterv_min_)
             .Equation.WithParameter(parameterV_Rd_c_)
             .Equation.WithParameter(parameterResistance)
               ).BuildInitialized();

            return node;
        }

        [Benchmark]
        public void CalculateWithNodeGeneration(  )
        {
            Random random = new Random();
            double V_Ed_Value = random.Next(10,200);
            double f_ck_Value = random.Next(10,40);
            double b_w_Value = random.Next(100,400);
            double dValue = random.Next(500,800);
            double A_sl_Value = random.Next(500, 800);
            double N_Ed_Value = random.Next(50, 800);
            double A_c_Value = random.Next(150000, 200000);

            var V_Ed_ = new ValueUnit(V_Ed_Value, new CustomUnit("kN", 1));
            var f_ck_ = new ValueUnit(f_ck_Value, new CustomUnit("MPa", 1));
            var b_w_ = new ValueUnit(b_w_Value, new CustomUnit("mm", 1));
            var d = new ValueUnit(dValue, new CustomUnit("mm", 1));
            var A_sl_ = new ValueUnit(A_sl_Value, new CustomUnit("mm", 2));
            var N_Ed_ = new ValueUnit(N_Ed_Value, new CustomUnit("N", 1));
            var A_c_ = new ValueUnit(A_c_Value, new CustomUnit("mm", 2));
            var k_1_ = new ValueUnit(0.15);
            var γ_c_ = new ValueUnit(1.4);

            var node = PrepareNode();


            node.SetValue("V_Ed_", V_Ed_);
            node.SetValue("f_ck_", f_ck_);
            node.SetValue("b_w_", b_w_);
            node.SetValue("d", d);
            node.SetValue("A_sl_", A_sl_);
            node.SetValue("N_Ed_", N_Ed_);
            node.SetValue("A_c_", A_c_);
            node.SetValue("k_1_", k_1_);
            node.SetValue("γ_c_", γ_c_);

            var calculatedNodes = node.CalculateNode();
        }

        [Benchmark]
        public void CalculateWithPreGeneratedNode()
        {
            Random random = new Random();
            double V_Ed_Value = random.Next(10, 200);
            double f_ck_Value = random.Next(10, 40);
            double b_w_Value = random.Next(100, 400);
            double dValue = random.Next(500, 800);
            double A_sl_Value = random.Next(500, 800);
            double N_Ed_Value = random.Next(50, 800);
            double A_c_Value = random.Next(150000, 200000);

            var V_Ed_ = new ValueUnit(V_Ed_Value, new CustomUnit("kN", 1));
            var f_ck_ = new ValueUnit(f_ck_Value, new CustomUnit("MPa", 1));
            var b_w_ = new ValueUnit(b_w_Value, new CustomUnit("mm", 1));
            var d = new ValueUnit(dValue, new CustomUnit("mm", 1));
            var A_sl_ = new ValueUnit(A_sl_Value, new CustomUnit("mm", 2));
            var N_Ed_ = new ValueUnit(N_Ed_Value, new CustomUnit("N", 1));
            var A_c_ = new ValueUnit(A_c_Value, new CustomUnit("mm", 2));
            var k_1_ = new ValueUnit(0.15);
            var γ_c_ = new ValueUnit(1.4);

            _node.SetValue("V_Ed_", V_Ed_);
            _node.SetValue("f_ck_", f_ck_);
            _node.SetValue("b_w_", b_w_);
            _node.SetValue("d", d);
            _node.SetValue("A_sl_", A_sl_);
            _node.SetValue("N_Ed_", N_Ed_);
            _node.SetValue("A_c_", A_c_);
            _node.SetValue("k_1_", k_1_);
            _node.SetValue("γ_c_", γ_c_);

            var calculatedNodes = _node.CalculateNode();
        }


        [Benchmark]
        public void CalculateNodeWithPreGeneratedNodeWithoutUsingLambda()
        {
            Random random = new Random();
            double V_Ed_Value = random.Next(10, 200);
            double f_ck_Value = random.Next(10, 40);
            double b_w_Value = random.Next(100, 400);
            double dValue = random.Next(500, 800);
            double A_sl_Value = random.Next(500, 800);
            double N_Ed_Value = random.Next(50, 800);
            double A_c_Value = random.Next(150000, 200000);

            var V_Ed_ = new ValueUnit(V_Ed_Value, new CustomUnit("kN", 1));
            var f_ck_ = new ValueUnit(f_ck_Value, new CustomUnit("MPa", 1));
            var b_w_ = new ValueUnit(b_w_Value, new CustomUnit("mm", 1));
            var d = new ValueUnit(dValue, new CustomUnit("mm", 1));
            var A_sl_ = new ValueUnit(A_sl_Value, new CustomUnit("mm", 2));
            var N_Ed_ = new ValueUnit(N_Ed_Value, new CustomUnit("N", 1));
            var A_c_ = new ValueUnit(A_c_Value, new CustomUnit("mm", 2));
            var k_1_ = new ValueUnit(0.15);
            var γ_c_ = new ValueUnit(1.4);

            _node.SetValue("V_Ed_", V_Ed_);
            _node.SetValue("f_ck_", f_ck_);
            _node.SetValue("b_w_", b_w_);
            _node.SetValue("d", d);
            _node.SetValue("A_sl_", A_sl_);
            _node.SetValue("N_Ed_", N_Ed_);
            _node.SetValue("A_c_", A_c_);
            _node.SetValue("k_1_", k_1_);
            _node.SetValue("γ_c_", γ_c_);

            var calculatedNodes = _node.CalculateNodeNoLambda();
        }

        [Benchmark]
        public void CalculateWithNodeGenerationWithoutUsingLambda()
        {
            Random random = new Random();
            double V_Ed_Value = random.Next(10, 200);
            double f_ck_Value = random.Next(10, 40);
            double b_w_Value = random.Next(100, 400);
            double dValue = random.Next(500, 800);
            double A_sl_Value = random.Next(500, 800);
            double N_Ed_Value = random.Next(50, 800);
            double A_c_Value = random.Next(150000, 200000);

            var V_Ed_ = new ValueUnit(V_Ed_Value, new CustomUnit("kN", 1));
            var f_ck_ = new ValueUnit(f_ck_Value, new CustomUnit("MPa", 1));
            var b_w_ = new ValueUnit(b_w_Value, new CustomUnit("mm", 1));
            var d = new ValueUnit(dValue, new CustomUnit("mm", 1));
            var A_sl_ = new ValueUnit(A_sl_Value, new CustomUnit("mm", 2));
            var N_Ed_ = new ValueUnit(N_Ed_Value, new CustomUnit("N", 1));
            var A_c_ = new ValueUnit(A_c_Value, new CustomUnit("mm", 2));
            var k_1_ = new ValueUnit(0.15);
            var γ_c_ = new ValueUnit(1.4);

            var node = PrepareNode();


            node.SetValue("V_Ed_", V_Ed_);
            node.SetValue("f_ck_", f_ck_);
            node.SetValue("b_w_", b_w_);
            node.SetValue("d", d);
            node.SetValue("A_sl_", A_sl_);
            node.SetValue("N_Ed_", N_Ed_);
            node.SetValue("A_c_", A_c_);
            node.SetValue("k_1_", k_1_);
            node.SetValue("γ_c_", γ_c_);

            var calculatedNodes = node.CalculateNodeNoLambda();
        }

    }
}
