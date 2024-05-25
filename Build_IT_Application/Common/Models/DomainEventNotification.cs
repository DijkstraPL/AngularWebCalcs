using Build_IT_WebDomain.Events;
using MediatR;

namespace Build_IT_WebApplication.Common.Models
{
    public class DomainEventNotification<TDomainEvent> : INotification where TDomainEvent : BuildItDomainEvent
    {
        public DomainEventNotification(TDomainEvent domainEvent)
        {
            DomainEvent = domainEvent;
        }

        public TDomainEvent DomainEvent { get; }
    }
}
