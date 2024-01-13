using BaseUI.Exceptions.DependencyExceptions;

namespace BaseUI.Services.DependencyInjection;

public class DependencyProvider : IDependencyProvider
{
    // Singleton collection of dependencies
    private readonly Dictionary<Type, object> _singletonDependencies = new();

    // Transient collection of dependencies
    private readonly Dictionary<Type, Type> _transientDependencies = new();

    public void AddSingletonDependency<TInterface, TImplementation>() where TImplementation : TInterface, new()
    {
        if (_singletonDependencies.ContainsKey(typeof(TInterface)))
            _singletonDependencies[typeof(TInterface)] = new TImplementation();
        else
            _singletonDependencies.Add(typeof(TInterface), new TImplementation());
    }

    public void AddTransientDependency<TInterface, TImplementation>() where TImplementation : TInterface, new()
    {
        if (_transientDependencies.ContainsKey(typeof(TInterface)))
            _transientDependencies[typeof(TInterface)] = typeof(TImplementation);
        else
            _transientDependencies.Add(typeof(TInterface), typeof(TImplementation));
    }

    public TInterface GetDependency<TInterface>()
    {
        if (_singletonDependencies.ContainsKey(typeof(TInterface)))
            return (TInterface)_singletonDependencies[typeof(TInterface)];

        if (!_transientDependencies.ContainsKey(typeof(TInterface)))
            throw new DependencyNotRegisteredException(typeof(TInterface));

        var obj = Activator.CreateInstance(_transientDependencies[typeof(TInterface)]);
        if (obj is TInterface tInterface) return tInterface;

        throw new DependencyNotCreateAbleException(typeof(TInterface));
    }
}