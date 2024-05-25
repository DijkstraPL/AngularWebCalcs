using Build_IT_CommonTools.Extensions;
using Build_IT_ScriptInterpreter.Formatters.Interfaces;
using Build_IT_ScriptInterpreter.Formatters.Nodes;
using Build_IT_ScriptInterpreter.Formatters.Nodes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Build_IT_ScriptInterpreter.Formatters
{
    public class MathMLFormatter : IFormatter
    {
        #region Fields

        private string _value;

        private readonly IEnumerable<char> _numbers = new List<char>
        {
            '0','1','2','3','4','5','6','7','8','9','.'
        };
        private readonly IEnumerable<char> _operations = new List<char>
        {
            '*','/','+','-','%','(',')','<','>','=','!','&','|'
        };

        private readonly string _texts = "^[a-zA-Z ]*$";

        private readonly char _parameterSign = '[';

        private IDictionary<string, Func<string, IMathMLNode>> _functions =
            new Dictionary<string, Func<string, IMathMLNode>>()
            {
                ["POW"] = value => new PowerNode(value),
                ["SQRT"] = value => new SqrtNode(value),
                ["IF"] = value => new IfNode(value),
                ["UNIT"] = value => new EmptyNode(value),
                ["ROUND"] = value => new RoundNode(value),
                ["SWITCH"] = value => new SwitchNode(value),
            };

        private IDictionary<string, (string value, string unit)> _parameters;
        private const string False = "FALSE";
        private const string True = "TRUE";

        #endregion // Fields

        #region Public_Methods

        public IMathMLNode GetMathML(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            var mathML = new MathNode();
            var nodes = Format(value);
            mathML.AddRange(nodes);
            return mathML;
        }

        public IMathMLNode GetMathMLWithValues(string value, IDictionary<string, (string, string)> parameters)
        {
            _parameters = parameters;
            var result = GetMathML(value);
            _parameters = null;
            return result;
        }

        public IEnumerable<IMathMLNode> GetInnerNodes(string value)
        {
            var result = GetMathML(value) as MathNode;
            return result.NestedNodes;
        }

        #endregion // Public_Methods

        #region Internal_Methods

        internal IList<IMathMLNode> Format(string value)
        {
            //_value = Regex.Replace(value, @"\s+", string.Empty);
            _value = Regex.Replace(value, @"(?<=^[^\']*(?:\'[^\']*\'[^\']*)*)[\s](?=(?:[^\']*\'[^\']*\')*[^\']*$)", string.Empty);

            var nodes = new List<IMathMLNode>();

            for (int i = 0; i < _value.Length; i++)
            {
                if (IsNumber(i))
                    nodes.Add(new NumberNode(GetNumber(ref i)));
                else if (IsOperation(i))
                    nodes.Add(GetOperation(ref i));
                else if (IsParameter(i))
                    nodes.Add(new ParameterNode(GetParameter(ref i)));
                else if (IsFunctionName(i))
                {
                    var functionName = GetFunctionName(ref i);
                    nodes.Add(GetFunction(ref i, functionName));
                }
                else if (IsText(i))
                    nodes.Add(new TextNode(GetText(ref i)));
            }

            return nodes;
        }

        #endregion // Internal_Methods

        #region Private_Methods

        private bool IsOperation(int index)
        {
            if (index >= _value.Length)
                return false;
            return _operations.Any(o => o == _value[index]);
        }

        private IMathMLNode GetOperation(ref int i)
        {
            if ((_value[i] == '>' || _value[i] == '<' || _value[i] == '=' || _value[i] == '!') &&
                _value[i + 1] == '=')
                return new OperationNode(_value[i].ToString() + _value[++i].ToString());
            else if (_value[i] == '&' && _value[i + 1] == '&' || _value[i] == '|' && _value[i + 1] == '|')
                return new OperationNode(_value[i].ToString() + _value[++i].ToString());
            return new OperationNode(_value[i].ToString());
        }

        private bool IsNumber(int index)
        {
            if (index >= _value.Length)
                return false;
            return _numbers.Any(n => n == _value[index]);
        }

        private string GetNumber(ref int index)
        {
            string number = _value[index].ToString();
            if (IsNumber(++index))
                number += GetNumber(ref index);
            else
                --index;
            return number;
        }

        private bool IsFunctionName(int index)
        {
            if (index >= _value.Length ||
                _value.Substring(index).ToUpper().StartsWith(True) ||
                _value.Substring(index).ToUpper().StartsWith(False))
                return false;
            var regex = new Regex(_texts);
            return regex.IsMatch(_value[index].ToString());
        }

        private string GetFunctionName(ref int index)
        {
            string text = _value[index].ToString();
            if (IsFunctionName(++index))
                text += GetFunctionName(ref index);
            return text;
        }

        private bool IsFunction(int index)
        {
            var functionName = GetFunctionName(ref index);
            if (_functions.Any(f => f.Key == functionName.ToUpper()))
                return true;
            return false;
        }

        private IMathMLNode GetFunction(ref int index, string functionName)
        {
            var value = _value.Substring(++index);

            int bracketLevel = 1;
            string finalValue = string.Empty;
            foreach (var @char in value)
            {
                if (@char == '(')
                    bracketLevel++;
                else if (@char == ')')
                    bracketLevel--;

                if (bracketLevel == 0)
                    break;

                finalValue += @char;
            }

            index += finalValue.Length;
            if (_functions.ContainsKey(functionName.ToUpper()))
                return _functions[functionName.ToUpper()](finalValue);
            return new DefaultFunctionNode(functionName, finalValue);
        }

        private bool IsParameter(int index)
        {
            if (index >= _value.Length)
                return false;
            return _value[index] == _parameterSign;
        }

        private string GetParameter(ref int i)
        {
            var value = _value.Substring(i + 1, _value.Substring(i).IndexOf(']') - 1);
            i += value.Length + 1;
            if (_parameters != null && _parameters.ContainsKey(value))
            {
                var parameter = _parameters[value];
                return parameter.value + parameter.unit;
            }

            return value;
        }

        private bool IsText(int index)
        {
            if (index >= _value.Length)
                return false;
            return _value[index] == '\'' ||
                _value.Substring(index).ToUpper().StartsWith(True) ||
                _value.Substring(index).ToUpper().StartsWith(False);
        }

        private string GetText(ref int index)
        {
            string value = string.Empty;
            if (_value.Substring(index).ToUpper().StartsWith(True))
            {
                value = _value.Substring(index, True.Length);
                index += value.Length;
                return value;
            }
            else if (_value.Substring(index).ToUpper().StartsWith(False))
            {
                value = _value.Substring(index, False.Length);
                index += value.Length;
                return value;
            }
            else
            {
                value = _value.Substring(index + 1, _value.Substring(index + 1).IndexOf('\''));
                index += value.Length + 1;
                return value;
            }
        }

        #endregion // Private_Methods
    }
}
