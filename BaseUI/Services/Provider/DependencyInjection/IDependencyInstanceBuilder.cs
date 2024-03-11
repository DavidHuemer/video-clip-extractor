namespace BaseUI.Services.Provider.DependencyInjection;

public interface IDependencyInstanceBuilder
{
    TInterface InstantiateType<TInterface>(Type implementationType);
}