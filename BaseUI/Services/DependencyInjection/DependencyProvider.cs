using BaseUI.Services.Provider;

namespace BaseUI.Services.DependencyInjection;

public class DependencyProvider : BaseProvider, IDependencyProvider
{
    public DependencyProvider()
    {
        InstanceBuilder = new DependencyInstanceBuilder(this);
    }

    protected override DependencyInstanceBuilder InstanceBuilder { get; }

    public void AddTransientDependency<TInterface, TImplementation>() where TImplementation : TInterface
    {
        if (TransientDependencies.ContainsKey(typeof(TInterface)))
            TransientDependencies[typeof(TInterface)] = typeof(TImplementation);
        else
            TransientDependencies.Add(typeof(TInterface), typeof(TImplementation));
    }
}