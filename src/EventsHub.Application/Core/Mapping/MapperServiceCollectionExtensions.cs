using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace EventsHub.Application.Core.Mapping;

public static class MapperServiceCollectionExtensions
{
    /// <summary>
    /// Registers <see cref="IMapper"/> as a singleton built from every <see cref="Profile"/> found in
    /// <paramref name="assemblies"/>. Throws at registration time if two profiles declare the same map or a
    /// map is invalid.
    /// </summary>
    public static IServiceCollection AddMapper(this IServiceCollection services, params Assembly[] assemblies)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(assemblies);

        var profiles = assemblies
            .Distinct()
            .SelectMany(a => a.GetTypes())
            .Where(t => t is { IsClass: true, IsAbstract: false, IsGenericTypeDefinition: false }
                        && t.IsSubclassOf(typeof(Profile)))
            .OrderBy(t => t.FullName, StringComparer.Ordinal)
            .Select(CreateProfile)
            .ToList();

        return services.AddSingleton<IMapper>(new Mapper(profiles));
    }

    private static Profile CreateProfile(Type type)
    {
        if (type.GetConstructor(Type.EmptyTypes) is null)
        {
            throw new InvalidOperationException(
                $"Profile {type.FullName} needs a public parameterless constructor.");
        }

        try
        {
            return (Profile)Activator.CreateInstance(type)!;
        }
        catch (TargetInvocationException ex) when (ex.InnerException is not null)
        {
            // Surface the real error from the profile constructor instead of the reflection wrapper.
            throw new InvalidOperationException(
                $"Profile {type.FullName} failed to initialize: {ex.InnerException.Message}", ex.InnerException);
        }
    }
}
