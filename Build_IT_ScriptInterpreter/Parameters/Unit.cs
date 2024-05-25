using Build_IT_CommonTools.Extensions;
using Build_IT_NCalc.Units;
using System;
using System.Collections.Generic;

namespace Build_IT_ScriptInterpreter.Parameters
{
    public class Unit
    {
        private string _unit;

        public Unit(string unit)
        {
            _unit = unit.Replace(" ", string.Empty);
        }

        public IEnumerable<CustomUnit> Format()
        {
            if (string.IsNullOrEmpty(_unit))
                yield break;

            List<string> units = new();

            string collectedSigns = string.Empty;
            bool nextValueInverted = false;
            int bracketsCount = 0;
            foreach (var sign in _unit)
            {
                if (sign == '*')
                {
                    if (nextValueInverted)
                    {
                        collectedSigns = InvertPowerSign(collectedSigns);
                        if (bracketsCount == 0)
                            nextValueInverted = false;
                    }
                    units.Add(collectedSigns);
                    collectedSigns = string.Empty;
                }
                else if (sign == '/')
                {
                    if (nextValueInverted)
                    {
                        collectedSigns = InvertPowerSign(collectedSigns);
                        if (bracketsCount == 0)
                            nextValueInverted = false;
                    }
                    units.Add(collectedSigns);
                    collectedSigns = string.Empty;
                    nextValueInverted = !nextValueInverted;
                }
                else if (sign == '(')
                {
                    bracketsCount++;
                }
                else if (sign == ')')
                {
                    bracketsCount--;
                }
                else
                    collectedSigns += sign;
            }

            if (nextValueInverted)
                collectedSigns = InvertPowerSign(collectedSigns);
            units.Add(collectedSigns);

            foreach (var unit in units)
            {
                var unitSplitted = unit.Split('^');
                if (unitSplitted.Length == 2 && Double.TryParse(unitSplitted[1], out double power))
                    yield return new CustomUnit(unitSplitted[0], power);
                else if (unitSplitted.Length == 1)
                    yield return new CustomUnit(unitSplitted[0]);
                else
                    throw new ArgumentException("Wrong unit " + unit);
            }
        }

        private string InvertPowerSign(string collectedSigns)
        {
            if (collectedSigns.Contains("^-"))
                collectedSigns = collectedSigns.Replace("^-", "^");
            else if (collectedSigns.Contains("^"))
                collectedSigns = collectedSigns.Replace("^", "^-");
            else
                collectedSigns += "^-1";
            return collectedSigns;
        }
    }
}
