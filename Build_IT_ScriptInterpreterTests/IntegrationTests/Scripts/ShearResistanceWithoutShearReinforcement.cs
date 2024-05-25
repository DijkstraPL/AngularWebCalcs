using Build_IT_NCalc.Units;
using Build_IT_ScriptInterpreter.Parameters;
using Build_IT_ScriptInterpreter.Scripts;
using NUnit.Framework;
using System.Linq;

namespace Build_IT_ScriptInterpreterTests.IntegrationTests.Scripts
{
    [TestFixture]
    public class ShearResistanceWithoutShearReinforcement
    {
        //[Test]
        //[TestCase(100, "kN", 30, "MPa", 240, "mm", 461, "mm", 339, "mm", 100, "N", 150000, "mm")]
        //[TestCase(100000, "N", 30000, "kPa", 0.240, "m", 4.61, "dm", 3.39, "cm", 0.100, "kN", 0.150000, "m")]
        //public void ShearResistanceWithoutShearReinforcementTest_Success(
        //    double V_Ed_Value, string V_Ed_Unit,
        //    double f_ck_Value, string f_ck_Unit,
        //    double b_w_Value, string b_w_Unit,
        //    double dValue, string dUnit,
        //    double A_sl_Value, string A_sl_Unit,
        //    double N_Ed_Value, string N_Ed_Unit,
        //    double A_c_Value, string A_c_Unit
        //    )
        //{
        //        var V_Ed_ = new ValueUnit(V_Ed_Value,  new CustomUnit(V_Ed_Unit, 1));
        //        var f_ck_ = new ValueUnit(f_ck_Value,  new CustomUnit(f_ck_Unit, 1));
        //        var b_w_ = new ValueUnit(b_w_Value,  new CustomUnit(b_w_Unit, 1));
        //        var d = new ValueUnit(dValue,  new CustomUnit(dUnit, 1));
        //        var A_sl_ = new ValueUnit(A_sl_Value,  new CustomUnit(A_sl_Unit, 2));
        //        var N_Ed_ = new ValueUnit(N_Ed_Value,  new CustomUnit(N_Ed_Unit, 1));
        //        var A_c_ = new ValueUnit(A_c_Value,  new CustomUnit(A_c_Unit, 2));
        //        var k_1_ = 0.15;
        //        var γ_c_ = 1.4;

        //        var parameterV_Ed_ = new InputParameter<ValueUnit>(1, "V_Ed_", V_Ed_);
        //        var parameterf_ck_ = new InputParameter<ValueUnit>(2, "f_ck_", f_ck_);
        //        var parameterb_w_ = new InputParameter<ValueUnit>(3, "b_w_", b_w_);
        //        var parameterd = new InputParameter<ValueUnit>(4, "d", d);
        //        var parameterA_sl_ = new InputParameter<ValueUnit>(5, "A_sl_", A_sl_);
        //        var parameterN_Ed_ = new InputParameter<ValueUnit>(6, "N_Ed_", N_Ed_);
        //        var parameterA_c_ = new InputParameter<ValueUnit>(7, "A_c_", A_c_);
        //        var parameterk_1_ = new InputParameter<double>(8, "k_1_", k_1_);
        //        var parameterγ_c_ = new InputParameter<double>(9, "γ_c_", γ_c_);

