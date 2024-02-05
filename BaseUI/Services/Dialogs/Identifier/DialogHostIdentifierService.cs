using System.Windows.Media;
using BaseUI.Services.DependencyInjection;
using BaseUI.Services.WindowService.ActiveWindow;
using JetBrains.Annotations;
using MaterialDesignThemes.Wpf;

namespace BaseUI.Services.Dialogs.Identifier;

[UsedImplicitly]
internal class DialogHostIdentifierService(IDependencyProvider provider) : IDialogHostIdentifierService
{
    public string GetIdentifier()
    {
        var activeWindowManager = provider.GetDependency<IActiveWindowManager>();
        return activeWindowManager.ActiveWindow is not DependencyObject activeWindow
            ? string.Empty
            : FindDialogHost(activeWindow);
    }

    private static string FindDialogHost(DependencyObject parent)
    {
        var childCount = VisualTreeHelper.GetChildrenCount(parent);

        for (var i = 0; i < childCount; i++)
        {
            var child = VisualTreeHelper.GetChild(parent, i);

            if (child is DialogHost dialogHost)
                // Found the DialogHost
                return dialogHost.Identifier as string ?? string.Empty;

            // If the child has children, recursively search for DialogHost
            if (VisualTreeHelper.GetChildrenCount(child) > 0) return FindDialogHost(child);
        }

        return string.Empty;
    }
}