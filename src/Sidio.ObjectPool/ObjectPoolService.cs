using Microsoft.Extensions.ObjectPool;

namespace Sidio.ObjectPool;

/// <summary>
/// This service provides an object pool for managing instances of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The instance type.</typeparam>
public sealed class ObjectPoolService<T> : IObjectPoolService<T>
    where T : class
{
    private readonly ObjectPool<T> _pool;

    /// <summary>
    /// Initializes a new instance of the <see cref="ObjectPoolService{T}"/> class with the specified object pool.
    /// </summary>
    /// <param name="pool">The object pool.</param>
    public ObjectPoolService(ObjectPool<T> pool)
    {
        _pool = pool;
    }

    /// <inheritdoc />
    public ObjectPool<T> Pool => _pool;

    /// <inheritdoc />
    public T Get() => _pool.Get();

    /// <inheritdoc />
    public void Return(T instance) => _pool.Return(instance);

    /// <inheritdoc />
    public Borrowed<T> Borrow() => new (_pool);
}