        //        var parameterC_Rd_c_ = new Parameter<string>(10, "C_Rd,c_", "0.18/[γ_c_]");
        //        var parameterk = new Parameter<string>(11, "k", "Min(1+Sqrt(Unit(200,'mm')/[d]),2)");
        //        var parameterρ_l_ = new Parameter<string>(12, "ρ_l_", "Min(0.02,[A_sl_]/([b_w_]*[d]))");
        //        var parameterf_cd_ = new Parameter<string>(13, "f_cd_", "[f_ck_]/[γ_c_]");
        //        var parameterσ_cp_ = new Parameter<string>(14, "σ_cp_", "Min([N_Ed_]/[A_c_],0.2*[f_cd_])");
        //        var parameterv_min_ = new Parameter<string>(15, "v_min_", "Unit(0.035,'MPa')*Pow([k],3/2)*Pow(([f_ck_]/Unit(1,'MPa')),1/2)");
        //        var parameterV_Rd_c_ = new Parameter<string>(16, "V_Rd,c_", "Max(([v_min_]+[k_1_]*[σ_cp_])*[b_w_]*[d]," +
        //            "(Unit(1,'MPa')*[C_Rd,c_]*[k]*Pow(100*[ρ_l_]*[f_ck_]/Unit(1,'MPa'),1/3)+[k_1_]*[σ_cp_])*[b_w_]*[d])" 
        //            );
        //        var parameterResistance = new Parameter<string>(17, "Resistance", "[V_Ed_]/[V_Rd,c_]*100");

        //        var script = new Script("ShearResistanceWithoutShearReinforcement",
        //            parameterV_Ed_, parameterf_ck_, parameterb_w_, parameterd, parameterA_sl_, parameterN_Ed_,
        //            parameterA_c_, parameterk_1_, parameterγ_c_, parameterC_Rd_c_, parameterk, parameterρ_l_,
        //            parameterf_cd_, parameterσ_cp_, parameterv_min_, parameterV_Rd_c_, parameterResistance);

        //        var calculatedParameters = script.CalculateScript();

        //        var calculatedParameterC_Rd_c_ = calculatedParameters.First(p => p.Name == "C_Rd,c_");
        //        var calculatedParameterk = calculatedParameters.First(p => p.Name == "k");
        //        var calculatedParameterρ_l_ = calculatedParameters.First(p => p.Name == "ρ_l_");
        //        var calculatedParameterf_cd_ = calculatedParameters.First(p => p.Name == "f_cd_");
        //        var calculatedParameterσ_cp_ = calculatedParameters.First(p => p.Name == "σ_cp_");
        //        var calculatedParameterv_min_ = calculatedParameters.First(p => p.Name == "v_min_");
        //        var calculatedParameterV_Rd_c_ = calculatedParameters.First(p => p.Name == "V_Rd,c_");
        //        var calculatedParameterResistance = calculatedParameters.First(p => p.Name == "Resistance");

        //        Assert.Multiple(() =>
        //        {
        //            Assert.That(new ValueUnit(0.12857142857142859d).AreEqual(calculatedParameterC_Rd_c_.Value, 0.000001));
        //            Assert.That(new ValueUnit(1.6586649219387843).AreEqual(calculatedParameterk.Value, 0.000001));
        //            Assert.That(new ValueUnit(0.0030639913232104123).AreEqual(calculatedParameterρ_l_.Value, 0.000001));
        //            Assert.That(new ValueUnit(21.42857142857143, new CustomUnit("MPa")).AreEqual(calculatedParameterf_cd_.Value, 0.000001));
        //            Assert.That(new ValueUnit(0.0006666666666666667, new CustomUnit("MPa")).AreEqual(calculatedParameterσ_cp_.Value, 0.000001));
        //            Assert.That(new ValueUnit(0.40951202774487683, new CustomUnit("MPa")).AreEqual(calculatedParameterv_min_.Value, 0.000001));
        //            Assert.That(new ValueUnit(49.43661944290878, new CustomUnit("kN")).AreEqual(calculatedParameterV_Rd_c_.Value, 0.000001));
        //            Assert.That(new ValueUnit(202.27920340605743).AreEqual(calculatedParameterResistance.Value, 0.000001));
        //        });
        //}

        //[Test]
        //public void NewlyCreatedScriptTest_Success()
        //{
        //    var scriptBuilder = ScriptBuilder.Create(name: "Shear resistance without shear reinforcement",
        //        description: "Calculate shear resistance without shear reinforcement. Base on [PN-EN-1992-1-1:2002 6.2.2].",
        //        "Eurocode 1992", "Concrete", "Shear", "Resistance");

