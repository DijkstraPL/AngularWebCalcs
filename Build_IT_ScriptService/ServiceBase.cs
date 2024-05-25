using Build_IT_Data.Calculators.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Build_IT_ScriptService
{
    public abstract class ServiceBase : ICalculator
    {
        #region Properties
        
        public string Description { get; protected set; }
        public IResult Result { get; protected set; }

        #endregion // Properties

        #region Public_Methods

        public abstract IResult Calculate();

        public virtual void Map(IList<object> args)
        {
            var properties = this.GetType().GetProperties()
                .Select(p => p.GetValue(this, null) as Property)
                .Where(p => p != null);

            for (int i = 0; i < args.Count; i += 2)
            {
                var property = properties.SingleOrDefault(p => p.Name == args[i].ToString());
                if (property != null)
                    property.SetValue(args[i + 1]);
            }
        }

        #endregion // Public_Methods
    }
}
