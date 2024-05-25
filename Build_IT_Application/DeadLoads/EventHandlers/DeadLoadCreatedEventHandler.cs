using Build_IT_WebDomain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_WebApplication.DeadLoads.EventHandlers
{
    public class DeadLoadCreatedEventHandler : INotificationHandler<DeadLoadCreatedEvent>
    {
        private readonly ILogger<DeadLoadCreatedEventHandler> _logger;

        public DeadLoadCreatedEventHandler(ILogger<DeadLoadCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DeadLoadCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("BuildIt Domain Event: {DomainEvent}", notification.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
