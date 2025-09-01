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
        return serviceCollection.AddObjectPoolService((provider, _) => provider.Create(new StringBuilderPolicy(initialCapacity, maxRetainedCapacity)));
    }

    /// <summary>
    /// Adds a generic object pool service to the service collection.
    /// </summary>
    /// <param name="serviceCollection">The service collection.</param>
    /// <param name="factory">The factory method.</param>
    /// <typeparam name="T">The object pool type.</typeparam>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddObjectPoolService<T>(this IServiceCollection serviceCollection, Func<ObjectPoolProvider, IServiceProvider, ObjectPool<T>> factory)
        where T : class
    {
        serviceCollection.AddObjectPoolProvider();
        serviceCollection.TryAddSingleton<IObjectPoolService<T>>(sp =>
        {
            var provider = sp.GetObjectPoolProvider();
            var pool = factory(provider, sp);
            return new ObjectPoolService<T>(pool);
        });

        return serviceCollection;
    }

    private static void AddObjectPoolProvider(this IServiceCollection serviceCollection)
    {
        serviceCollection.TryAddKeyedSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>(ObjectPoolProviderKey);
    }
}