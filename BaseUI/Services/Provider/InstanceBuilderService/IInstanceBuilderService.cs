namespace BaseUI.Services.Provider.InstanceBuilderService;

public interface IInstanceBuilderService
{
    T InstantiateType<T>(Type type);

    T InstantiateType<T>(Type type, object[] args);
}