        //    scriptBuilder
        //        .AppendParameter(new Parameter()
        //        {
        //            Number = 1,
        //            Name = "V_Ed_",
        //            ValueType = ValueTypes.Number,
        //            ParameterOptions = ParameterOptions.Editable | ParameterOptions.Visible,
        //            Unit = "kN"
        //        })
        //        .AppendParameter(new Parameter()
        //        {
        //            Number = 2,
        //            Name = "f_ck_",
        //            ValueType = ValueTypes.Number,
        //            ParameterOptions = ParameterOptions.Editable | ParameterOptions.Visible,
        //            Unit = "MPa"
        //        })
        //        .AppendParameter(new Parameter()
        //        {
        //            Number = 4,
        //            Name = "b_w_",
        //            ValueType = ValueTypes.Number,
        //            ParameterOptions = ParameterOptions.Editable | ParameterOptions.Visible,
        //            Unit = "mm"
        //        })
        //        .AppendParameter(new Parameter()
        //        {
        //            Number = 5,
        //            Name = "d",
        //            ValueType = ValueTypes.Number,
        //            ParameterOptions = ParameterOptions.Editable | ParameterOptions.Visible,
        //            Unit = "mm"
        //        })
        //        .AppendParameter(new Parameter()
        //        {
        //            Number = 6,
        //            Name = "A_sl_",
        //            ValueType = ValueTypes.Number,
        //            ParameterOptions = ParameterOptions.Editable | ParameterOptions.Visible,
        //            Unit = "cm^2^"
        //        })
        //        .AppendParameter(new Parameter()
        //        {
        //            Number = 7,
        //            Name = "N_Ed_",
        //            Value = 0,
        //            ValueType = ValueTypes.Number,
        //            ParameterOptions = ParameterOptions.Editable | ParameterOptions.Visible,
        //            Unit = "N"
        //        })
        //        .AppendParameter(new Parameter()
        //        {
        //            Number = 8,
        //            Name = "A_c_",
        //            ValueType = ValueTypes.Number,
        //            ParameterOptions = ParameterOptions.Editable | ParameterOptions.Visible,
        //            Unit = "mm^2^"
        //        })
        //        .AppendParameter(new Parameter()
        //        {
        //            Number = 10,
        //            Name = "k_1_",
        //            Value = 0.15,
        //            ValueType = ValueTypes.Number,
        //            ParameterOptions = ParameterOptions.StaticData,
        //            Unit = "-"
        //        })
        //        .AppendParameter(new Parameter()
        //        {
        //            Number = 11,
        //            Name = "γ_c_",
        //            Value = 1.4,
        //            ValueType = ValueTypes.Number,
        //            ParameterOptions = ParameterOptions.StaticData,
        //            Unit = "-"
        //        }); 

        //    scriptBuilder.AppendParameter(new Parameter()
        //    {
        //        Number = 20,
        //        Name = "C_Rd,c_",
        //        Value = "0.18/[γ_c_]",
        //        ValueType = ValueTypes.Number,
        //        ParameterOptions = ParameterOptions.Calculation | ParameterOptions.Visible,
        //        Unit = "-"
        //    });

        //    scriptBuilder.AppendParameter(new Parameter()
        //    {
        //        Number = 21,
        //        Name = "k",
        //        Value = "Min(1+Sqrt(200/[d]),2)",
        //        ValueType = ValueTypes.Number,
        //        ParameterOptions = ParameterOptions.Calculation | ParameterOptions.Visible,
        //        Unit = "-"
        //    });

