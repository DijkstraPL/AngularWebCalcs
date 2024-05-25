using System;

namespace NCalc
{
    public class ParameterArgs : EventArgs
    {
        #region Properties
        
        private object _result;
        public object Result
        {
            get { return _result; }
            set
            {
                _result = value;
                HasResult = true;
            }
        }

        public bool HasResult { get; set; }

        #endregion // Properties
    }
}
