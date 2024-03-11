namespace BaseUI.Services.Provider.InstanceBuilderService;

/// <summary>
/// This service is used to create instances of classes.
/// </summary>
public class InstanceBuilderService : IInstanceBuilderService
{
    public T InstantiateType<T>(Type type)
    {
        var instance = Activator.CreateInstance(type);
        return ValidateInstance<T>(instance);
    }

    public T InstantiateType<T>(Type type, object[] args)
    {
        var instance = Activator.CreateInstance(type, args);
        return ValidateInstance<T>(instance);
    }

    private static T ValidateInstance<T>(object? instance)
    {
        if (instance is not T result)
            throw new InvalidOperationException($"Failed to instantiate type {typeof(T).Name}");

        return result;
    }
}