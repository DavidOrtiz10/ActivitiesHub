using System.Collections;
using System.Linq.Expressions;
using System.Reflection;

namespace EventsHub.Application.Core.Mapping;

/// <summary>
/// One declared source to destination map. It is configured by a <see cref="Profile"/>, then built once by
/// <see cref="Mapper"/> into compiled delegates, so a configuration mistake fails at startup.
/// </summary>
internal abstract class TypeMap
{
    public abstract Type SourceType { get; }
    public abstract Type DestinationType { get; }

    /// <summary>The profile that declared this map, used in error messages.</summary>
    public Type? ProfileType { get; set; }

    /// <summary>Compiles the map. Runs after every map is registered, so nested maps can be resolved.</summary>
    public abstract void Build(IReadOnlyDictionary<(Type Source, Type Destination), TypeMap> maps);

    public abstract object MapNew(object source);

    public abstract void MapInto(object source, object destination);

    /// <summary>
    /// Builds the expression that turns a source value into a destination value, or returns null when
    /// the types are not assignable and no nested or collection map covers them.
    /// </summary>
    protected static Expression? ConvertMember(
        Expression value,
        Type from,
        Type to,
        IReadOnlyDictionary<(Type Source, Type Destination), TypeMap> maps)
    {
        if (to.IsAssignableFrom(from))
        {
            return from == to ? value : Expression.Convert(value, to);
        }

        if (maps.TryGetValue((from, to), out var nestedMap))
        {
            Func<object?, object?> mapNested = source => source is null ? null : nestedMap.MapNew(source);
            return InvokeOnObject(mapNested, value, to);
        }

        if (TryGetElementType(from, out var fromElement)
            && TryGetElementType(to, out var toElement)
            && maps.TryGetValue((fromElement, toElement), out var elementMap)
            && to.IsAssignableFrom(typeof(List<>).MakeGenericType(toElement)))
        {
            var listType = typeof(List<>).MakeGenericType(toElement);
            Func<object?, object?> mapCollection = source =>
            {
                if (source is not IEnumerable items)
                {
                    return null;
                }

                var list = (IList)Activator.CreateInstance(listType)!;
                foreach (var item in items)
                {
                    list.Add(item is null ? null : elementMap.MapNew(item));
                }

                return list;
            };
            return InvokeOnObject(mapCollection, value, to);
        }

        return null;
    }

    private static Expression InvokeOnObject(Func<object?, object?> function, Expression value, Type resultType) =>
        Expression.Convert(
            Expression.Invoke(Expression.Constant(function), Expression.Convert(value, typeof(object))),
            resultType);

    private static bool TryGetElementType(Type type, out Type element)
    {
        element = null!;
        if (type == typeof(string))
        {
            return false;
        }

        var enumerable = type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>)
            ? type
            : type.GetInterfaces().FirstOrDefault(
                i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>));
        if (enumerable is null)
        {
            return false;
        }

        element = enumerable.GetGenericArguments()[0];
        return true;
    }
}

