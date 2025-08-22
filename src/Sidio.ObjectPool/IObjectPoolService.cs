namespace Sidio.ObjectPool;

/// <summary>
/// This interface defines a generic object pool service.
/// </summary>
/// <typeparam name="T">The object type.</typeparam>
public interface IObjectPoolService<T>
    where T : class
{
    /// <summary>
    /// Gets the underlying object pool.
    /// </summary>
    Microsoft.Extensions.ObjectPool.ObjectPool<T> Pool { get; }

    /// <summary>
    /// Gets an object from the pool if one is available, otherwise creates one. This object should be used and
    /// returned to the pool when done (using the <see cref="Return"/> function).
    /// </summary>
    /// <returns>An instance of type <see cref="T"/>.</returns>
    T Get();

    /// <summary>
    /// Returns an object to the pool.
    /// </summary>
    /// <param name="instance">The instance to return.</param>
    void Return(T instance);

    /// <summary>
    /// Borrows an object from the object pool. The object is automatically returned to the pool
    /// when the object is disposed.
    /// </summary>
    /// <returns>A <see cref="Borrowed{T}"/>.</returns>
    Borrowed<T> Borrow();
}