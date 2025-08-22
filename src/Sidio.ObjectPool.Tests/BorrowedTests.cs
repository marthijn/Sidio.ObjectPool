using System.Text;

namespace Sidio.ObjectPool.Tests;

public sealed class BorrowedTests
{
    [Fact]
    public void Instance_ReturnsInstance()
    {
        // Arrange
        var objectPool = Microsoft.Extensions.ObjectPool.ObjectPool.Create<StringBuilder>();
        using var borrowed = new Borrowed<StringBuilder>(objectPool);

        // Act
        var instance = borrowed.Instance;

        // Assert
        instance.Should().NotBeNull();
    }

    [Fact]
    public void Dispose_ObjectIsReturned()
    {
        // Arrange
        var objectPoolMock = new Mock<Microsoft.Extensions.ObjectPool.ObjectPool<StringBuilder>>();
        objectPoolMock.Setup(x => x.Get()).Returns(new StringBuilder());
        var borrowed = new Borrowed<StringBuilder>(objectPoolMock.Object);
        var instance = borrowed.Instance;

        // Act
        borrowed.Dispose();

        // Assert
        objectPoolMock.Verify(x => x.Return(It.Is<StringBuilder>(y => y == instance)), Times.Once());
    }
}