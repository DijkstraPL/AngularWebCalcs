using BenchmarkDotNet.Attributes;
using Build_IT_NCalc.Units;
using Build_IT_ScriptInterpreter.Parameters;
using Build_IT_ScriptInterpreter.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Build_IT_ScriptBenchmark.Scripts
{
    public class SteelTensionBenchmark
    {
        private readonly Script _script;

        public SteelTensionBenchmark()
        {

            double gamma_M0_ = 1.0;
            var parameterA = new InputParameter<ValueUnit>(1, "A", null);
            var parameterf_y_ = new InputParameter<ValueUnit>(2, "f_y_", null);
            var parameterN_Ed_ = new InputParameter<ValueUnit>(3, "N_Ed_", null);
            var parametergamma_M0_ = new InputParameter<double>(4, "γ_M0_", gamma_M0_);
            var parameterNplRd = new Parameter<string>(5, "N_pl,Rd_", "[A]*[f_y_]/[γ_M0_]");
            var parameterResistance = new Parameter<string>(6, "Resistance", "[N_Ed_]/[N_pl,Rd_]");

            _script = new Script("SteelTension",
                parameterA, parameterf_y_, parameterN_Ed_, parametergamma_M0_,
                parameterNplRd, parameterResistance);
        }


        [Benchmark]
        public ValueUnit CalculateWithNewObject()
        {
            var random = new Random();

                var A = new ValueUnit(random.Next(1, 120), new CustomUnit("cm", 2));
                var f_y_ = new ValueUnit(random.Next(100, 500),  new CustomUnit("MPa", 1));
                var N_Ed_ = new ValueUnit(random.Next(700, 7000),  new CustomUnit("kN", 1));
                double gamma_M0_ = 1.0;

                var parameterA = new InputParameter<ValueUnit>(1, "A", A);
                var parameterf_y_ = new InputParameter<ValueUnit>(2, "f_y_", f_y_);
                var parameterN_Ed_ = new InputParameter<ValueUnit>(3, "N_Ed_", N_Ed_);
                var parametergamma_M0_ = new InputParameter<double>(4, "γ_M0_", gamma_M0_);
                var parameterNplRd = new Parameter<string>(5, "N_pl,Rd_", "[A]*[f_y_]/[γ_M0_]");
                var parameterResistance = new Parameter<string>(6, "Resistance", "[N_Ed_]/[N_pl,Rd_]");

                var script = new Script("SteelTension",
                    parameterA, parameterf_y_, parameterN_Ed_, parametergamma_M0_,
                    parameterNplRd, parameterResistance);

                var calculatedParameters = script.CalculateScript();

                var calculatedParameterNplRd = calculatedParameters.First(p => p.Name == "N_pl,Rd_");
                var calculatedParameterResistance = calculatedParameters.First(p => p.Name == "Resistance");
                var resistanceResult = (ValueUnit)calculatedParameterResistance.CalculatedValue;
                resistanceResult.OrganizeUnits();

                return resistanceResult;
        }

        [Benchmark]
        public ValueUnit CalculateWithSameObject()
        {
            var random = new Random();
                var A = new ValueUnit(random.Next(1, 120), new CustomUnit("cm", 2));
                var f_y_ = new ValueUnit(random.Next(100, 500), new CustomUnit("MPa", 1));
                var N_Ed_ = new ValueUnit(random.Next(700, 7000),  new CustomUnit("kN", 1));

                var calculatedParameters = _script.CalculateScript(("A", A), ("f_y_", f_y_), ("N_Ed_", N_Ed_));

                var calculatedParameterNplRd = calculatedParameters.First(p => p.Name == "N_pl,Rd_");
                var calculatedParameterResistance = calculatedParameters.First(p => p.Name == "Resistance");
                var resistanceResult = (ValueUnit)calculatedParameterResistance.CalculatedValue;
                resistanceResult.OrganizeUnits();

                return resistanceResult;
        }

        [Benchmark]
        public void CalculateWithNewObjectManyTimes()
        {
            for (int i = 0; i < 100; i++)
                 CalculateWithNewObject();
        }

        [Benchmark]
        public void CalculateWithNewObjectManyTimesParallel()
        {
            Parallel.For(0, 100, i => CalculateWithNewObject());
        }
    }
}
