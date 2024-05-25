using Build_IT_DataAccess.Common;
using System.Threading.Tasks;

namespace Build_IT_WebApplication.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
