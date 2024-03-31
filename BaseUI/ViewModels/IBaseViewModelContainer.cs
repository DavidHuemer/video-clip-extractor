using BaseUI.Services.Provider.DependencyInjection;

namespace BaseUI.ViewModels;

public interface IBaseViewModelContainer : IBaseViewModel
{
    IDependencyProvider DependencyProvider { get; }
}