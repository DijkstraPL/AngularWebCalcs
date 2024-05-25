using Build_IT_WebDomain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_WebApplication.Projects.EventHandlers
{
    public class ProjectCreatedEventHandler : INotificationHandler<ProjectCreatedEvent>
    {
        private readonly ILogger<ProjectCreatedEventHandler> _logger;

        public ProjectCreatedEventHandler(ILogger<ProjectCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(ProjectCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("BuildIt Domain Event: {DomainEvent}", notification.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
