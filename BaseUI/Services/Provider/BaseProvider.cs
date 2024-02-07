using BaseUI.Exceptions.DependencyExceptions;
using BaseUI.Services.Provider.DependencyInjection;

namespace BaseUI.Services.Provider;

/// <summary>
/// Provides basic functionality for providers
/// </summary>
public abstract class BaseProvider
{
    public void AddSingletonDependency<TInterface, TImplementation>() where TImplementation : class, TInterface
    {
        if (SingletonDependencies.ContainsKey(typeof(TInterface)))
            SingletonDependencies[typeof(TInterface)] =
                InstanceBuilder.InstantiateType<TInterface, TImplementation>()!;
        else
            SingletonDependencies.Add(typeof(TInterface),
                InstanceBuilder.InstantiateType<TInterface, TImplementation>()!);
    }

    public TInterface GetDependency<TInterface>()
    {
        if (SingletonDependencies.ContainsKey(typeof(TInterface)))
            return (TInterface)SingletonDependencies[typeof(TInterface)];

        if (!TransientDependencies.ContainsKey(typeof(TInterface)))
            throw new DependencyNotRegisteredException(typeof(TInterface));

        return InstanceBuilder.InstantiateType<TInterface>(TransientDependencies[typeof(TInterface)]);
    }

    protected bool IsDependencyRegistered<TInterface>() => SingletonDependencies.ContainsKey(typeof(TInterface)) ||
                                                           TransientDependencies.ContainsKey(typeof(TInterface));

    #region Protected Fields

    /// <summary>
    ///     The existing singleton dependencies
    /// </summary>
    protected readonly Dictionary<Type, object> SingletonDependencies = new();

    /// <summary>
    ///     The transient dependencies
    /// </summary>
    protected readonly Dictionary<Type, Type> TransientDependencies = new();

    protected abstract DependencyInstanceBuilder InstanceBuilder { get; }

    #endregion
}