internal sealed class TypeMap<TSource, TDestination> : TypeMap, IMappingExpression<TSource, TDestination>
    where TSource : class
    where TDestination : class
{
    // Destination member name -> MapFrom expression, or null when the member is ignored.
    private readonly Dictionary<string, LambdaExpression?> _overrides = new(StringComparer.Ordinal);
    private Action<TSource, TDestination>? _copy;
    private Func<TDestination>? _create;

    public override Type SourceType => typeof(TSource);
    public override Type DestinationType => typeof(TDestination);

    public IMappingExpression<TSource, TDestination> ForMember<TMember>(
        Expression<Func<TDestination, TMember>> destinationMember,
        Action<IMemberOptions<TSource, TMember>> options)
    {
        ArgumentNullException.ThrowIfNull(destinationMember);
        ArgumentNullException.ThrowIfNull(options);

        var property = GetWritableProperty(destinationMember);
        var memberOptions = new MemberOptions<TMember>();
        options(memberOptions);
        if (!memberOptions.IsConfigured)
        {
            throw new ArgumentException(
                $"ForMember({property.Name}) must call MapFrom or Ignore.", nameof(options));
        }

        _overrides[property.Name] = memberOptions.Source;
        return this;
    }

    public override void Build(IReadOnlyDictionary<(Type Source, Type Destination), TypeMap> maps)
    {
        if (typeof(TDestination).IsAbstract || typeof(TDestination).GetConstructor(Type.EmptyTypes) is null)
        {
            throw new InvalidOperationException(
                $"Map {Describe()}: destination {typeof(TDestination).Name} needs a public parameterless constructor.");
        }

        var source = Expression.Parameter(typeof(TSource), "source");
        var destination = Expression.Parameter(typeof(TDestination), "destination");
        var assignments = new List<Expression>();

        var sourceProperties = new Dictionary<string, PropertyInfo>(StringComparer.Ordinal);
        foreach (var property in typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            if (property.GetMethod is { IsPublic: true } && property.GetIndexParameters().Length == 0)
            {
                sourceProperties.TryAdd(property.Name, property);
            }
        }

        foreach (var target in typeof(TDestination).GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            if (target.SetMethod is not { IsPublic: true } || target.GetIndexParameters().Length != 0)
            {
                continue;
            }

            Expression value;
            if (_overrides.TryGetValue(target.Name, out var mapFrom))
            {
                if (mapFrom is null)
                {
                    continue;
                }

                value = Expression.Invoke(mapFrom, source);
                if (value.Type != target.PropertyType)
                {
                    value = Expression.Convert(value, target.PropertyType);
                }
            }
            else if (sourceProperties.TryGetValue(target.Name, out var origin))
            {
                value = ConvertMember(
                            Expression.Property(source, origin), origin.PropertyType, target.PropertyType, maps)
                        ?? throw new InvalidOperationException(
                            $"Map {Describe()}: member '{target.Name}' is {origin.PropertyType.Name} on the " +
                            $"source and {target.PropertyType.Name} on the destination, and no map or override " +
                            "converts it. Declare a map for those types or use ForMember.");
            }
            else
            {
                continue;
            }

            assignments.Add(Expression.Assign(Expression.Property(destination, target), value));
        }

        if (assignments.Count == 0)
        {
            assignments.Add(Expression.Empty());
        }

        _copy = Expression.Lambda<Action<TSource, TDestination>>(
            Expression.Block(typeof(void), assignments), source, destination).Compile();
        _create = Expression.Lambda<Func<TDestination>>(Expression.New(typeof(TDestination))).Compile();
    }

    public override object MapNew(object source)
    {
        var destination = _create!();
        _copy!((TSource)source, destination);
        return destination;
    }

    public override void MapInto(object source, object destination) =>
        _copy!((TSource)source, (TDestination)destination);

    private string Describe() =>
        $"{typeof(TSource).Name} -> {typeof(TDestination).Name} ({ProfileType?.Name ?? "unregistered"})";

    private static PropertyInfo GetWritableProperty<TMember>(Expression<Func<TDestination, TMember>> selector)
    {
        var body = selector.Body;
        while (body is UnaryExpression { NodeType: ExpressionType.Convert or ExpressionType.ConvertChecked } unary)
        {
            body = unary.Operand;
        }

        if (body is MemberExpression { Member: PropertyInfo property, Expression: ParameterExpression parameter }
            && parameter == selector.Parameters[0]
            && property.SetMethod is { IsPublic: true }
            && property.GetIndexParameters().Length == 0)
        {
            return property;
        }

        throw new ArgumentException(
            $"'{selector}' must select a public writable property of {typeof(TDestination).Name}, " +
            "for example d => d.Name.",
            nameof(selector));
    }

    private sealed class MemberOptions<TMember> : IMemberOptions<TSource, TMember>
    {
        public bool IsConfigured { get; private set; }
        public LambdaExpression? Source { get; private set; }

        public void MapFrom(Expression<Func<TSource, TMember>> source)
        {
            ArgumentNullException.ThrowIfNull(source);
            Source = source;
            IsConfigured = true;
        }

        public void Ignore()
        {
            Source = null;
            IsConfigured = true;
        }
    }
}
