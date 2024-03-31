using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.Services.Provider.ViewModelProvider;

namespace BaseUI.ViewModels;

/// <summary>
/// Base class for all view models that require access to the dependency provider.
/// </summary>
/// <param name="provider">The provider that grants access to the dependencies</param>
public abstract class BaseViewModelContainer(IDependencyProvider provider) : BaseViewModel, IBaseViewModelContainer
{
    protected IViewModelProvider ViewModelProvider { get; } = provider.GetDependency<IViewModelProvider>();
    public IDependencyProvider DependencyProvider { get; } = provider;
}