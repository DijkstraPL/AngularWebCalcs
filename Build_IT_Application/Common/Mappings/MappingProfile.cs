using AutoMapper;
using System;
using System.Linq;
using System.Reflection;

namespace Build_IT_WebApplication.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            SetupMappingFrom(assembly);
            SetupMappingTo(assembly);
        }

        private void SetupMappingTo(Assembly assembly)
        {
            SetupMapping(assembly, typeof(IMapTo<>), "IMapTo");
        }

        private void SetupMappingFrom(Assembly assembly)
        {
            SetupMapping(assembly, typeof(IMapFrom<>), "IMapFrom");
        }

        private void SetupMapping(Assembly assembly, Type mappingType, string interfaceName)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == mappingType))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod("Mapping")
                    ?? type.GetInterface(interfaceName + "`1").GetMethod("Mapping");

                if(instance is null)
                    methodInfo?.Invoke(null, new object[] { this });
                else
                    methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}