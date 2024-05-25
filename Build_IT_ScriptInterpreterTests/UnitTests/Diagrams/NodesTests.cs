using Build_IT_NCalc.Units;
using Build_IT_ScriptInterpreter.Diagrams;
using Build_IT_ScriptInterpreter.Diagrams.Nodes;
using Build_IT_ScriptInterpreter.Diagrams.Parameters;
using Build_IT_ScriptInterpreter.Expressions;
using NCalc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Build_IT_ScriptInterpreterTests.UnitTests.Diagrams
{
    [TestFixture]
    public class NodesTests
    {
        [Test]
        public void Test1()
        {
            var par0 = new InputParameter<int>("a", 2);
            var par1 = new CalculationParameter("b", "Unit(50,'m')/2", typeof(ValueUnit));

            var calcNode = Node.Builder(nb =>
                nb.Data.With(par0)
                .Equation.WithParameter(par1))
                .Build();

            var result = calcNode.CalculateNode();
            var calculatedParameters = result.CalculatedParameters;

            Assert.Multiple(() =>
            {
                Assert.That(calculatedParameters.Any(p => p.Name == "b" && p.Value.ToString() == "25m"));
            });
        }
        [Test]
        public void Test4()
        {
            var par0 = new InputParameter<int>("a", 2);
            var par1 = new CalculationParameter("b", "Unit([a],'m')", typeof(ValueUnit));

            var calcNode = Node.Builder(nb =>
                nb.Data.With(par0)
                .Equation.WithParameter(par1))
                .Build();

            var result = calcNode.CalculateNode();
            var calculatedParameters = result.CalculatedParameters;

            Assert.Multiple(() =>
            {
                Assert.That(calculatedParameters.Any(p => p.Name == "b" && p.Value.ToString() == "2m"));
            });
        }

        [Test]
        public void Test2()
        {
            var par0 = new InputParameter<int>("a", 24);
            var par1 = new CalculationParameter("b", "a+5", typeof(int));
            var par2 = new CalculationParameter("c", "b>a", typeof(bool));
            var par3 = new CalculationParameter("d", "b*2", typeof(int));
            var par4 = new CalculationParameter("e", "a*2", typeof(int));

            var calcNode = Node.Builder(nb =>
                nb.Data.With(par0)
                .Equation.WithParameter(par1)
                .If.Condition(par2)
                    .True(nb => 
                        nb.Equation.WithParameter(par3))
                    .False(nb => 
                        nb.Equation.WithParameter(par4)))
                .Build();
                

            var result = calcNode.CalculateNode();
            var calculatedParameters = result.CalculatedParameters;

            Assert.Multiple(() =>
            {
                Assert.That(calculatedParameters.Any(p => p.Name == "b" && p.Value.ToString() == "29"));
                Assert.That(calculatedParameters.Any(p => p.Name == "c" && p.Value.ToString() == "True"));
                Assert.That(calculatedParameters.Any(p => p.Name == "d" && p.Value.ToString() == "58"));
                Assert.That(calculatedParameters.All(p => p.Name != "e"));
            });
        }

        [Test]
        public void Test3()
        {
            var par0 = new InputParameter<int>("a");
            var par1 = new CalculationParameter("b", "a-1", typeof(int));
            var par2 = new CalculationParameter("c", "b>a", typeof(bool));
            var par3 = new CalculationParameter("d", "b*2", typeof(int));
            var par4 = new CalculationParameter("e", "a*2", typeof(int));

            var calcNode = Node.Builder(nb =>
                nb.Data.With(par0)
                .Equation.WithParameter(par1)
                .If.Condition(par2)
                    .True(nb =>
                        nb.Equation.WithParameter(par3))
                    .False(nb =>
                        nb.Equation.WithParameter(par4)))
                .BuildInitialized();


            par0.SetValue(1);
            var result = calcNode.CalculateNode();
            var calculatedParameters = result.CalculatedParameters;

            Assert.Multiple(() =>
            {
                Assert.That(calculatedParameters.Any(p => p.Name == "b" && p.Value.ToString() == "0"));
                Assert.That(calculatedParameters.Any(p => p.Name == "c" && p.Value.ToString() == "False"));
                Assert.That(calculatedParameters.Any(p => p.Name == "e" && p.Value.ToString() == "2"));
                Assert.That(calculatedParameters.All(p => p.Name != "d"));
            });
        }



        [Test]
        [TestCase(100,"kN",     30, "MPa",    240, "mm",  461, "mm",  339, "mm",  100, "N",    150000, "mm")]
        [TestCase(100000, "N",  30000, "kPa", 0.240, "m", 4.61, "dm", 3.39, "cm", 0.100, "kN", 0.150000, "m")]
        public void ShearResistanceWithoutShearReinforcementTest_Success(
            double V_Ed_Value, string V_Ed_Unit,
            double f_ck_Value, string f_ck_Unit,
            double b_w_Value, string b_w_Unit,
            double dValue, string dUnit,
            double A_sl_Value, string A_sl_Unit,
            double N_Ed_Value, string N_Ed_Unit,
            double A_c_Value, string A_c_Unit
            )
        {
                            var V_Ed_ = new ValueUnit(V_Ed_Value,  new CustomUnit(V_Ed_Unit, 1));
            var f_ck_ = new ValueUnit(f_ck_Value, new CustomUnit(f_ck_Unit, 1));
            var b_w_ = new ValueUnit(b_w_Value, new CustomUnit(b_w_Unit, 1));
            var d = new ValueUnit(dValue, new CustomUnit(dUnit, 1));
            var A_sl_ = new ValueUnit(A_sl_Value, new CustomUnit(A_sl_Unit, 2));
            var N_Ed_ = new ValueUnit(N_Ed_Value, new CustomUnit(N_Ed_Unit, 1));
            var A_c_ = new ValueUnit(A_c_Value, new CustomUnit(A_c_Unit, 2));
            var k_1_ = 0.15;
            var γ_c_ = 1.4;

            var parameterV_Ed_ = new InputParameter<ValueUnit>("V_Ed_", V_Ed_);
            var parameterf_ck_ = new InputParameter<ValueUnit>("f_ck_", f_ck_);
            var parameterb_w_ = new InputParameter<ValueUnit>("b_w_", b_w_);
            var parameterd = new InputParameter<ValueUnit>("d", d);
            var parameterA_sl_ = new InputParameter<ValueUnit>( "A_sl_", A_sl_);
            var parameterN_Ed_ = new InputParameter<ValueUnit>("N_Ed_", N_Ed_);
            var parameterA_c_ = new InputParameter<ValueUnit>( "A_c_", A_c_);
            var parameterk_1_ = new InputParameter<double>("k_1_", k_1_);
            var parameterγ_c_ = new InputParameter<double>("γ_c_", γ_c_);

            var parameterC_Rd_c_ = new CalculationParameter("C_Rd,c_", "0.18/[γ_c_]", typeof(double));
            var parameterk = new CalculationParameter("k", "Min(1+Sqrt(Unit(200,'mm')/[d]),2)", typeof(double));
            var parameterρ_l_ = new CalculationParameter("ρ_l_", "Min(0.02,[A_sl_]/([b_w_]*[d]))", typeof(ValueUnit));
            var parameterf_cd_ = new CalculationParameter( "f_cd_", "[f_ck_]/[γ_c_]", typeof(ValueUnit));
            var parameterσ_cp_ = new CalculationParameter( "σ_cp_", "Min([N_Ed_]/[A_c_],0.2*[f_cd_])", typeof(ValueUnit));
            var parameterv_min_ = new CalculationParameter( "v_min_", "Unit(0.035,'MPa')*Pow([k],3/2)*Pow(([f_ck_]/Unit(1,'MPa')),1/2)", typeof(ValueUnit));
            var parameterV_Rd_c_ = new CalculationParameter ("V_Rd,c_", "Max(([v_min_]+[k_1_]*[σ_cp_])*[b_w_]*[d]," +
                "(Unit(1,'MPa')*[C_Rd,c_]*[k]*Pow(100*[ρ_l_]*[f_ck_]/Unit(1,'MPa'),1/3)+[k_1_]*[σ_cp_])*[b_w_]*[d])", typeof(ValueUnit));
            var parameterResistance = new CalculationParameter("Resistance", "[V_Ed_]/[V_Rd,c_]*100", typeof(ValueUnit));

            var node= Node.Builder(b =>
             b.Data.With(parameterV_Ed_)
                .With(parameterf_ck_)
                .With(parameterb_w_)
                .With(parameterd)
                .With(parameterA_sl_)
                .With(parameterN_Ed_)
                .With(parameterA_c_)
                .With(parameterk_1_)
                .With(parameterγ_c_)
             .Equation.WithParameter(parameterC_Rd_c_)
             .Equation.WithParameter(parameterk)
             .Equation.WithParameter(parameterρ_l_)
             .Equation.WithParameter(parameterf_cd_)
             .Equation.WithParameter(parameterσ_cp_)
             .Equation.WithParameter(parameterv_min_)
             .Equation.WithParameter(parameterV_Rd_c_)
             .Equation.WithParameter(parameterResistance)
               ).BuildInitialized();


            var calculatedNodes = node.CalculateNode();
            var calculatedParameters = calculatedNodes.CalculatedParameters;

            var calculatedParameterC_Rd_c_ = calculatedParameters.First(p => p.Name == "C_Rd,c_");
            var calculatedParameterk = calculatedParameters.First(p => p.Name == "k");
            var calculatedParameterρ_l_ = calculatedParameters.First(p => p.Name == "ρ_l_");
            var calculatedParameterf_cd_ = calculatedParameters.First(p => p.Name == "f_cd_");
            var calculatedParameterσ_cp_ = calculatedParameters.First(p => p.Name == "σ_cp_");
            var calculatedParameterv_min_ = calculatedParameters.First(p => p.Name == "v_min_");
            var calculatedParameterV_Rd_c_ = calculatedParameters.First(p => p.Name == "V_Rd,c_");
            var calculatedParameterResistance = calculatedParameters.First(p => p.Name == "Resistance");

            Assert.Multiple(() =>
            {
                Assert.That(new ValueUnit(0.12857142857142859d).AreEqual(calculatedParameterC_Rd_c_.Value, 0.000001));
                Assert.That(new ValueUnit(1.6586649219387843).AreEqual(calculatedParameterk.Value, 0.000001));
                Assert.That(new ValueUnit(0.0030639913232104123).AreEqual(calculatedParameterρ_l_.Value, 0.000001));
                Assert.That(new ValueUnit(21.42857142857143, new CustomUnit("MPa")).AreEqual(calculatedParameterf_cd_.Value, 0.000001));
                Assert.That(new ValueUnit(0.0006666666666666667, new CustomUnit("MPa")).AreEqual(calculatedParameterσ_cp_.Value, 0.000001));
                Assert.That(new ValueUnit(0.40951202774487683, new CustomUnit("MPa")).AreEqual(calculatedParameterv_min_.Value, 0.000001));
                Assert.That(new ValueUnit(49.43661944290878, new CustomUnit("kN")).AreEqual(calculatedParameterV_Rd_c_.Value, 0.000001));
                Assert.That(new ValueUnit(202.27920340605743).AreEqual(calculatedParameterResistance.Value, 0.000001));
            });
        }

        [Test]
        [TestCase(100, "kN", 30, "MPa", 240, "mm", 461, "mm", 339, "mm", 100, "N", 150000, "mm")]
        [TestCase(100000, "N", 30000, "kPa", 0.240, "m", 4.61, "dm", 3.39, "cm", 0.100, "kN", 0.150000, "m")]
        public void ShearResistanceWithoutShearReinforcementTest_OnlyValueUnit_Success(
        double V_Ed_Value, string V_Ed_Unit,
        double f_ck_Value, string f_ck_Unit,
        double b_w_Value, string b_w_Unit,
        double dValue, string dUnit,
        double A_sl_Value, string A_sl_Unit,
        double N_Ed_Value, string N_Ed_Unit,
        double A_c_Value, string A_c_Unit
        )
        {
            var V_Ed_ = new ValueUnit(V_Ed_Value, new CustomUnit(V_Ed_Unit, 1));
            var f_ck_ = new ValueUnit(f_ck_Value, new CustomUnit(f_ck_Unit, 1));
            var b_w_ = new ValueUnit(b_w_Value, new CustomUnit(b_w_Unit, 1));
            var d = new ValueUnit(dValue, new CustomUnit(dUnit, 1));
            var A_sl_ = new ValueUnit(A_sl_Value, new CustomUnit(A_sl_Unit, 2));
            var N_Ed_ = new ValueUnit(N_Ed_Value, new CustomUnit(N_Ed_Unit, 1));
            var A_c_ = new ValueUnit(A_c_Value, new CustomUnit(A_c_Unit, 2));
            var k_1_ = new ValueUnit(0.15);
            var γ_c_ = new ValueUnit(1.4);

            var parameterV_Ed_ = new InputParameter<ValueUnit>("V_Ed_", V_Ed_);
            var parameterf_ck_ = new InputParameter<ValueUnit>("f_ck_", f_ck_);
            var parameterb_w_ = new InputParameter<ValueUnit>("b_w_", b_w_);
            var parameterd = new InputParameter<ValueUnit>("d", d);
            var parameterA_sl_ = new InputParameter<ValueUnit>("A_sl_", A_sl_);
            var parameterN_Ed_ = new InputParameter<ValueUnit>("N_Ed_", N_Ed_);
            var parameterA_c_ = new InputParameter<ValueUnit>("A_c_", A_c_);
            var parameterk_1_ = new InputParameter<ValueUnit>("k_1_", k_1_);
            var parameterγ_c_ = new InputParameter<ValueUnit>("γ_c_", γ_c_);

            var parameterC_Rd_c_ = new CalculationParameter("C_Rd,c_", "0.18/[γ_c_]");
            var parameterk = new CalculationParameter("k", "Min(1+Sqrt(Unit(200,'mm')/[d]),2)");
            var parameterρ_l_ = new CalculationParameter("ρ_l_", "Min(0.02,[A_sl_]/([b_w_]*[d]))");
            var parameterf_cd_ = new CalculationParameter("f_cd_", "[f_ck_]/[γ_c_]");
            var parameterσ_cp_ = new CalculationParameter("σ_cp_", "Min([N_Ed_]/[A_c_],0.2*[f_cd_])");
            var parameterv_min_ = new CalculationParameter("v_min_", "Unit(0.035,'MPa')*Pow([k],3/2)*Pow(([f_ck_]/Unit(1,'MPa')),1/2)");
            var parameterV_Rd_c_ = new CalculationParameter("V_Rd,c_", "Max(([v_min_]+[k_1_]*[σ_cp_])*[b_w_]*[d]," +
                "(Unit(1,'MPa')*[C_Rd,c_]*[k]*Pow(100*[ρ_l_]*[f_ck_]/Unit(1,'MPa'),1/3)+[k_1_]*[σ_cp_])*[b_w_]*[d])" 
                );
            var parameterResistance = new CalculationParameter("Resistance", "[V_Ed_]/[V_Rd,c_]*100");

            var node = Node.Builder(b =>
             b.Data.With(parameterV_Ed_)
                .With(parameterf_ck_)
                .With(parameterb_w_)
                .With(parameterd)
                .With(parameterA_sl_)
                .With(parameterN_Ed_)
                .With(parameterA_c_)
                .With(parameterk_1_)
                .With(parameterγ_c_)
             .Equation.WithParameter(parameterC_Rd_c_)
             .Equation.WithParameter(parameterk)
             .Equation.WithParameter(parameterρ_l_)
             .Equation.WithParameter(parameterf_cd_)
             .Equation.WithParameter(parameterσ_cp_)
             .Equation.WithParameter(parameterv_min_)
             .Equation.WithParameter(parameterV_Rd_c_)
             .Equation.WithParameter(parameterResistance)
               ).BuildInitialized();


            var calculatedNodes = node.CalculateNode();
            var calculatedParameters = calculatedNodes.CalculatedParameters;

            var calculatedParameterC_Rd_c_ = calculatedParameters.First(p => p.Name == "C_Rd,c_");
            var calculatedParameterk = calculatedParameters.First(p => p.Name == "k");
            var calculatedParameterρ_l_ = calculatedParameters.First(p => p.Name == "ρ_l_");
            var calculatedParameterf_cd_ = calculatedParameters.First(p => p.Name == "f_cd_");
            var calculatedParameterσ_cp_ = calculatedParameters.First(p => p.Name == "σ_cp_");
            var calculatedParameterv_min_ = calculatedParameters.First(p => p.Name == "v_min_");
            var calculatedParameterV_Rd_c_ = calculatedParameters.First(p => p.Name == "V_Rd,c_");
            var calculatedParameterResistance = calculatedParameters.First(p => p.Name == "Resistance");

            Assert.Multiple(() =>
            {
                Assert.That(new ValueUnit(0.12857142857142859d).AreEqual(calculatedParameterC_Rd_c_.Value, 0.000001));
                Assert.That(new ValueUnit(1.6586649219387843).AreEqual(calculatedParameterk.Value, 0.000001));
                Assert.That(new ValueUnit(0.0030639913232104123).AreEqual(calculatedParameterρ_l_.Value, 0.000001));
                Assert.That(new ValueUnit(21.42857142857143, new CustomUnit("MPa")).AreEqual(calculatedParameterf_cd_.Value, 0.000001));
                Assert.That(new ValueUnit(0.0006666666666666667, new CustomUnit("MPa")).AreEqual(calculatedParameterσ_cp_.Value, 0.000001));
                Assert.That(new ValueUnit(0.40951202774487683, new CustomUnit("MPa")).AreEqual(calculatedParameterv_min_.Value, 0.000001));
                Assert.That(new ValueUnit(49.43661944290878, new CustomUnit("kN")).AreEqual(calculatedParameterV_Rd_c_.Value, 0.000001));
                Assert.That(new ValueUnit(202.27920340605743).AreEqual(calculatedParameterResistance.Value, 0.000001));
            });
        }


        [Test]
        [TestCase(100, "kN", 30, "MPa", 240, "mm", 461, "mm", 339, "mm", 100, "N", 150000, "mm")]
        [TestCase(100000, "N", 30000, "kPa", 0.240, "m", 4.61, "dm", 3.39, "cm", 0.100, "kN", 0.150000, "m")]
        public void ShearResistanceWithoutShearReinforcementTest_NoValuesAtTheBegining_SetValueOnNodeLevel_Success(
            double V_Ed_Value, string V_Ed_Unit,
            double f_ck_Value, string f_ck_Unit,
            double b_w_Value, string b_w_Unit,
            double dValue, string dUnit,
            double A_sl_Value, string A_sl_Unit,
            double N_Ed_Value, string N_Ed_Unit,
            double A_c_Value, string A_c_Unit
            )
        {

            var parameterV_Ed_ = new InputParameter<ValueUnit>("V_Ed_");
            var parameterf_ck_ = new InputParameter<ValueUnit>("f_ck_");
            var parameterb_w_ = new InputParameter<ValueUnit>("b_w_");
            var parameterd = new InputParameter<ValueUnit>("d");
            var parameterA_sl_ = new InputParameter<ValueUnit>("A_sl_");
            var parameterN_Ed_ = new InputParameter<ValueUnit>("N_Ed_");
            var parameterA_c_ = new InputParameter<ValueUnit>("A_c_");
            var parameterk_1_ = new InputParameter<ValueUnit>("k_1_");
            var parameterγ_c_ = new InputParameter<ValueUnit>("γ_c_");

            var parameterC_Rd_c_ = new CalculationParameter("C_Rd,c_", "0.18/[γ_c_]");
            var parameterk = new CalculationParameter("k", "Min(1+Sqrt(Unit(200,'mm')/[d]),2)");
            var parameterρ_l_ = new CalculationParameter("ρ_l_", "Min(0.02,[A_sl_]/([b_w_]*[d]))");
            var parameterf_cd_ = new CalculationParameter("f_cd_", "[f_ck_]/[γ_c_]");
            var parameterσ_cp_ = new CalculationParameter("σ_cp_", "Min([N_Ed_]/[A_c_],0.2*[f_cd_])");
            var parameterv_min_ = new CalculationParameter("v_min_", "Unit(0.035,'MPa')*Pow([k],3/2)*Pow(([f_ck_]/Unit(1,'MPa')),1/2)");
            var parameterV_Rd_c_ = new CalculationParameter("V_Rd,c_", "Max(([v_min_]+[k_1_]*[σ_cp_])*[b_w_]*[d]," +
                "(Unit(1,'MPa')*[C_Rd,c_]*[k]*Pow(100*[ρ_l_]*[f_ck_]/Unit(1,'MPa'),1/3)+[k_1_]*[σ_cp_])*[b_w_]*[d])" 
                );
            var parameterResistance = new CalculationParameter("Resistance", "[V_Ed_]/[V_Rd,c_]*100");

            var node = Node.Builder(b =>
              b.Data.With(parameterV_Ed_)
                 .With(parameterf_ck_)
                 .With(parameterb_w_)
                 .With(parameterd)
                 .With(parameterA_sl_)
                 .With(parameterN_Ed_)
                 .With(parameterA_c_)
                 .With(parameterk_1_)
                 .With(parameterγ_c_)
              .Equation.WithParameter(parameterC_Rd_c_)
              .Equation.WithParameter(parameterk)
              .Equation.WithParameter(parameterρ_l_)
              .Equation.WithParameter(parameterf_cd_)
              .Equation.WithParameter(parameterσ_cp_)
              .Equation.WithParameter(parameterv_min_)
              .Equation.WithParameter(parameterV_Rd_c_)
              .Equation.WithParameter(parameterResistance)
               ).BuildInitialized();

            var V_Ed_ = new ValueUnit(V_Ed_Value, new CustomUnit(V_Ed_Unit, 1));
            var f_ck_ = new ValueUnit(f_ck_Value, new CustomUnit(f_ck_Unit, 1));
            var b_w_ = new ValueUnit(b_w_Value, new CustomUnit(b_w_Unit, 1));
            var d = new ValueUnit(dValue, new CustomUnit(dUnit, 1));
            var A_sl_ = new ValueUnit(A_sl_Value, new CustomUnit(A_sl_Unit, 2));
            var N_Ed_ = new ValueUnit(N_Ed_Value, new CustomUnit(N_Ed_Unit, 1));
            var A_c_ = new ValueUnit(A_c_Value, new CustomUnit(A_c_Unit, 2));
            var k_1_ = new ValueUnit(0.15);
            var γ_c_ = new ValueUnit(1.4);

            node.SetValue(parameterV_Ed_.Name, V_Ed_);
            node.SetValue(parameterf_ck_.Name, f_ck_);
            node.SetValue(parameterb_w_.Name, b_w_);
            node.SetValue(parameterd.Name, d);
            node.SetValue(parameterA_sl_.Name, A_sl_);
            node.SetValue(parameterN_Ed_.Name, N_Ed_);
            node.SetValue(parameterA_c_.Name, A_c_);
            node.SetValue(parameterk_1_.Name, k_1_);
            node.SetValue(parameterγ_c_.Name, γ_c_);

            var calculatedNodes = node.CalculateNode();
            var calculatedParameters = calculatedNodes.CalculatedParameters;

            var calculatedParameterC_Rd_c_ = calculatedParameters.First(p => p.Name == "C_Rd,c_");
            var calculatedParameterk = calculatedParameters.First(p => p.Name == "k");
            var calculatedParameterρ_l_ = calculatedParameters.First(p => p.Name == "ρ_l_");
            var calculatedParameterf_cd_ = calculatedParameters.First(p => p.Name == "f_cd_");
            var calculatedParameterσ_cp_ = calculatedParameters.First(p => p.Name == "σ_cp_");
            var calculatedParameterv_min_ = calculatedParameters.First(p => p.Name == "v_min_");
            var calculatedParameterV_Rd_c_ = calculatedParameters.First(p => p.Name == "V_Rd,c_");
            var calculatedParameterResistance = calculatedParameters.First(p => p.Name == "Resistance");

            Assert.Multiple(() =>
            {
                Assert.That(new ValueUnit(0.12857142857142859d).AreEqual(calculatedParameterC_Rd_c_.Value, 0.000001));
                Assert.That(new ValueUnit(1.6586649219387843).AreEqual(calculatedParameterk.Value, 0.000001));
                Assert.That(new ValueUnit(0.0030639913232104123).AreEqual(calculatedParameterρ_l_.Value, 0.000001));
                Assert.That(new ValueUnit(21.42857142857143, new CustomUnit("MPa")).AreEqual(calculatedParameterf_cd_.Value, 0.000001));
                Assert.That(new ValueUnit(0.0006666666666666667, new CustomUnit("MPa")).AreEqual(calculatedParameterσ_cp_.Value, 0.000001));
                Assert.That(new ValueUnit(0.40951202774487683, new CustomUnit("MPa")).AreEqual(calculatedParameterv_min_.Value, 0.000001));
                Assert.That(new ValueUnit(49.43661944290878, new CustomUnit("kN")).AreEqual(calculatedParameterV_Rd_c_.Value, 0.000001));
                Assert.That(new ValueUnit(202.27920340605743).AreEqual(calculatedParameterResistance.Value, 0.000001));
            });
        }


        [Test]
        [TestCase(100, "kN", 30, "MPa", 240, "mm", 461, "mm", 339, "mm", 100, "N", 150000, "mm")]
        [TestCase(100000, "N", 30000, "kPa", 0.240, "m", 4.61, "dm", 3.39, "cm", 0.100, "kN", 0.150000, "m")]
        public void ShearResistanceWithoutShearReinforcementTest_NoValuesAtTheBegining_SetValueToParameters_Success(
            double V_Ed_Value, string V_Ed_Unit,
            double f_ck_Value, string f_ck_Unit,
            double b_w_Value, string b_w_Unit,
            double dValue, string dUnit,
            double A_sl_Value, string A_sl_Unit,
            double N_Ed_Value, string N_Ed_Unit,
            double A_c_Value, string A_c_Unit
            )
        {

            var parameterV_Ed_ = new InputParameter<ValueUnit>("V_Ed_");
            var parameterf_ck_ = new InputParameter<ValueUnit>("f_ck_");
            var parameterb_w_ = new InputParameter<ValueUnit>("b_w_");
            var parameterd = new InputParameter<ValueUnit>("d");
            var parameterA_sl_ = new InputParameter<ValueUnit>("A_sl_");
            var parameterN_Ed_ = new InputParameter<ValueUnit>("N_Ed_");
            var parameterA_c_ = new InputParameter<ValueUnit>("A_c_");
            var parameterk_1_ = new InputParameter<ValueUnit>("k_1_");
            var parameterγ_c_ = new InputParameter<ValueUnit>("γ_c_");

            var parameterC_Rd_c_ = new CalculationParameter("C_Rd,c_", "0.18/[γ_c_]");
            var parameterk = new CalculationParameter("k", "Min(1+Sqrt(Unit(200,'mm')/[d]),2)");
            var parameterρ_l_ = new CalculationParameter("ρ_l_", "Min(0.02,[A_sl_]/([b_w_]*[d]))");
            var parameterf_cd_ = new CalculationParameter("f_cd_", "[f_ck_]/[γ_c_]");
            var parameterσ_cp_ = new CalculationParameter("σ_cp_", "Min([N_Ed_]/[A_c_],0.2*[f_cd_])");
            var parameterv_min_ = new CalculationParameter("v_min_", "Unit(0.035,'MPa')*Pow([k],3/2)*Pow(([f_ck_]/Unit(1,'MPa')),1/2)");
            var parameterV_Rd_c_ = new CalculationParameter("V_Rd,c_", "Max(([v_min_]+[k_1_]*[σ_cp_])*[b_w_]*[d]," +
                "(Unit(1,'MPa')*[C_Rd,c_]*[k]*Pow(100*[ρ_l_]*[f_ck_]/Unit(1,'MPa'),1/3)+[k_1_]*[σ_cp_])*[b_w_]*[d])");
            var parameterResistance = new CalculationParameter("Resistance", "[V_Ed_]/[V_Rd,c_]*100");

            var node = Node.Builder(b =>
              b.Data.With(parameterV_Ed_)
                 .With(parameterf_ck_)
                 .With(parameterb_w_)
                 .With(parameterd)
                 .With(parameterA_sl_)
                 .With(parameterN_Ed_)
                 .With(parameterA_c_)
                 .With(parameterk_1_)
                 .With(parameterγ_c_)
              .Equation.WithParameter(parameterC_Rd_c_)
              .Equation.WithParameter(parameterk)
              .Equation.WithParameter(parameterρ_l_)
              .Equation.WithParameter(parameterf_cd_)
              .Equation.WithParameter(parameterσ_cp_)
              .Equation.WithParameter(parameterv_min_)
              .Equation.WithParameter(parameterV_Rd_c_)
              .Equation.WithParameter(parameterResistance)
               ).BuildInitialized();

            var V_Ed_ = new ValueUnit(V_Ed_Value, new CustomUnit(V_Ed_Unit, 1));
            var f_ck_ = new ValueUnit(f_ck_Value, new CustomUnit(f_ck_Unit, 1));
            var b_w_ = new ValueUnit(b_w_Value, new CustomUnit(b_w_Unit, 1));
            var d = new ValueUnit(dValue, new CustomUnit(dUnit, 1));
            var A_sl_ = new ValueUnit(A_sl_Value, new CustomUnit(A_sl_Unit, 2));
            var N_Ed_ = new ValueUnit(N_Ed_Value, new CustomUnit(N_Ed_Unit, 1));
            var A_c_ = new ValueUnit(A_c_Value, new CustomUnit(A_c_Unit, 2));
            var k_1_ = new ValueUnit(0.15);
            var γ_c_ = new ValueUnit(1.4);

            parameterV_Ed_.SetValue(V_Ed_);
            parameterf_ck_.SetValue(f_ck_);
            parameterb_w_.SetValue( b_w_);
            parameterd.SetValue(d);
            parameterA_sl_.SetValue(A_sl_);
            parameterN_Ed_.SetValue( N_Ed_);
            parameterA_c_.SetValue(A_c_);
            parameterk_1_.SetValue(k_1_);
            parameterγ_c_.SetValue(γ_c_);

            var calculatedNodes = node.CalculateNode();
            var calculatedParameters = calculatedNodes.CalculatedParameters;

            var calculatedParameterC_Rd_c_ = calculatedParameters.First(p => p.Name == "C_Rd,c_");
            var calculatedParameterk = calculatedParameters.First(p => p.Name == "k");
            var calculatedParameterρ_l_ = calculatedParameters.First(p => p.Name == "ρ_l_");
            var calculatedParameterf_cd_ = calculatedParameters.First(p => p.Name == "f_cd_");
            var calculatedParameterσ_cp_ = calculatedParameters.First(p => p.Name == "σ_cp_");
            var calculatedParameterv_min_ = calculatedParameters.First(p => p.Name == "v_min_");
            var calculatedParameterV_Rd_c_ = calculatedParameters.First(p => p.Name == "V_Rd,c_");
            var calculatedParameterResistance = calculatedParameters.First(p => p.Name == "Resistance");

            Assert.Multiple(() =>
            {
                Assert.That(new ValueUnit(0.12857142857142859d).AreEqual(calculatedParameterC_Rd_c_.Value, 0.000001));
                Assert.That(new ValueUnit(1.6586649219387843).AreEqual(calculatedParameterk.Value, 0.000001));
                Assert.That(new ValueUnit(0.0030639913232104123).AreEqual(calculatedParameterρ_l_.Value, 0.000001));
                Assert.That(new ValueUnit(21.42857142857143, new CustomUnit("MPa")).AreEqual(calculatedParameterf_cd_.Value, 0.000001));
                Assert.That(new ValueUnit(0.0006666666666666667, new CustomUnit("MPa")).AreEqual(calculatedParameterσ_cp_.Value, 0.000001));
                Assert.That(new ValueUnit(0.40951202774487683, new CustomUnit("MPa")).AreEqual(calculatedParameterv_min_.Value, 0.000001));
                Assert.That(new ValueUnit(49.43661944290878, new CustomUnit("kN")).AreEqual(calculatedParameterV_Rd_c_.Value, 0.000001));
                Assert.That(new ValueUnit(202.27920340605743).AreEqual(calculatedParameterResistance.Value, 0.000001));
            });
        }



        [Test]
        [TestCase(100, "kN", 30, "MPa", 240, "mm", 461, "mm", 339, "mm", 100, "N", 150000, "mm")]
        [TestCase(100000, "N", 30000, "kPa", 0.240, "m", 4.61, "dm", 3.39, "cm", 0.100, "kN", 0.150000, "m")]
        public void ShearResistanceWithoutShearReinforcementTest_NoValuesAtTheBegining_SetValueToParameters_NoLambda_Success(
            double V_Ed_Value, string V_Ed_Unit,
            double f_ck_Value, string f_ck_Unit,
            double b_w_Value, string b_w_Unit,
            double dValue, string dUnit,
            double A_sl_Value, string A_sl_Unit,
            double N_Ed_Value, string N_Ed_Unit,
            double A_c_Value, string A_c_Unit
            )
        {

            var parameterV_Ed_ = new InputParameter<ValueUnit>("V_Ed_");
            var parameterf_ck_ = new InputParameter<ValueUnit>("f_ck_");
            var parameterb_w_ = new InputParameter<ValueUnit>("b_w_");
            var parameterd = new InputParameter<ValueUnit>("d");
            var parameterA_sl_ = new InputParameter<ValueUnit>("A_sl_");
            var parameterN_Ed_ = new InputParameter<ValueUnit>("N_Ed_");
            var parameterA_c_ = new InputParameter<ValueUnit>("A_c_");
            var parameterk_1_ = new InputParameter<ValueUnit>("k_1_");
            var parameterγ_c_ = new InputParameter<ValueUnit>("γ_c_");

            var parameterC_Rd_c_ = new CalculationParameter("C_Rd,c_", "0.18/[γ_c_]");
            var parameterk = new CalculationParameter("k", "Min(1+Sqrt(Unit(200,'mm')/[d]),2)");
            var parameterρ_l_ = new CalculationParameter("ρ_l_", "Min(0.02,[A_sl_]/([b_w_]*[d]))");
            var parameterf_cd_ = new CalculationParameter("f_cd_", "[f_ck_]/[γ_c_]");
            var parameterσ_cp_ = new CalculationParameter("σ_cp_", "Min([N_Ed_]/[A_c_],0.2*[f_cd_])");
            var parameterv_min_ = new CalculationParameter("v_min_", "Unit(0.035,'MPa')*Pow([k],3/2)*Pow(([f_ck_]/Unit(1,'MPa')),1/2)");
            var parameterV_Rd_c_ = new CalculationParameter("V_Rd,c_", "Max(([v_min_]+[k_1_]*[σ_cp_])*[b_w_]*[d]," +
                "(Unit(1,'MPa')*[C_Rd,c_]*[k]*Pow(100*[ρ_l_]*[f_ck_]/Unit(1,'MPa'),1/3)+[k_1_]*[σ_cp_])*[b_w_]*[d])");
            var parameterResistance = new CalculationParameter("Resistance", "[V_Ed_]/[V_Rd,c_]*100");

            var node = Node.Builder(b =>
              b.Data.With(parameterV_Ed_)
                 .With(parameterf_ck_)
                 .With(parameterb_w_)
                 .With(parameterd)
                 .With(parameterA_sl_)
                 .With(parameterN_Ed_)
                 .With(parameterA_c_)
                 .With(parameterk_1_)
                 .With(parameterγ_c_)
              .Equation.WithParameter(parameterC_Rd_c_)
              .Equation.WithParameter(parameterk)
              .Equation.WithParameter(parameterρ_l_)
              .Equation.WithParameter(parameterf_cd_)
              .Equation.WithParameter(parameterσ_cp_)
              .Equation.WithParameter(parameterv_min_)
              .Equation.WithParameter(parameterV_Rd_c_)
              .Equation.WithParameter(parameterResistance)
               ).BuildInitialized();

            var V_Ed_ = new ValueUnit(V_Ed_Value, new CustomUnit(V_Ed_Unit, 1));
            var f_ck_ = new ValueUnit(f_ck_Value, new CustomUnit(f_ck_Unit, 1));
            var b_w_ = new ValueUnit(b_w_Value, new CustomUnit(b_w_Unit, 1));
            var d = new ValueUnit(dValue, new CustomUnit(dUnit, 1));
            var A_sl_ = new ValueUnit(A_sl_Value, new CustomUnit(A_sl_Unit, 2));
            var N_Ed_ = new ValueUnit(N_Ed_Value, new CustomUnit(N_Ed_Unit, 1));
            var A_c_ = new ValueUnit(A_c_Value, new CustomUnit(A_c_Unit, 2));
            var k_1_ = new ValueUnit(0.15);
            var γ_c_ = new ValueUnit(1.4);

            parameterV_Ed_.SetValue(V_Ed_);
            parameterf_ck_.SetValue(f_ck_);
            parameterb_w_.SetValue(b_w_);
            parameterd.SetValue(d);
            parameterA_sl_.SetValue(A_sl_);
            parameterN_Ed_.SetValue(N_Ed_);
            parameterA_c_.SetValue(A_c_);
            parameterk_1_.SetValue(k_1_);
            parameterγ_c_.SetValue(γ_c_);

            var calculatedNodes = node.CalculateNodeNoLambda();
            var calculatedParameters = calculatedNodes.CalculatedParameters;

            var calculatedParameterC_Rd_c_ = calculatedParameters.First(p => p.Name == "C_Rd,c_");
            var calculatedParameterk = calculatedParameters.First(p => p.Name == "k");
            var calculatedParameterρ_l_ = calculatedParameters.First(p => p.Name == "ρ_l_");
            var calculatedParameterf_cd_ = calculatedParameters.First(p => p.Name == "f_cd_");
            var calculatedParameterσ_cp_ = calculatedParameters.First(p => p.Name == "σ_cp_");
            var calculatedParameterv_min_ = calculatedParameters.First(p => p.Name == "v_min_");
            var calculatedParameterV_Rd_c_ = calculatedParameters.First(p => p.Name == "V_Rd,c_");
            var calculatedParameterResistance = calculatedParameters.First(p => p.Name == "Resistance");

            Assert.Multiple(() =>
            {
                Assert.That(new ValueUnit(0.12857142857142859d).AreEqual(calculatedParameterC_Rd_c_.Value, 0.000001));
                Assert.That(new ValueUnit(1.6586649219387843).AreEqual(calculatedParameterk.Value, 0.000001));
                Assert.That(new ValueUnit(0.0030639913232104123).AreEqual(calculatedParameterρ_l_.Value, 0.000001));
                Assert.That(new ValueUnit(21.42857142857143, new CustomUnit("MPa")).AreEqual(calculatedParameterf_cd_.Value, 0.000001));
                Assert.That(new ValueUnit(0.0006666666666666667, new CustomUnit("MPa")).AreEqual(calculatedParameterσ_cp_.Value, 0.000001));
                Assert.That(new ValueUnit(0.40951202774487683, new CustomUnit("MPa")).AreEqual(calculatedParameterv_min_.Value, 0.000001));
                Assert.That(new ValueUnit(49.43661944290878, new CustomUnit("kN")).AreEqual(calculatedParameterV_Rd_c_.Value, 0.000001));
                Assert.That(new ValueUnit(202.27920340605743).AreEqual(calculatedParameterResistance.Value, 0.000001));
            });
        }

        [Test]
        public void SteelBending()
        {
            var parameterf_y_ = new InputParameter<ValueUnit>("f_y_");
            var parameterh_e_ = new InputParameter<ValueUnit>("h_e_");
            var parametert_f_e_ = new InputParameter<ValueUnit>("t_f,e_");
            var parameterr_e_ = new InputParameter<ValueUnit>("r_e_");
            var parametert_w_e_ = new InputParameter<ValueUnit>("t_w,e_");
            var parameterb_e_ = new InputParameter<ValueUnit>("b_e_");
            var parameterγ_M0_ = new InputParameter<ValueUnit>("γ_M0_");
            var parameterA_e_ = new InputParameter<ValueUnit>("A_e_");
            var parameterd_c_ = new InputParameter<ValueUnit>("d_c_");
            var parameterx_c_ = new InputParameter<ValueUnit>("x_c_");
            var parameterM_y_Ed_ = new InputParameter<ValueUnit>("M_y,Ed_");

            var parameterε = new CalculationParameter("ε", "Sqrt(Unit(235,'MPa')/[f_y_])");
            var parameterct_w_ = new CalculationParameter("c/t_w_", "([h_e_]-2*[t_f,e_]-2*[r_e_])/[t_w,e_]");
            var parameterct_f_= new CalculationParameter("c/t_f_", "([b_e_]-[t_w,e_]-2*[r_e_])/(2*[t_f,e_])");
            var parameterM_pl_Rd_ = new CalculationParameter("M_pl,Rd_", "[f_y_]/[γ_M0_]*[A_e_]*([d_c_]-1/2*[x_c_])");
            var parameterResistance = new CalculationParameter("Resistance", "[M_y,Ed_]/[M_pl,Rd_]");

            var node = Node.Builder(b =>
              b.Data.With(parameterf_y_)
                 .With(parameterh_e_)
                 .With(parametert_f_e_)
                 .With(parameterr_e_)
                 .With(parametert_w_e_)
                 .With(parameterb_e_)
                 .With(parameterγ_M0_)
                 .With(parameterA_e_)
                 .With(parameterd_c_)
                 .With(parameterx_c_)
                 .With(parameterM_y_Ed_)
              .Equation.WithParameter(parameterε)
              .Equation.WithParameter(parameterct_w_)
              .Equation.WithParameter(parameterct_f_)
              .Equation.WithParameter(parameterM_pl_Rd_)
              .Equation.WithParameter(parameterResistance)
               ).BuildInitialized();


            parameterf_y_.SetValue(new ValueUnit(355, new CustomUnit("MPa", 1)));
            parameterh_e_.SetValue(new ValueUnit(270, new CustomUnit("mm")));
            parametert_f_e_.SetValue(new ValueUnit(10.2, new CustomUnit("mm")));
            parameterr_e_.SetValue(new ValueUnit(15, new CustomUnit("mm")));
            parametert_w_e_.SetValue(new ValueUnit(6.6, new CustomUnit("mm")));
            parameterb_e_.SetValue(new ValueUnit(135, new CustomUnit("mm")));
            parameterγ_M0_.SetValue(new ValueUnit(1));
            parameterA_e_.SetValue(new ValueUnit(45.9, new CustomUnit("cm",2)));
            parameterd_c_.SetValue(new ValueUnit(217, new CustomUnit("mm")));
            parameterx_c_.SetValue(new ValueUnit(44.73, new CustomUnit("mm")));
            parameterM_y_Ed_.SetValue(new ValueUnit(304.04, new CustomUnit("kN"), new CustomUnit("m")));

            var calculatedNodes = node.CalculateNode();
            var calculatedParameters = calculatedNodes.CalculatedParameters;

            var calculatedParameterε = calculatedParameters.First(p => p.Name == "ε");
            var calculatedParameterct_w_ = calculatedParameters.First(p => p.Name == "c/t_w_");
            var calculatedParameterct_f_ = calculatedParameters.First(p => p.Name == "c/t_f_");
            var calculatedParameterM_pl_Rd_ = calculatedParameters.First(p => p.Name == "M_pl,Rd_");
            var calculatedParameterResistance = calculatedParameters.First(p => p.Name == "Resistance");

            Assert.Multiple(() =>
            {
                Assert.AreEqual(new ValueUnit(0.8136165134668271), calculatedParameterε.Value);
                Assert.AreEqual(new ValueUnit(33.27272727272727), calculatedParameterct_w_.Value);
                Assert.AreEqual(new ValueUnit(4.8235294117647065), calculatedParameterct_f_.Value);
                Assert.AreEqual(new ValueUnit(317.14800075, "kN", "m"), calculatedParameterM_pl_Rd_.Value);
                Assert.AreEqual(new ValueUnit(0.9586691364315657), calculatedParameterResistance.Value);
            });
        }


        [Test]
        public void SteelBending_Simplified()
        {
            var node = Node.Builder(b =>
              b.Data.With("f_y_")
                 .With("h_e_")
                 .With("t_f,e_")
                 .With("r_e_")
                 .With("t_w,e_")
                 .With("b_e_")
                 .With("γ_M0_")
                 .With("A_e_")
                 .With("d_c_")
                 .With("x_c_")
                 .With("M_y,Ed_")
              .Equation.WithParameter("ε", "Sqrt(Unit(235,'MPa')/[f_y_])")
              .Equation.WithParameter("c/t_w_", "([h_e_]-2*[t_f,e_]-2*[r_e_])/[t_w,e_]")
              .Equation.WithParameter("c/t_f_", "([b_e_]-[t_w,e_]-2*[r_e_])/(2*[t_f,e_])")
              .Equation.WithParameter("M_pl,Rd_", "[f_y_]/[γ_M0_]*[A_e_]*([d_c_]-1/2*[x_c_])")
              .Equation.WithParameter("Resistance", "[M_y,Ed_]/[M_pl,Rd_]")
               ).BuildInitialized();

            node.SetValue("f_y_", 355, "MPa");
            node.SetValue("h_e_", 270, "mm");
            node.SetValue("t_f,e_", 10.2, "mm");
            node.SetValue("r_e_", 15, "mm");
            node.SetValue("t_w,e_", 6.6, "mm");
            node.SetValue("b_e_", 135, "mm");
            node.SetValue("γ_M0_", 1);
            node.SetValue("A_e_", 45.9, "cm^2");
            node.SetValue("d_c_", 217, "mm");
            node.SetValue("x_c_", 44.73, "mm");
            node.SetValue("M_y,Ed_", 304.04, "kN", "m");

            var calculatedNodes = node.CalculateNode();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(new ValueUnit(0.8136165134668271), calculatedNodes["ε"].Value);
                Assert.AreEqual(new ValueUnit(33.27272727272727), calculatedNodes["c/t_w_"].Value);
                Assert.AreEqual(new ValueUnit(4.8235294117647065), calculatedNodes["c/t_f_"].Value);
                Assert.AreEqual(new ValueUnit(317.14800075, "kN", "m"), calculatedNodes["M_pl,Rd_"].Value);
                Assert.AreEqual(new ValueUnit(0.9586691364315657), calculatedNodes["Resistance"].Value);
            });
        }
    }
}
