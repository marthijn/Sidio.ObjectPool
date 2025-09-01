using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ObjectPool;

namespace Sidio.ObjectPool;

/// <summary>
/// This class provides extension methods for <see cref="IServiceProvider"/> to retrieve services related to object pooling.
/// </summary>
public static class ServiceProviderExtensions
{
    /// <summary>
    /// Gets the <see cref="ObjectPoolProvider"/> from the service provider.
    /// </summary>
    /// <param name="serviceProvider">The service provider.</param>
    /// <returns>The <see cref="ObjectPoolProvider"/>.</returns>
    /// <exception cref="System.InvalidOperationException">There is no service of type <see cref="ObjectPoolProvider"/>.</exception>
    public static ObjectPoolProvider GetObjectPoolProvider(this IServiceProvider serviceProvider) =>
        (serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider)))
        .GetRequiredKeyedService<ObjectPoolProvider>(ServiceCollectionExtensions.ObjectPoolProviderKey);
}