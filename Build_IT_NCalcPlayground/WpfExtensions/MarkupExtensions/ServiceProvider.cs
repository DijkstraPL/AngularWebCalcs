using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Build_IT_NCalcPlayground.WpfExtensions.MarkupExtensions
{
    public class ServiceProvider : MarkupExtension
    {
        public Type DataType { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (DataType is null)
                return null;

            var service = ContainerData.Container.Resolve(DataType);
            if (service is not null)
                return service;
            return null;
        }
    }
}
