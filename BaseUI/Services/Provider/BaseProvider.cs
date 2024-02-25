using System.Reflection;
using BaseUI.Exceptions.DependencyExceptions;
using BaseUI.Services.Provider.Attributes;

namespace BaseUI.Services.Provider;

/// <summary>
/// Base class for all providers
/// </summary>
public abstract class BaseProvider
{
    protected readonly Dictionary<Type, Type> SingletonDependencies = new();

    protected readonly Dictionary<Type, object> StoredSingletons = new();
    protected readonly Dictionary<Type, Type> TransientDependencies = new();

    protected abstract TInterface Instantiate<TInterface>(Type t) where TInterface : class;
    protected abstract Type? FindDependency<TInterface>() where TInterface : class;

    #region Add

    /// <summary>
    /// Adds the dependency as a singleton
    /// </summary>
    /// <typeparam name="TInterface">The interface of the dependency</typeparam>
    /// <typeparam name="TImplementation">The actual implementation</typeparam>
    protected void AddSingleton<TInterface, TImplementation>() where TImplementation : class, TInterface
    {
        AddDependency<TInterface, TImplementation>(SingletonDependencies);
    }

    /// <summary>
    /// Adds the dependency as a transient
    /// </summary>
    /// <typeparam name="TInterface">The interface of the dependency</typeparam>
    /// <typeparam name="TImplementation">The actual implementation</typeparam>
    protected void AddTransient<TInterface, TImplementation>() where TImplementation : TInterface
    {
        AddDependency<TInterface, TImplementation>(TransientDependencies);
    }

    private void AddDependency<TInterface, TImplementation>(Dictionary<Type, Type> dependencies)
        where TImplementation : TInterface
    {
        if (dependencies.ContainsKey(typeof(TInterface)))
        {
            dependencies[typeof(TInterface)] = typeof(TImplementation);
        }
        else
        {
            dependencies.Add(typeof(TInterface), typeof(TImplementation));
        }
    }

    #endregion

    #region Get

    protected TInterface Get<TInterface>() where TInterface : class
    {
        // Check if the requested dependency is already stored as a singleton
        if (StoredSingletons.ContainsKey(typeof(TInterface)))
            return (TInterface)StoredSingletons[typeof(TInterface)];

        // Check if the requested dependency is registered as a singleton
        if (SingletonDependencies.ContainsKey(typeof(TInterface)))
        {
            var instance = Instantiate<TInterface>(SingletonDependencies[typeof(TInterface)]);
            StoredSingletons.Add(typeof(TInterface), instance);
            return instance;
        }

        // Check if the requested dependency is registered as a transient
        if (TransientDependencies.ContainsKey(typeof(TInterface)))
            return Instantiate<TInterface>(TransientDependencies[typeof(TInterface)]);

        // If the requested dependency is not registered, try to find it
        var foundInstance = GetByResolving<TInterface>();

        if (foundInstance == null)
            throw new DependencyNotRegisteredException(typeof(TInterface));

        return foundInstance;
    }

    private TInterface? GetByResolving<TInterface>() where TInterface : class
    {
        var foundDependency = FindDependency<TInterface>();
        if (foundDependency == null)
            return null;

        // Check if the found dependency has Singleton attribute
        if (foundDependency.GetCustomAttribute<SingletonAttribute>() != null)
        {
            var instance = Instantiate<TInterface>(foundDependency);
            StoredSingletons.Add(typeof(TInterface), instance);
            return instance;
        }

        TransientDependencies.Add(typeof(TInterface), foundDependency);
        return Instantiate<TInterface>(foundDependency);
    }

    #endregion

    //public DependencyInstanceBuilder DependencyInstanceBuilder { get; set; } = new();
}