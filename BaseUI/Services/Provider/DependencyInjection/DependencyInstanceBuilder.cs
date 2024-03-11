using BaseUI.Services.Provider.InstanceBuilderService;

namespace BaseUI.Services.Provider.DependencyInjection;

/// <summary>
///     Responsible for building instances of dependencies
/// </summary>
public class DependencyInstanceBuilder(IDependencyProvider provider, IInstanceBuilderService? instanceBuilder = null)
    : IDependencyInstanceBuilder
{
    private readonly IInstanceBuilderService _instanceBuilderService =
        instanceBuilder ?? new InstanceBuilderService.InstanceBuilderService();

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
            return _instanceBuilderService.InstantiateType<TInterface>(implementationType);

        // Check if implementationType has constructor with only IDependencyProvider as parameter
        var dependencyProviderConstructor = implementationType.GetConstructor([typeof(IDependencyProvider)]);
        if (dependencyProviderConstructor == null)
            throw new InvalidOperationException($"No suitable constructor found for type {implementationType.Name}");
        {
            return _instanceBuilderService.InstantiateType<TInterface>(implementationType, new object[] { provider });
        }
    }
}