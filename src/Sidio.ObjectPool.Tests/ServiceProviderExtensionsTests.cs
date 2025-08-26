using Microsoft.Extensions.DependencyInjection;

namespace Sidio.ObjectPool.Tests;

public sealed class ServiceProviderExtensionsTests
{
    [Fact]
    public void GetObjectPoolProvider_ReturnsObjectPoolProvider()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddStringBuilderObjectPool();
        var serviceProvider = serviceCollection.BuildServiceProvider();

        // Act
        var objectPoolProvider = serviceProvider.GetObjectPoolProvider();

        // Assert
        objectPoolProvider.Should().NotBeNull();
        objectPoolProvider.Should().BeOfType<Microsoft.Extensions.ObjectPool.DefaultObjectPoolProvider>();
    }
}