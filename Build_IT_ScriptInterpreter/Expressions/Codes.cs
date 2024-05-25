using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_ScriptInterpreter.Expressions
{
    public static class Codes
    {
        public const string Hidden = "#HIDDEN#";

        public static readonly string[] TokenNames = new string[]
        {
            "<invalid>", "<EOR>", "<DOWN>", "<UP>", "DATETIME", "DIGIT", "E", "EscapeSequence", "FALSE",
            "FLOAT", "HexDigit", "ID", "INTEGER", "LETTER", "NAME", "STRING", "TRUE", "UnicodeEscape", "WS",
            "!", "!=", "%", "&&", "&", "(", ")", "*", "+", ",", "-", "/", ":",
            "<", "<<", "<=", "<>", "=", "==", ">", ">=", ">>", "?", "^", "and", "not", "or", "|", "||", "~"
        };
    }
}
