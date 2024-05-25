using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Build_IT_NCalcPlayground.Events.Interfaces
{
    public interface IEventAggregator
    {
        T GetEvent<T>() where T : BaseEvent, new();
    }
}
