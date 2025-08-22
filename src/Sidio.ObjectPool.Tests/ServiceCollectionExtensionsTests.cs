using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Sidio.ObjectPool.Tests;

public sealed class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddStringBuilderObjectPool_ShouldRegisterServices()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();

        // Act
        var existingServiceCollection = serviceCollection.AddStringBuilderObjectPool();
        var serviceProvider = serviceCollection.BuildServiceProvider();

        // Assert
        existingServiceCollection.Should().BeSameAs(serviceCollection);
        serviceProvider.GetKeyedService<Microsoft.Extensions.ObjectPool.ObjectPoolProvider>("Sidio.ObjectPoolProvider").Should().BeOfType<Microsoft.Extensions.ObjectPool.DefaultObjectPoolProvider>();
        serviceProvider.GetService<IObjectPoolService<StringBuilder>>().Should().NotBeNull();
    }
}