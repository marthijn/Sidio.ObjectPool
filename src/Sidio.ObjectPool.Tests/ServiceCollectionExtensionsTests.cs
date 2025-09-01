using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ObjectPool;

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
        serviceProvider.GetKeyedService<ObjectPoolProvider>("Sidio.ObjectPoolProvider").Should().BeOfType<DefaultObjectPoolProvider>();
        serviceProvider.GetService<IObjectPoolService<StringBuilder>>().Should().NotBeNull();
    }

    [Fact]
    public void AddObjectPoolService_ShouldRegisterServices()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();

        // Act
        var existingServiceCollection = serviceCollection.AddObjectPoolService((provider, _) => provider.Create(new MyBuilderPolicy()));
        var serviceProvider = serviceCollection.BuildServiceProvider();

        // Assert
        existingServiceCollection.Should().BeSameAs(serviceCollection);
        serviceProvider.GetKeyedService<ObjectPoolProvider>("Sidio.ObjectPoolProvider").Should().BeOfType<DefaultObjectPoolProvider>();
        serviceProvider.GetService<IObjectPoolService<MyBuilder>>().Should().NotBeNull();
    }

    private sealed class MyBuilder;

    private sealed class MyBuilderPolicy : PooledObjectPolicy<MyBuilder>
    {
        public override MyBuilder Create()
        {
            return new MyBuilder();
        }

        public override bool Return(MyBuilder obj)
        {
            return false;
        }
    }
}