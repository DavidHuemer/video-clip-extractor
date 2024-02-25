using BaseUI.Services.Provider.DependencyFinderService;

namespace BaseUI.Services.Provider.DependencyInjection;

public class DependencyProvider : BaseProvider, IDependencyProvider
{
    private readonly IDependencyFinder _dependencyFinder;
    private readonly DependencyInstanceBuilder _instanceBuilder;

    public DependencyProvider(IDependencyFinder? dependencyFinder = null)
    {
        _instanceBuilder = new DependencyInstanceBuilder(this);
        _dependencyFinder = dependencyFinder ?? new DependencyFinder();
    }

    public void AddSingletonDependency<TInterface, TImplementation>() where TImplementation : class, TInterface
        => AddSingleton<TInterface, TImplementation>();

    public void AddTransientDependency<TInterface, TImplementation>() where TImplementation : TInterface
        => AddTransient<TInterface, TImplementation>();

    public TInterface GetDependency<TInterface>() where TInterface : class
        => Get<TInterface>();

    protected override TInterface Instantiate<TInterface>(Type t)
        => _instanceBuilder.InstantiateType<TInterface>(t);

    protected override Type? FindDependency<TInterface>()
        => _dependencyFinder.FindDependency<TInterface>();
}