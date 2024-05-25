using BenchmarkDotNet.Running;
using Build_IT_ScriptBenchmark.Scripts;
using System;

namespace Build_IT_ScriptBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<ShearResistanceWithoutShearReinforcement>();
        }
    }
}
