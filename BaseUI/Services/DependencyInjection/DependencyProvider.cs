using BaseUI.Exceptions.DependencyExceptions;

namespace BaseUI.Services.DependencyInjection;

public class DependencyProvider : IDependencyProvider
{
    public DependencyProvider()
    {
        _instanceBuilder = new DependencyInstanceBuilder(this);
    }

    public void AddSingletonDependency<TInterface, TImplementation>() where TImplementation : class, TInterface
    {
        if (_singletonDependencies.ContainsKey(typeof(TInterface)))
            _singletonDependencies[typeof(TInterface)] =
                _instanceBuilder.InstantiateType<TInterface, TImplementation>()!;
        else
            _singletonDependencies.Add(typeof(TInterface),
                _instanceBuilder.InstantiateType<TInterface, TImplementation>()!);
    }

    public void AddTransientDependency<TInterface, TImplementation>() where TImplementation : TInterface
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

        return _instanceBuilder.InstantiateType<TInterface>(_transientDependencies[typeof(TInterface)]);
    }

    #region Private Fields

    /// <summary>
    ///     The existing singleton dependencies
    /// </summary>
    private readonly Dictionary<Type, object> _singletonDependencies = new();

    /// <summary>
    ///     The transient dependencies
    /// </summary>
    private readonly Dictionary<Type, Type> _transientDependencies = new();

    private readonly DependencyInstanceBuilder _instanceBuilder;

    #endregion
}