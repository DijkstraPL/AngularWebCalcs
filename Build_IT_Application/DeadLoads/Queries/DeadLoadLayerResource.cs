using Build_IT_DataAccess.Projects.Entites;
using Build_IT_WebApplication.Common.Mappings;

namespace Build_IT_WebApplication.DeadLoads.Queries
{
    public class DeadLoadLayerResource : IMapFrom<DeadLoadLayer>
    {
        public int Id { get; init; }
        public int MaterialId { get; init; }
        public int DeadLoadId { get; init; }
        public double? Width { get; init; }
        public double? Height { get; init; }
        public double? Length { get; init; }
        public int? PreviousDeadLoadId { get; init; }
    }
}
