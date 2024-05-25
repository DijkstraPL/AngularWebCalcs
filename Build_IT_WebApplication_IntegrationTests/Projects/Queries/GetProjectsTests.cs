using Build_IT_DataAccess.Projects.Entites;
using Build_IT_WebApplication.Projects.Queries;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Build_IT_WebApplication_IntegrationTests.Projects.Queries
{
    using static Testing;

    public class GetProjectsTests : BaseTestFixture
    {
        [Test]
        public async Task ShouldReturnPriorityLevels()
        {
            await RunAsDefaultUserAsync();

            var query = new GetAllProjectsQuery();

            var result = await SendAsync(query);
        }

        [Test]
        public async Task ShouldReturnAllProjects()
        {
            await RunAsDefaultUserAsync();

            await AddAsync(new Company
            {
                Name = "Name",
                Projects =
                    {
                        new Project { Name = "Proj1", Description = "Desc1" },
                        new Project { Name = "Proj2", Description = "Desc2" },
                        new Project { Name = "Proj3", Description = "Desc3" },
                        new Project { Name = "Proj4", Description = "Desc4" },
                    }
            });

            var query = new GetAllProjectsQuery();

            var result = await SendAsync(query);

            result.Should().HaveCount(4);
        }

        [Test]
        public async Task ShouldDenyAnonymousUser()
        {
            var query = new GetAllProjectsQuery();

            var action = () => SendAsync(query);

            await action.Should().ThrowAsync<UnauthorizedAccessException>();
        }
    }
}
