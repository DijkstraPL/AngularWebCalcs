using AutoMapper;

namespace Build_IT_WebApplication.Common.Mappings
{
    public interface IMapTo<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(GetType(), typeof(T));
    }
}
