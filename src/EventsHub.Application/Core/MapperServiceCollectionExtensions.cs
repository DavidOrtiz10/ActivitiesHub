using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace EventsHub.Application.Core;

public static class MapperServiceCollectionExtensions
{
    /// <summary>
    /// Registers <see cref="IMapper"/> with every <see cref="Profile"/> found in <paramref name="assemblies"/>.
    /// Throws at registration time if two profiles declare the same map or a map has incompatible members.
    /// </summary>
    public static IServiceCollection AddMapper(this IServiceCollection services, params Assembly[] assemblies)
    {
        var registry = MapperRegistry.FromAssemblies(assemblies);

        services.AddSingleton(registry);
        services.AddSingleton<IMapper, Mapper>();

        return services;
    }
}
