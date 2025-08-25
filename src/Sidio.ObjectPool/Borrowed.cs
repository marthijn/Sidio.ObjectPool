using Microsoft.Extensions.ObjectPool;

namespace Sidio.ObjectPool;

/// <summary>
/// This struct represents a borrowed instance from an object pool.
/// </summary>
/// <typeparam name="T">The object type.</typeparam>
public readonly struct Borrowed<T> : IDisposable
    where T : class
{
    private readonly ObjectPool<T> _pool;

    /// <summary>
    /// Initializes a new instance of the <see cref="Borrowed{T}"/> struct.
    /// </summary>
    /// <param name="pool">The object pool.</param>
    public Borrowed(ObjectPool<T> pool)
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

    /// <summary>
    /// Converts the borrowed <see cref="Instance"/> to its string representation.
    /// </summary>
    /// <returns>A <see cref="string"/>.</returns>
    public override string ToString() => Instance?.ToString() ?? string.Empty;
}