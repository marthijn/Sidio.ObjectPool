using Microsoft.Extensions.ObjectPool;

namespace Sidio.ObjectPool;

/// <summary>
/// This struct represents a borrowed instance from an object pool.
/// </summary>
/// <typeparam name="T">The instance type.</typeparam>
public readonly struct Borrowed<T> : IDisposable
    where T : class
{
    private readonly ObjectPool<T> _pool;

    internal Borrowed(ObjectPool<T> pool)
    {
        _pool = pool;
        Instance = pool.Get();
    }

    /// <inheritdoc />
    public void Dispose() => _pool.Return(Instance);

    /// <summary>
    /// Gets the borrowed instance.
    /// </summary>
    public T Instance { get; }
}