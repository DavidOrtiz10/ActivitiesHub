namespace EventsHub.Application.Core;

/// <summary>
/// Base class for mapping profiles. Declare maps in the constructor with <see cref="CreateMap{TSource, TDestination}"/>.
/// Profiles need a public parameterless constructor so they can be found by assembly scanning.
/// </summary>
public abstract class Profile
{
    private readonly List<(Type Source, Type Destination)> _maps = [];

    internal IReadOnlyList<(Type Source, Type Destination)> Maps => _maps;

    protected void CreateMap<TSource, TDestination>()
        => _maps.Add((typeof(TSource), typeof(TDestination)));
}
