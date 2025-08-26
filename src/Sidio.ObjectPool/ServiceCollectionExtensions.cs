using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.ObjectPool;
using Sidio.ObjectPool.Policies;

namespace Sidio.ObjectPool;

/// <summary>
/// The service collection extensions.
/// </summary>
public static class ServiceCollectionExtensions
{
    internal const string ObjectPoolProviderKey = $"{nameof(Sidio)}.{nameof(ObjectPoolProvider)}";

    /// <summary>
    /// Adds an object pool of string builders to the service collection.
    /// </summary>
    /// <param name="serviceCollection">The service collection.</param>
    /// <param name="initialCapacity">The initial capacity.</param>
    /// <param name="maxRetainedCapacity">The max retained capacity.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddStringBuilderObjectPool(this IServiceCollection serviceCollection, int initialCapacity = 256, int maxRetainedCapacity = 1024)
    {
        serviceCollection.AddObjectPoolProvider();
        serviceCollection.TryAddSingleton<IObjectPoolService<StringBuilder>>(sp =>
        {
            var provider = sp.GetObjectPoolProvider();
            var pool = provider.Create(new StringBuilderPolicy(initialCapacity, maxRetainedCapacity));
            return new ObjectPoolService<StringBuilder>(pool);
        });

        return serviceCollection;
    }

    private static IServiceCollection AddObjectPoolProvider(this IServiceCollection serviceCollection)
    {
        serviceCollection.TryAddKeyedSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>(ObjectPoolProviderKey);
        return serviceCollection;
    }
}