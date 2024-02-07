using BaseUI.ViewModels;

namespace BaseUI.Services.Provider.ViewModelProvider;

/// <summary>
/// Responsible for providing view models.
/// View models can be provided as singletons or transient.
/// </summary>
public interface IViewModelProvider
{
    void AddSingletonViewModel<TInterface, TViewModel>() where TViewModel : BaseViewModel, TInterface;

    TInterface GetViewModel<TInterface>();
}