using BaseUI.Services.Basics.Time;
using BaseUI.Services.Dialogs;
using BaseUI.Services.Dialogs.Identifier;
using BaseUI.Services.FileServices;
using BaseUI.Services.FileServices.Implementations;
using BaseUI.Services.ViewModelProvider;
using BaseUI.Services.WindowService;
using BaseUI.Services.WindowService.ActiveWindow;

namespace BaseUI.Services.DependencyInjection;

public static class BaseUiDependencies
{
    public static void AddBaseUiDependencies(IDependencyProvider provider)
    {
        provider.AddSingletonDependency<IViewModelProvider, ViewModelProvider.ViewModelProvider>();
        provider.AddSingletonDependency<IWindowService, WindowService.WindowService>();
        provider.AddSingletonDependency<IActiveWindowManager, ActiveWindowManager>();
        provider.AddSingletonDependency<IDialogService, DialogService>();
        provider.AddTransientDependency<IDialogHostIdentifierService, DialogHostIdentifierService>();
        provider.AddTransientDependency<IFileExplorer, FileExplorer>();
        provider.AddTransientDependency<ITimeService, TimeService>();
    }
}