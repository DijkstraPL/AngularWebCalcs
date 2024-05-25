using Build_IT_NCalc.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Build_IT_ScriptInterpreter.Diagrams
{
    public class ParameterMetadata
    {
        public string Name { get;  }
        public NCalcTypeCode TypeCode { get; }
        public Type Type => TypeCode.GetTypeFromCode();

        public ParameterMetadata(string name, NCalcTypeCode typeCode)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            Name = name;
            TypeCode = typeCode;
        }
    }
}
