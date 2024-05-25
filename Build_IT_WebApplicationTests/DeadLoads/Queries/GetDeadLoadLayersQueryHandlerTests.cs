using AutoMapper;
using Build_IT_DataAccess.Projects.Entites;
using Build_IT_DataAccess.Projects.Repositories.Interfaces;
using Build_IT_WebApplication.DeadLoads.Queries;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Build_IT_WebApplicationTests.DeadLoads.Queries
{
    [TestFixture]
    public class GetDeadLoadLayersQueryHandlerTests
    {
        private Mock<IDeadLoadRepository> _deadLoadRepositoryMock;
        private Mock<IMapper> _mapperMock;

        private GetDeadLoadLayersQueryHandler _handler;

        [SetUp]
        public void Setup()
        {
            _deadLoadRepositoryMock = new Mock<IDeadLoadRepository>();
            _mapperMock = new Mock<IMapper>();
            _handler = new GetDeadLoadLayersQueryHandler(_deadLoadRepositoryMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task Handle_ReturnsExpectedResult()
        {
            // Arrange
            var query = new GetDeadLoadLayersQuery { DeadLoadId = 1 };

            var deadLoadLayers = new List<DeadLoadLayer>();
            _deadLoadRepositoryMock.Setup(x => x.GetLayers(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                                   .ReturnsAsync(deadLoadLayers);

            var expectedResults = new List<DeadLoadLayerResource>();
            _mapperMock.Setup(x => x.Map<List<DeadLoadLayerResource>>(It.IsAny<List<DeadLoadLayer>>()))
                       .Returns(expectedResults);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.EqualTo(expectedResults) );
        }

    }
}
