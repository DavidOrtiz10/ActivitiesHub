namespace EventsHub.Application.Core.Mapping;

/// <summary>
/// Maps objects using the maps declared by the profiles it is built from. Every map is validated and compiled
/// in the constructor, so a configuration mistake surfaces at startup. The instance is immutable afterwards
/// and safe to share between threads.
/// </summary>
public sealed class Mapper : IMapper
{
    private readonly Dictionary<(Type Source, Type Destination), TypeMap> _maps = [];

    public Mapper(params Profile[] profiles) : this((IEnumerable<Profile>)profiles)
    {
    }

    public Mapper(IEnumerable<Profile> profiles)
    {
        ArgumentNullException.ThrowIfNull(profiles);

        foreach (var profile in profiles)
        {
            foreach (var map in profile.Maps)
            {
                map.ProfileType = profile.GetType();
                if (_maps.TryGetValue((map.SourceType, map.DestinationType), out var existing))
                {
                    throw new InvalidOperationException(
                        $"Map {map.SourceType.Name} -> {map.DestinationType.Name} is declared twice, " +
                        $"by {existing.ProfileType!.Name} and {map.ProfileType.Name}.");
                }

                _maps.Add((map.SourceType, map.DestinationType), map);
            }
        }

        // Build only after every map is registered, so nested maps resolve in any profile order.
        foreach (var map in _maps.Values)
        {
            map.Build(_maps);
        }
    }

    public TDestination Map<TDestination>(object source) where TDestination : class
    {
        ArgumentNullException.ThrowIfNull(source);

        // The map is looked up first, so a missing map fails before anything is created.
        return (TDestination)GetMap(source.GetType(), typeof(TDestination)).MapNew(source);
    }

    public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        where TSource : class
        where TDestination : class
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(destination);

        GetMap(typeof(TSource), typeof(TDestination)).MapInto(source, destination);
        return destination;
    }

    private TypeMap GetMap(Type source, Type destination) =>
        _maps.TryGetValue((source, destination), out var map)
            ? map
            : throw new InvalidOperationException(
                $"No map is declared from {source.Name} to {destination.Name}. " +
                "Declare it with CreateMap in a Profile.");
}