        //    scriptBuilder.AppendParameter(new Parameter()
        //    {
        //        Number = 22,
        //        Name = "ρ_l_",
        //        Value = "Min(0.02,[A_sl_]/([b_w_]*[d]))",
        //        ValueType = ValueTypes.Number,
        //        ParameterOptions = ParameterOptions.Calculation | ParameterOptions.Visible,
        //        Unit = "-"
        //    });

        //    scriptBuilder.AppendParameter(new Parameter()
        //    {
        //        Number = 23,
        //        Name = "f_cd_",
        //        Value = "[f_ck_]/[γ_c_]",
        //        ValueType = ValueTypes.Number,
        //        ParameterOptions = ParameterOptions.Calculation | ParameterOptions.Visible,
        //        Unit = "MPa"
        //    });

        //    scriptBuilder.AppendParameter(new Parameter()
        //    {
        //        Number = 24,
        //        Name = "σ_cp_",
        //        Value = "Min([N_Ed_]/[A_c_],0.2*[f_cd_])",
        //        ValueType = ValueTypes.Number,
        //        ParameterOptions = ParameterOptions.Calculation | ParameterOptions.Visible,
        //        Unit = "MPa"
        //    });

        //    scriptBuilder.AppendParameter(new Parameter()
        //    {
        //        Number = 25,
        //        Name = "v_min_",
        //        Value = "0.035*Pow([k],3/2)*Pow([f_ck_],1/2)",
        //        ValueType = ValueTypes.Number,
        //        ParameterOptions = ParameterOptions.Calculation | ParameterOptions.Visible,
        //        Unit = "MPa"
        //    });

        //    scriptBuilder.AppendParameter(new Parameter()
        //    {
        //        Number = 26,
        //        Name = "V_Rd,c_",
        //        Value = "Max(([v_min_]+[k_1_]*[σ _cp_])*[b_w_]*[d]," +
        //        "([C_Rd,c_]*[k]*Pow(100*[ρ_l_]*[f_ck_],1/3)+[k_1_]*[σ _cp_])*[b_w_]*[d])" +
        //        "/1000",
        //        ValueType = ValueTypes.Number,
        //        ParameterOptions = ParameterOptions.Calculation | ParameterOptions.Visible,
        //        Unit = "kN"
        //    });

        //    scriptBuilder.AppendParameter(new Parameter()
        //    {
        //        Number = 27,
        //        Name = "Resistance",
        //        Value = "[V_Ed_]/[V_Rd,c_]*100",
        //        ValueType = ValueTypes.Number,
        //        ParameterOptions = ParameterOptions.Calculation | ParameterOptions.Visible,
        //        Unit = "%"
        //    });

        //    var script = scriptBuilder.Build();

        //    var calculationEngine = new CalculationEngine(script.Parameters);
        //    calculationEngine.CalculateFromText("[V_Ed_]=100|[f_ck_]=30|[b_w_]=240|[d]=461|[A_sl_]=339|[N_Ed_]=100|[A_c_]=150000");

        //    Assert.That(0.128571, Is.EqualTo(script.GetParameterByName("C_Rd,c_").Value).Within(0.000001));
        //    Assert.That(1.658664, Is.EqualTo(script.GetParameterByName("k").Value).Within(0.000001));
        //    Assert.That(0.003063, Is.EqualTo(script.GetParameterByName("ρ_l_").Value).Within(0.000001));
        //    Assert.That(21.428571, Is.EqualTo(script.GetParameterByName("f_cd_").Value).Within(0.000001));
        //    Assert.That(0.000666, Is.EqualTo(script.GetParameterByName("σ_cp_").Value).Within(0.000001));
        //    Assert.That(0.409512, Is.EqualTo(script.GetParameterByName("v_min_").Value).Within(0.000001));
        //    Assert.That(49.436619, Is.EqualTo(script.GetParameterByName("V_Rd,c_").Value).Within(0.000001));
        //    Assert.That(202.279, Is.EqualTo(script.GetParameterByName("Resistance").Value).Within(0.001));
        //}
    }
}
