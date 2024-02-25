using BaseUI.Services.Provider.DependencyFinderService;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.ViewModels;
using JetBrains.Annotations;

namespace BaseUI.Services.Provider.ViewModelProvider;

[UsedImplicitly]
public class ViewModelProvider(IDependencyProvider provider) : BaseProvider, IViewModelProvider
{
    private readonly IDependencyFinder _dependencyFinder = provider.GetDependency<IDependencyFinder>();
    private readonly DependencyInstanceBuilder _instanceBuilder = new(provider);

    public new void AddSingleton<TInterface, TViewModel>() where TViewModel : BaseViewModel, TInterface =>
        base.AddSingleton<TInterface, TViewModel>();

    public new void AddTransient<TInterface, TViewModel>() where TViewModel : BaseViewModel, TInterface =>
        base.AddTransient<TInterface, TViewModel>();

    public new TInterface Get<TInterface>() where TInterface : class =>
        base.Get<TInterface>();

    //protected override DependencyInstanceBuilder InstanceBuilder { get; } = new(provider);


    // public void AddSingletonViewModel<TInterface, TViewModel>() where TViewModel : BaseViewModel, TInterface =>
    //     AddSingletonDependency<TInterface, TViewModel>();
    //
    // public TInterface GetViewModel<TInterface>() => IsDependencyRegistered<TInterface>()
    //     ? GetDependency<TInterface>()
    //     : AutoResolveViewModel<TInterface>();
    //
    // private TInterface AutoResolveViewModel<TInterface>()
    // {
    //     var assemblies = GetAssemblies();
    //
    //     foreach (var assembly in assemblies)
    //     {
    //         var types = assembly.GetTypes();
    //
    //         foreach (var type in types)
    //         {
    //             // continue if not instance of TInterface or not assignable to TInterface
    //             if (!type.IsAssignableTo(typeof(TInterface)) || type.IsInterface) continue;
    //             
    //             SingletonDependencies.Add(typeof(TInterface), InstanceBuilder.InstantiateType<TInterface>(type)!);
    //             return GetDependency<TInterface>();
    //         }
    //     }
    //
    //     throw new DependencyNotRegisteredException(typeof(TInterface));
    // }
    //
    // private static Assembly[] GetAssemblies() => AppDomain.CurrentDomain.GetAssemblies()
    //     .Where(IsAssemblySuitable).ToArray();
    //
    // private static bool IsAssemblySuitable(Assembly assembly) => assembly.FullName != null &&
    //                                                              !assembly.FullName.StartsWith("System") &&
    //                                                              !assembly.FullName.StartsWith("Microsoft") &&
    //                                                              !assembly.FullName.StartsWith("ReSharper") &&
    //                                                              !assembly.FullName.StartsWith("netstandard") &&
    //                                                              !assembly.FullName.StartsWith("nunit") &&
    //                                                              !assembly.FullName.StartsWith("Moq") &&
    //                                                              !assembly.FullName.StartsWith("testcentric") &&
    //                                                              !assembly.FullName.StartsWith("Castle.Core") &&
    //                                                              !assembly.FullName.StartsWith("JetBrains");
    protected override TInterface Instantiate<TInterface>(Type t)
        => _instanceBuilder.InstantiateType<TInterface>(t);

    protected override Type? FindDependency<TInterface>()
        => _dependencyFinder.FindDependency<TInterface>();
}