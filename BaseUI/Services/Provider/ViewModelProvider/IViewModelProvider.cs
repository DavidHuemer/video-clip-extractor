using BaseUI.ViewModels;

namespace BaseUI.Services.Provider.ViewModelProvider;

/// <summary>
/// Responsible for providing view models.
/// There is no need to specify the implementation of a view model.
/// The provider will automatically resolve an implementation for the requested view model if there is one.
/// </summary>
public interface IViewModelProvider
{
    void AddSingleton<TInterface, TViewModel>() where TViewModel : BaseViewModel, TInterface;

    void AddTransient<TInterface, TViewModel>() where TViewModel : BaseViewModel, TInterface;

    TInterface Get<TInterface>() where TInterface : class;
}