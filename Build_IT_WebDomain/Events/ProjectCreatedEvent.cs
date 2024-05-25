using Build_IT_DataAccess.Projects.Entites;

namespace Build_IT_WebDomain.Events
{
    public class ProjectCreatedEvent : BuildItDomainEvent
    {
        public Project Project { get; }

        public ProjectCreatedEvent(Project project)
        {
            Project = project;
        }
    }
}
