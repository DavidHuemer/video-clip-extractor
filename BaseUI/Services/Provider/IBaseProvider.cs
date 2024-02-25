namespace BaseUI.Services.Provider;

public interface IBaseProvider
{
    void AddSingleton<TService, TImplementation>() where TService : class where TImplementation : class, TService;
}