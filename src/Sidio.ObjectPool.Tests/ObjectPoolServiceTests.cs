using System.Text;

namespace Sidio.ObjectPool.Tests;

public sealed class ObjectPoolServiceTests
{
    [Fact]
    public void Get_ReturnsObjectFromPool()
    {
        // Arrange
        var objectPool = Microsoft.Extensions.ObjectPool.ObjectPool.Create<StringBuilder>();
        var objectPoolService = new ObjectPoolService<StringBuilder>(objectPool);

        // Act
        var obj = objectPoolService.Get();

        // Assert
        obj.Should().NotBeNull();
    }

    [Fact]
    public void Return_ObjectShouldBeReturned()
    {
        // Arrange
        var objectPool = Microsoft.Extensions.ObjectPool.ObjectPool.Create<StringBuilder>();
        var objectPoolService = new ObjectPoolService<StringBuilder>(objectPool);
        var obj = objectPoolService.Get();

        // Act
        var action = () => objectPoolService.Return(obj);

        // Assert
        action.Should().NotThrow();
    }

    [Fact]
    public void Borrow_ReturnsObjectFromPool()
    {
        // Arrange
        var objectPool = Microsoft.Extensions.ObjectPool.ObjectPool.Create<StringBuilder>();
        var objectPoolService = new ObjectPoolService<StringBuilder>(objectPool);

        // Act
        using var obj = objectPoolService.Borrow();

        // Assert
        obj.Should().NotBeNull();
    }
}