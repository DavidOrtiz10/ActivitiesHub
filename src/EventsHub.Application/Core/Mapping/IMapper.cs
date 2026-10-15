namespace EventsHub.Application.Core.Mapping;

/// <summary>
/// Copies data between object types using the maps declared in <see cref="Profile"/> classes.
/// </summary>
public interface IMapper
{
    /// <summary>
    /// Creates a new <typeparamref name="TDestination"/> and fills it from <paramref name="source"/>.
    /// The map is looked up by the runtime type of <paramref name="source"/>.
    /// </summary>
    TDestination Map<TDestination>(object source) where TDestination : class;

    /// <summary>
    /// Copies <paramref name="source"/> onto the existing <paramref name="destination"/> and returns it.
    /// </summary>
    TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        where TSource : class
        where TDestination : class;
}
