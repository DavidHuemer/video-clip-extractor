using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.Services.Provider.ViewModelProvider;

namespace BaseUI.ViewModels;

/// <summary>
/// Base class for all view models that require access to the dependency provider.
/// </summary>
/// <param name="provider">The provider that grants access to the dependencies</param>
public abstract class BaseViewModelContainer(IDependencyProvider provider) : BaseViewModel
{
    protected IDependencyProvider DependencyProvider { get; set; } = provider;

    protected IViewModelProvider ViewModelProvider { get; set; } = provider.GetDependency<IViewModelProvider>();
}