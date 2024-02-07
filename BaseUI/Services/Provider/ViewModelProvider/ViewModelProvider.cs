using System.Reflection;
using BaseUI.Exceptions.DependencyExceptions;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.ViewModels;
using JetBrains.Annotations;

namespace BaseUI.Services.Provider.ViewModelProvider;

[UsedImplicitly]
public class ViewModelProvider(IDependencyProvider provider) : BaseProvider, IViewModelProvider
{
    protected override DependencyInstanceBuilder InstanceBuilder { get; } = new(provider);

    public void AddSingletonViewModel<TInterface, TViewModel>() where TViewModel : BaseViewModel, TInterface =>
        AddSingletonDependency<TInterface, TViewModel>();

    public TInterface GetViewModel<TInterface>() => IsDependencyRegistered<TInterface>()
        ? GetDependency<TInterface>()
        : AutoResolveViewModel<TInterface>();

    private TInterface AutoResolveViewModel<TInterface>()
    {
        var assemblies = GetAssemblies();

        foreach (var assembly in assemblies)
        {
            var types = assembly.GetTypes();

            foreach (var type in types)
            {
                // continue if not instance of TInterface or not assignable to TInterface
                if (!type.IsAssignableTo(typeof(TInterface)) || type.IsInterface) continue;
                SingletonDependencies.Add(typeof(TInterface), InstanceBuilder.InstantiateType<TInterface>(type)!);
                return GetDependency<TInterface>();
            }
        }

        throw new DependencyNotRegisteredException(typeof(TInterface));
    }

    private static Assembly[] GetAssemblies() => AppDomain.CurrentDomain.GetAssemblies()
        .Where(IsAssemblySuitable).ToArray();

    private static bool IsAssemblySuitable(Assembly assembly) => assembly.FullName != null &&
                                                                 !assembly.FullName.StartsWith("System") &&
                                                                 !assembly.FullName.StartsWith("Microsoft") &&
                                                                 !assembly.FullName.StartsWith("ReSharper") &&
                                                                 !assembly.FullName.StartsWith("netstandard") &&
                                                                 !assembly.FullName.StartsWith("nunit") &&
                                                                 !assembly.FullName.StartsWith("Moq") &&
                                                                 !assembly.FullName.StartsWith("testcentric") &&
                                                                 !assembly.FullName.StartsWith("Castle.Core") &&
                                                                 !assembly.FullName.StartsWith("JetBrains");
}