namespace BaseUI.Services.DependencyInjection;

/// <summary>
///     Responsible for building instances of dependencies
/// </summary>
public class DependencyInstanceBuilder(IDependencyProvider provider)
{
    /// <summary>
    ///     Instantiates a type
    /// </summary>
    /// <typeparam name="TInterface">The interface of the type that should be instantiated</typeparam>
    /// <typeparam name="TImplementation">The type that should be instantiated</typeparam>
    /// <returns>The instantiated type</returns>
    public TInterface InstantiateType<TInterface, TImplementation>() where TImplementation : class, TInterface
    {
        var implementationType = typeof(TImplementation);
        return InstantiateType<TInterface>(implementationType);
    }

    /// <summary>
    ///     Instantiates a type
    /// </summary>
    /// <param name="implementationType">The type that should be instantiated</param>
    /// <typeparam name="TInterface">The interface of the type that should be instantiated</typeparam>
    /// <returns>The instantiated type</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public TInterface InstantiateType<TInterface>(Type implementationType)
    {
        // Check if implementationType has parameterless constructor
        var parameterlessConstructor = implementationType.GetConstructor(Type.EmptyTypes);
        if (parameterlessConstructor != null)
        {
            if (Activator.CreateInstance(implementationType) is not TInterface instance)
                throw new InvalidOperationException($"Failed to instantiate type {implementationType.Name}");

            return instance;
        }

        // Check if implementationType has constructor with only IDependencyProvider as parameter
        var dependencyProviderConstructor = implementationType.GetConstructor([typeof(IDependencyProvider)]);
        if (dependencyProviderConstructor == null)
            throw new InvalidOperationException($"No suitable constructor found for type {implementationType.Name}");
        {
            if (Activator.CreateInstance(implementationType, provider) is not TInterface instance)
                throw new InvalidOperationException($"Failed to instantiate type {implementationType.Name}");

            return instance;
        }
    }
}