using Build_IT_DataAccess.Common;
using MediatR;

namespace Build_IT_WebDomain.Events
{
    public abstract class BuildItDomainEvent : BaseEvent, INotification
    {
    }
}
