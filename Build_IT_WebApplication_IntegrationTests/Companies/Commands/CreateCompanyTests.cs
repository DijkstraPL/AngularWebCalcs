using Build_IT_DataAccess.Projects.Entites;
using Build_IT_WebApplication.Common.Exceptions;
using Build_IT_WebApplication.Companies.Commands;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Build_IT_WebApplication_IntegrationTests.Companies.Commands
{
    using static Testing;
    public class CreateCompanyTests : BaseTestFixture
    {
        [Test]
        public async Task ShouldRequireMinimumFields()
        {
            var command = new CreateCompanyCommand();

            await FluentActions.Invoking(() =>
                SendAsync(command)).Should().ThrowAsync<ValidationException>();
        }

        [Test]
        public async Task ShouldCreateCompany()
        {
            var userId = await RunAsDefaultUserAsync();

            var command = new CreateCompanyCommand
            {
                Name = "New Company"
            };
            var companyId = await SendAsync(command);

            var item = await FindAsync<Company>(companyId);

            item.Should().NotBeNull();
            item!.Name.Should().Be(command.Name);
            item.CreatedBy.Should().Be(userId);
            item.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
            item.LastModifiedBy.Should().Be(userId);
            item.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
        }
    }
}
