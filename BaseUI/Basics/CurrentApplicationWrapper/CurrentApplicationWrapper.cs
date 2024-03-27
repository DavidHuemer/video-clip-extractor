using BaseUI.Services.Provider.Attributes;

namespace BaseUI.Basics.CurrentApplicationWrapper;

[Transient]
public class CurrentApplicationWrapper : ICurrentApplicationWrapper
{
    public void Run(Action action) =>
        Application.Current.Dispatcher.Invoke(action);
}