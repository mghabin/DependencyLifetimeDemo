using DependencyLifetimeDemo.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace DependencyLifetimeDemo.Tests
{
    public class SingletonServiceTests
    {
        [Fact]
        public void SingletonService_CreatesInstance_WithValidProperties()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<SingletonService>>();
            var service = new SingletonService(loggerMock.Object);

            // Assert
            Assert.NotEqual(Guid.Empty, service.InstanceId);
            Assert.True(service.CreatedAt <= DateTime.UtcNow);
            Assert.Equal("Singleton", service.LifetimeType);
        }

        [Fact]
        public void PerformOperation_ReturnsExpectedString()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<SingletonService>>();
            var service = new SingletonService(loggerMock.Object);

            // Act
            var result = service.PerformOperation();

            // Assert
            Assert.Contains(service.InstanceId.ToString(), result);
            Assert.Contains("Singleton service instance", result);
        }
    }
}
