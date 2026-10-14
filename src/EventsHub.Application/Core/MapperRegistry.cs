using System.Reflection;

namespace EventsHub.Application.Core;

/// <summary>
/// Immutable set of declared maps. Built once at startup so configuration errors surface before the first request.
/// </summary>
internal sealed class MapperRegistry
{
    private readonly Dictionary<(Type Source, Type Destination), TypeMap> _maps;

    private MapperRegistry(Dictionary<(Type, Type), TypeMap> maps) => _maps = maps;

    public static MapperRegistry FromAssemblies(IEnumerable<Assembly> assemblies)
    {
        var profileTypes = assemblies
            .Distinct()
            .SelectMany(a => a.GetTypes())
            .Where(t => t is { IsAbstract: false, IsClass: true } && t.IsSubclassOf(typeof(Profile)))
            .OrderBy(t => t.FullName, StringComparer.Ordinal);

        var profiles = new List<Profile>();
        foreach (var type in profileTypes)
        {
            if (type.GetConstructor(Type.EmptyTypes) is null)
            {
                throw new InvalidOperationException(
                    $"Profile {type.FullName} needs a public parameterless constructor.");
            }

            profiles.Add((Profile)Activator.CreateInstance(type)!);
        }

        return FromProfiles(profiles);
    }

    public static MapperRegistry FromProfiles(IEnumerable<Profile> profiles)
    {
        var maps = new Dictionary<(Type, Type), TypeMap>();

        foreach (var profile in profiles)
        {
            foreach (var (source, destination) in profile.Maps)
            {
                if (maps.TryGetValue((source, destination), out var existing))
                {
                    throw new InvalidOperationException(
                        $"Map {source.Name} -> {destination.Name} is declared twice, " +
                        $"by {existing.Profile.Name} and {profile.GetType().Name}.");
                }

                maps.Add((source, destination), TypeMap.Create(source, destination, profile.GetType()));
            }
        }

        return new MapperRegistry(maps);
    }

    public TypeMap GetMap(Type source, Type destination)
        => _maps.TryGetValue((source, destination), out var map)
            ? map
            : throw new InvalidOperationException(
                $"No map is declared from {source.Name} to {destination.Name}. " +
                "Declare it with CreateMap in a Profile.");

    /// <summary>
    /// The member pairs copied for one source/destination type pair, resolved by name.
    /// </summary>
    internal sealed class TypeMap
    {
        private readonly (PropertyInfo Source, PropertyInfo Destination)[] _members;

        private TypeMap(Type profile, (PropertyInfo, PropertyInfo)[] members)
        {
            Profile = profile;
            _members = members;
        }

        public Type Profile { get; }

        public static TypeMap Create(Type source, Type destination, Type profile)
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;

            var sourceProperties = source.GetProperties(flags)
                .Where(p => p.GetMethod is { IsPublic: true } && p.GetIndexParameters().Length == 0)
                .ToDictionary(p => p.Name, StringComparer.Ordinal);

            var members = new List<(PropertyInfo, PropertyInfo)>();
            foreach (var target in destination.GetProperties(flags))
            {
                if (target.SetMethod is not { IsPublic: true } || target.GetIndexParameters().Length != 0)
                {
                    continue;
                }

                if (!sourceProperties.TryGetValue(target.Name, out var origin))
                {
                    continue;
                }

                if (!target.PropertyType.IsAssignableFrom(origin.PropertyType))
                {
                    throw new InvalidOperationException(
                        $"Map {source.Name} -> {destination.Name} ({profile.Name}): member '{target.Name}' " +
                        $"is {origin.PropertyType.Name} on the source and {target.PropertyType.Name} on the destination.");
                }

                members.Add((origin, target));
            }

            return new TypeMap(profile, [.. members]);
        }

        public void Apply(object source, object destination)
        {
            foreach (var (origin, target) in _members)
            {
                target.SetValue(destination, origin.GetValue(source));
            }
        }
    }
}
