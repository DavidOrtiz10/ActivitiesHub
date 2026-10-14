namespace EventsHub.Application.Core;

internal sealed class Mapper(MapperRegistry registry) : IMapper
{
    public TDestination Map<TDestination>(object source)
    {
        ArgumentNullException.ThrowIfNull(source);

        // Look the map up first so a missing map fails before anything is created.
        var map = registry.GetMap(source.GetType(), typeof(TDestination));

        var destination = Activator.CreateInstance<TDestination>();
        map.Apply(source, destination!);
        return destination;
    }

    public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(destination);

        var map = registry.GetMap(typeof(TSource), typeof(TDestination));

        map.Apply(source, destination);
        return destination;
    }
}
