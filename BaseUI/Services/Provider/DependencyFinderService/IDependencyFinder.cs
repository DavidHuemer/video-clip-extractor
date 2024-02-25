namespace BaseUI.Services.Provider.DependencyFinderService;

/// <summary>
/// Responsible for finding dependencies.
/// </summary>
public interface IDependencyFinder
{
    Type? FindDependency<TInterface>() where TInterface : class;
}