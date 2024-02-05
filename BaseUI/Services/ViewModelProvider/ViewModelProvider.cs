using BaseUI.Services.DependencyInjection;
using BaseUI.Services.Provider;
using BaseUI.ViewModels;
using JetBrains.Annotations;

namespace BaseUI.Services.ViewModelProvider;

[UsedImplicitly]
public class ViewModelProvider(IDependencyProvider provider) : BaseProvider, IViewModelProvider
{
    protected override DependencyInstanceBuilder InstanceBuilder { get; } = new(provider);

    public void AddSingletonViewModel<TInterface, TViewModel>() where TViewModel : BaseViewModel, TInterface
    {
        AddSingletonDependency<TInterface, TViewModel>();
    }

    public TInterface GetViewModel<TInterface>() => GetDependency<TInterface>();
}