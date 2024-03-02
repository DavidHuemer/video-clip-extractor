namespace BaseUI.Services.Provider.DependencyFinderService;

/// <summary>
/// Responsible for finding dependencies.
/// </summary>
public interface IDependencyFinder
{
    /// <summary>
    /// Predicate to filter types.
    /// </summary>
    Predicate<Type> TypePredicate { set; }

    /// <summary>
    /// Returns the first type that implements the given interface and matches the predicate.
    /// </summary>
    /// <typeparam name="TInterface">The interface that is used to resolve the type.</typeparam>
    /// <returns>Type that implements the given interface. Null if no type is found.</returns>
    Type? FindDependency<TInterface>() where TInterface : class;
}