using System.Text;
using Microsoft.Extensions.ObjectPool;

namespace Sidio.ObjectPool.Policies;

/// <summary>
/// The <see cref="StringBuilderPolicy"/> class provides a policy for pooling <see cref="StringBuilder"/> instances.
/// </summary>
public sealed class StringBuilderPolicy : PooledObjectPolicy<StringBuilder>
{
    private readonly int _initialCapacity;
    private readonly int _maxRetainedCapacity;

    /// <summary>
    /// Initializes a new instance of the <see cref="StringBuilderPolicy"/> class.
    /// </summary>
    /// <param name="initialCapacity">The initial capacity.</param>
    /// <param name="maxRetainedCapacity">The maximum retained capacity.</param>
    public StringBuilderPolicy(int initialCapacity = 256, int maxRetainedCapacity = 1024)
    {
        _initialCapacity = initialCapacity;
        _maxRetainedCapacity = maxRetainedCapacity;
    }

    /// <inheritdoc />
    public override StringBuilder Create() => new (_initialCapacity);

    /// <inheritdoc />
    public override bool Return(StringBuilder sb)
    {
        if (sb.Capacity > _maxRetainedCapacity)
        {
            // too big, discard it and let GC collect it
            return false;
        }

        sb.Clear(); // reset contents for next caller
        return true;
    }
}