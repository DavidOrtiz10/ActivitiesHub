using System.Linq.Expressions;

namespace EventsHub.Application.Core.Mapping;

/// <summary>
/// Configures one source to destination map. Returned by <see cref="Profile"/>'s <c>CreateMap</c>.
/// </summary>
public interface IMappingExpression<TSource, TDestination>
{
    /// <summary>
    /// Overrides how one destination member is filled. The override wins over name matching for that member.
    /// </summary>
    IMappingExpression<TSource, TDestination> ForMember<TMember>(
        Expression<Func<TDestination, TMember>> destinationMember,
        Action<IMemberOptions<TSource, TMember>> options);
}

/// <summary>
/// The rule for one destination member. Call exactly one of the methods.
/// </summary>
public interface IMemberOptions<TSource, TMember>
{
    /// <summary>Fills the member from an expression over the source.</summary>
    void MapFrom(Expression<Func<TSource, TMember>> source);

    /// <summary>Never writes the member.</summary>
    void Ignore();
}
