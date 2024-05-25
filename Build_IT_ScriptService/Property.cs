using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Build_IT_ScriptService
{
    public abstract class Property
    {
        #region Properties
        
        public string Name { get; }
        public Type Type { get; }
        public bool HasValue { get; protected set; }
        public string Description { get; }
        public bool Required { get; }

        #endregion // Properties

        #region Constructors

        public Property(string name, Type type, string description, bool required = true)
        {
            Name = name;
            Type = type;
            Description = description;
            Required = required;
        }

        #endregion // Constructors

        #region Internal_Methods
        
        internal abstract void SetValue(object value);

        #endregion // Internal_Methods
    }

    public class Property<T> : Property
    {
        #region Properties
        
        private T _value;
        public T Value
        {
            get => _value;
            private set
            {
                _value = value;
                HasValue = true;
            }
        }

        #endregion // Properties

        #region Fields
        
        private readonly Func<object, T> _toType;

        #endregion // Fields

        #region Constructors
        
        public Property(string name, Func<object, T> toType, string description = null, bool required = true)
            : base(name, typeof(T), description, required)
        {
            _toType = toType;
        }
        
        #endregion // Constructors

        #region Internal_Methods
        
        internal override void SetValue(object value)
        {
            Value = _toType(value);
        }

        internal void SetValue(T value)
        {
            Value = value;
        }

        #endregion // Internal_Methods
    }
}
