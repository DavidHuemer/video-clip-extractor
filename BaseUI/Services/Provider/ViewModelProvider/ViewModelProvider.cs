using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyFinderService;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.ViewModels;
using JetBrains.Annotations;

namespace BaseUI.Services.Provider.ViewModelProvider;

[UsedImplicitly]
public class ViewModelProvider : BaseProvider, IViewModelProvider
{
    private readonly IDependencyFinder _dependencyFinder;
    private readonly DependencyInstanceBuilder _instanceBuilder;

    public ViewModelProvider(IDependencyProvider provider)
    {
        _dependencyFinder = provider.GetDependency<IDependencyFinder>();
        _instanceBuilder = new DependencyInstanceBuilder(provider);

        _dependencyFinder.TypePredicate = IsValidViewModel;
    }

    public new void AddSingleton<TInterface, TViewModel>() where TViewModel : BaseViewModel, TInterface =>
        base.AddSingleton<TInterface, TViewModel>();

    public new void AddTransient<TInterface, TViewModel>() where TViewModel : BaseViewModel, TInterface =>
        base.AddTransient<TInterface, TViewModel>();

    public new TInterface Get<TInterface>() where TInterface : class =>
        base.Get<TInterface>();

    protected override TInterface Instantiate<TInterface>(Type t)
        => _instanceBuilder.InstantiateType<TInterface>(t);

    protected override Type? FindDependency<TInterface>()
        => _dependencyFinder.FindDependency<TInterface>();

    private bool IsValidViewModel(Type type)
    {
        // return true when type has no DesignData Attribute
        return !type.GetCustomAttributes(typeof(DesignDataAttribute), true).Any();
    }
}