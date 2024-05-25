using System.Collections.Generic;

namespace Build_IT_ScriptInterpreter.Formatters
{
    internal static class MathMLHelper
    {
        #region Public_Methods
        
        public static IEnumerable<string> Split(string value)
        {
            string result = string.Empty;
            bool inParameter = false;
            bool inText = false;
            int bracketLevel = 0;
            foreach (var @char in value)
            {
                if (@char == '(')
                    bracketLevel++;
                else if (@char == ')')
                    bracketLevel--;
                else if (@char == '[')
                    inParameter = true;
                else if (@char == ']')
                    inParameter = false;
                else if (@char == '\'')
                    inText = !inText;

                if (@char == ',' && bracketLevel == 0 && !inParameter && !inText)
                {
                    yield return result;
                    result = string.Empty;
                }
                else
                    result += @char;
            }

            yield return result;
        }

        #endregion // Public_Methods
    }
}
