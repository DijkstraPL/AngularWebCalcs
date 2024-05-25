using System.Runtime.Serialization;
using AutoMapper;
using Build_IT_DataAccess.Projects.Entites;
using Build_IT_WebApplication.Common.Mappings;
using Build_IT_WebApplication.Common.Models;
using Build_IT_WebApplication.Companies.Queries;
using Build_IT_WebApplication.Projects.Queries;
using NUnit.Framework;

namespace Build_IT_WebApplicationTests.Common.Mappings;

public class MappingTests
{
    private readonly IConfigurationProvider _configuration;
    private readonly IMapper _mapper;

    public MappingTests()
    {
        _configuration = new MapperConfiguration(config => 
            config.AddProfile<MappingProfile>());

        _mapper = _configuration.CreateMapper();
    }

    [Test]
    public void ShouldHaveValidConfiguration()
    {
        _configuration.AssertConfigurationIsValid();
    }

    [Test]
    [TestCase(typeof(Project), typeof(ProjectResource))]
    [TestCase(typeof(Company), typeof(CompanyResource))]
    public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
    {
        var instance = GetInstanceOf(source);

        _mapper.Map(instance, source, destination);
    }

    private object GetInstanceOf(Type type)
    {
        if (type.GetConstructor(Type.EmptyTypes) != null)
            return Activator.CreateInstance(type)!;

        // Type without parameterless constructor
        return FormatterServices.GetUninitializedObject(type);
    }
}
