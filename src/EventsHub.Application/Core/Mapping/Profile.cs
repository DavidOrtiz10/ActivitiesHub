namespace EventsHub.Application.Core.Mapping;

/// <summary>
/// Base class for a group of related maps. Declare them in the constructor with
/// <see cref="CreateMap{TSource, TDestination}"/>. Profiles need a public parameterless constructor so
/// <c>AddMapper</c> can find them, and a profile instance belongs to a single <see cref="Mapper"/>.
/// </summary>
public abstract class Profile
{
    private readonly List<TypeMap> _maps = [];

    internal IReadOnlyList<TypeMap> Maps => _maps;

    protected IMappingExpression<TSource, TDestination> CreateMap<TSource, TDestination>()
        where TSource : class
        where TDestination : class
    {
        var map = new TypeMap<TSource, TDestination>();
        _maps.Add(map);
        return map;
    }
}
