using Build_IT_WebApplication.Common.Behaviours;
using Build_IT_WebApplication.Common.Interfaces;
using Build_IT_WebApplication.Projects.Commands;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Build_IT_WebApplicationTests.Common.Behaviours;

public class RequestLoggerTests
{
    private Mock<ILogger<CreateProjectCommand>> _logger = null!;
    private Mock<ICurrentUserService> _currentUserService = null!;
    private Mock<IIdentityService> _identityService = null!;

    [SetUp]
    public void Setup()
    {
        _logger = new Mock<ILogger<CreateProjectCommand>>();
        _currentUserService = new Mock<ICurrentUserService>();
        _identityService = new Mock<IIdentityService>();
    }

    [Test]
    public async Task ShouldCallGetUserNameAsyncOnceIfAuthenticated()
    {
        _currentUserService.Setup(x => x.UserId).Returns(Guid.NewGuid().ToString());

        var requestLogger = new LoggingBehaviour<CreateProjectCommand>(_logger.Object, _currentUserService.Object, _identityService.Object);

        await requestLogger.Process(new CreateProjectCommand { Name = "Name", Description = "Desc" }, new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public async Task ShouldNotCallGetUserNameAsyncOnceIfUnauthenticated()
    {
        var requestLogger = new LoggingBehaviour<CreateProjectCommand>(_logger.Object, _currentUserService.Object, _identityService.Object);

        await requestLogger.Process(new CreateProjectCommand { Name = "Name", Description = "Desc" }, new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Never);
    }
}
