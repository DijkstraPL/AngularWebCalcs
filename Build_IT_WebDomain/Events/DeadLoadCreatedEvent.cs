using Build_IT_DataAccess.Projects.Entites;

namespace Build_IT_WebDomain.Events
{
    public class DeadLoadCreatedEvent : BuildItDomainEvent
    {
        public DeadLoad DeadLoad { get; }

        public DeadLoadCreatedEvent(DeadLoad deadLoad)
        {
            DeadLoad = deadLoad;
        }
    }
}
